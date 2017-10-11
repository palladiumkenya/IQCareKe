using System;
using System.Runtime.Remoting;

using Application.BusinessProcess;
using System.Diagnostics;

using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RemServer
{
    class StartPoint
    {
        [STAThread]
 
        static void Main(string[] args)
        {
            System.Diagnostics.Process theProc = Process.GetCurrentProcess();
            string s = theProc.MainModule.FileName;
            s = s + ".config";
            string Config = @"RemServer.exe.config";
            RemotingConfiguration.Configure(Config, false);
            RemotingConfiguration.ApplicationName = "IQCAREEMR";
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);

            Console.Write("Business Server Activated. Press Any Key to Disconnect");
            Console.ReadLine();         
        }
    }
}

