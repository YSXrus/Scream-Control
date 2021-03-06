﻿using ScreamControl.WCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Linq;

namespace MicrophoneTest
{

    class Program
    {

        readonly string[] YES_VARIANTS = { "yes", "y" };

        private class ControllerCallback : IControllerServiceCallback
        {
            WcfScServiceController _parent;

            public ControllerCallback()
            {

            }

            public void ConnectionChanged()
            {

            }

            public void SettingsReceive(List<AppSettingsProperty> settings)
            {

            }

            public void VolumeReceive(float volume)
            {
               
            }
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }

        EventServiceController proxy;

        public void Run()
        {
            try
            {

           //     AppSettingsProperty hui = new AppSettingsProperty("Threshold", "", typeof(float).);

                Console.WriteLine("Searching for service...");

                DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

                Collection<EndpointDiscoveryMetadata> helloWorldServices = discoveryClient.Find(new FindCriteria(typeof(IControllerService))).Endpoints;

                discoveryClient.Close();

                if (helloWorldServices.Count == 0)
                {
                    Console.WriteLine("No services. Try again? (y/n)");
                    var input = Console.ReadKey().KeyChar.ToString();
                    if (YES_VARIANTS.Any(x => x == input))
                        new Program().Run();
                }
                else
                {
                    Console.WriteLine("Something finded, connecting...");
                    EndpointAddress serviceAddress = helloWorldServices[0].Address;

                    IControllerServiceCallback evnt = new ControllerCallback();
                    InstanceContext evntCntx = new InstanceContext(evnt);

                    NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                    binding.ReliableSession.Enabled = true;
                    binding.ReliableSession.Ordered = false;

                    proxy = new EventServiceController(evntCntx, binding, serviceAddress);

                    proxy.Connect();

                    AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

                  //  Console.WriteLine(output);

                    string tmp = "";
                    Console.Write(Environment.NewLine + "Enter Alarm Threshold: ");

                    AppSettingsProperty setting = new AppSettingsProperty("Threshold", "", typeof(float).FullName);
                    while (tmp.ToLower() != "exit")
                    {
                        tmp = Console.ReadLine();
                        setting.value = tmp;
                        proxy.SendSettings(setting);
                    }

                    Console.WriteLine(proxy.DisconnectPrepare());
                    //try
                    //{
                    //    if (proxy.State != System.ServiceModel.CommunicationState.Faulted)
                    //    {
                    //        proxy.Close();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //    proxy.Abort();
                    //}

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }


        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Exiting...");
            if (proxy != null)
            {
                proxy.Disconnect();
                proxy.Close();
            }
        }
    }
}