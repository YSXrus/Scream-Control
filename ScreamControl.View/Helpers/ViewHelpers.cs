﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ScreamControl.View
{
    [ValueConversion(typeof(string), typeof(string))]
    public class ToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return string.IsNullOrEmpty(str) ? string.Empty : str.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class TimerValueConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            int timerValue = (int)value[0];
            if (timerValue == 0)
                return "";
            if (timerValue == -1 && value[1] != null)
                return value[1].ToString();
            return timerValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PrefixLanguageConverter : IMultiValueConverter
    {

        //TODO: if possible, redo this in more efficient way.

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string resource = value[1].ToString() + value[0].ToString();
                return Application.Current.FindResource(resource);
            }
            return "Connection in unknown state";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConnectionStatesToVisibilityConverter : IMultiValueConverter
    {

        //TODO: if possible, redo this in more efficient way.

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[1] != DependencyProperty.UnsetValue && value[0] != DependencyProperty.UnsetValue)
            {
                ConnectionInfoStates state = (ConnectionInfoStates)value[0];
                Visibility visibility = (Visibility)value[1];
                if (visibility == Visibility.Visible && new[] { ConnectionInfoStates.Ready }.Contains(state))
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class RetryConnectionConverter : IMultiValueConverter
    {

        //TODO: if possible, redo this in more efficient way.

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[1] != DependencyProperty.UnsetValue && value[0]!= DependencyProperty.UnsetValue)
            {
                ConnectionInfoStates state = (ConnectionInfoStates)value[0];
                Visibility visibility = (Visibility)value[1];
                if (visibility == Visibility.Visible && new[] { ConnectionInfoStates.Failed }.Contains(state))
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConnectionStatesToExpandingConverter : IMultiValueConverter
    {

        //TODO: if possible, redo this in more efficient way.

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            ConnectionInfoStates state = (ConnectionInfoStates)value[0];
            Visibility visibility = (Visibility)value[1];
            if (visibility == Visibility.Visible && new[] { ConnectionInfoStates.Connected }.Contains(state))
                return true;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    #region About window converters
    [ValueConversion(typeof(string), typeof(string))]
    public class VersionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            str = String.Format(str, Assembly.GetEntryAssembly().GetName().Version.ToString());
            return string.IsNullOrEmpty(str) ? string.Empty : str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    //Thanks to Thomas Levesque
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}
