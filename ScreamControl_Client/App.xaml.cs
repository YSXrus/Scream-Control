﻿using ScreamControl;
using ScreamControl.View;
using ScreamControl.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ScreamControl.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        private static List<CultureInfo> m_Languages = new List<CultureInfo>();
        private static bool _isUpdateUpdater = false;
        private static bool _isDebugMode = false;

        internal static ExtendedVersion Version
        {
            get
            {
                Version assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return new ExtendedVersion(assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build);
            }
        }

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            if (!ScreamControl.Client.Properties.Settings.Default.IsUpgraded)
            {
                ScreamControl.Client.Properties.Settings.Default.Upgrade();
                ScreamControl.Client.Properties.Settings.Default.IsUpgraded = true;
            }

            ChangeLogFile();

            Trace.TraceInformation("Scream Control initiating...");
            Trace.Indent();

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ru-RU"));

            App.LanguageChanged += App_LanguageChanged;

            Trace.Unindent();
            Trace.TraceInformation("... OK");
        }

        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();

                dict.Source = new Uri(String.Format("pack://application:,,,/ScreamControl.View;component/Language/lang.{0}.xaml", value.Name), UriKind.RelativeOrAbsolute);

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                ResourceDictionary oldDict = (from d in System.Windows.Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.Contains("Language/lang.")
                                              select d).FirstOrDefault();
                if (oldDict != null)
                {
                    int ind = System.Windows.Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    System.Windows.Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    System.Windows.Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    System.Windows.Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                //4. Вызываем евент для оповещения всех окон.
                if (LanguageChanged != null)
                    LanguageChanged(System.Windows.Application.Current, new EventArgs());
            }

        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            ScreamControl.Client.Properties.Settings.Default.CurrentLanguage = Language;
            ScreamControl.Client.Properties.Settings.Default.Save();
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Language = ScreamControl.Client.Properties.Settings.Default.CurrentLanguage;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Trace.TraceInformation("Scream Control startup...");
            Trace.Indent();

            if (e.Args.Length > 0)
                foreach (string item in e.Args)
                {
                    switch (item)
                    {
                        case "d":
                            _isDebugMode = true;
                            break;
                        default:
                            _isUpdateUpdater = Convert.ToBoolean(item);
                            break;
                    }
                }

            #if !DEBUG
            var checkUpdates = CheckUpdates.Check(App.Version, _isUpdateUpdater, ScreamControl.Client.Properties.Settings.Default.IsStealthMode, _isDebugMode);
            checkUpdates.Wait();
            if (!checkUpdates.Result)
                this.Shutdown();
            #endif

            Language = ScreamControl.Client.Properties.Settings.Default.CurrentLanguage;
            MainWindow window = new MainWindow(_isDebugMode);
            window.DataContext = new MainViewModel();
            window.Show();

            Trace.Unindent();
            Trace.TraceInformation("... Done");
        }

        private void ChangeLogFile()
        {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");

            if (File.Exists("logs/log.txt"))
            {
                File.Copy("logs/log.txt", "logs/log" + DateTime.Now.ToString("ddMM-HHmmss") + ".txt", true);
                File.Delete("logs/log.txt");
            }
            File.Create("logs/log.txt").Close();

            string[] files = Directory.GetFiles("logs");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastWriteTime.AddDays(7) < DateTime.Now)
                    fi.Delete();
            }

            Trace.TraceInformation("Created at {0}", DateTime.Now.ToString());
        }
    }


}
