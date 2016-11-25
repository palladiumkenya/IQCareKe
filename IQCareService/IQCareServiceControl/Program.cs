using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace IQCareServiceControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IQServiceControl serviceControl = IQServiceControl.ServiceInstance;
            Application.Run(serviceControl);
          
           
        }
    }
}
