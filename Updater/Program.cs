﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const string FILE_NAME = "temp.zip";

        private static bool silentMode = false;

        //args[0] - Download URL, args[1] - target EXE to close before update and run after, args[2] - 's' for silent update
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    return;
                }

                var handle = GetConsoleWindow();
                if (args.Length > 2)
                    if (args[2] == "s")
                    {
                        HideWindow(handle, SW_HIDE);
                        silentMode = true;
                    }

                Console.WriteLine("Waiting for app close...");
                bool appClosed = false;
                while (!appClosed)
                {
                    try
                    {
                        CheckMainAppClosed(args[1]);
                        appClosed = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error while closing app: {0}", e.Message);
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadLine();
                    }
                }

                Console.WriteLine("Update started.");
                using (WebClient client = new WebClient())
                {
                    Console.Write("Downloading...");
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnFileDownloadProgress);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnFileDownloadCompleted);
                    StartDownload(client, args[0]).Wait();
                }

                Extract();
                File.Delete(FILE_NAME);

                if (!silentMode)
                    System.Threading.Thread.Sleep(2000);

            }
            finally
            {
                Process.Start(args[1]);
            }
        }

        private static void Extract()
        {
            ZipArchive za = ZipFile.OpenRead(FILE_NAME);
            foreach (ZipArchiveEntry file in za.Entries)
            {
                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(file.FullName));
                    continue;
                }
                file.ExtractToFile(file.FullName, true);
            }
            za.Dispose();
        }

        private static async Task StartDownload(WebClient client, string address)
        {
            await client.DownloadFileTaskAsync(address, "temp.zip");
        }

        private static void CheckMainAppClosed(string exeName)
        {
            if (!Task.Run(() => WaitForClosed(exeName)).Wait(5000))
            {
                foreach (Process p in Process.GetProcessesByName(exeName))
                {
                    p.Kill();
                }
            }
        }

        private static void WaitForClosed(string exeName)
        {
            while (Process.GetProcessesByName(exeName).Length > 0)
            {
                Process.GetProcessesByName(exeName)[0].Kill();
            }
        }

        private static void OnFileDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("{0} / {1}                    {2}%", e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
            Console.SetCursorPosition(0, 2);
        }

        private static void OnFileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.SetCursorPosition(20, 1);
            Console.WriteLine("Done!");
            Console.SetCursorPosition(0, 3);
            //   Console.WriteLine("Press any key...");
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool HideWindow(IntPtr hWnd, int nCmdShow);

    }
}
