using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;
using System.Xml;
using System;


namespace RemServer.Service
{
    [RunInstaller(true)]
    public class IQCareServiceInstaller : Installer 
    {
        /// <summary>
        /// The service installer
        /// </summary>
        private ServiceInstaller serviceInstaller;
        /// <summary>
        /// The process installer
        /// </summary>
		private ServiceProcessInstaller processInstaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="IQCareServiceInstaller"/> class.
        /// </summary>
		public IQCareServiceInstaller() 
		{
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();

			processInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
            string c = this.GetServiceName();
            serviceInstaller.ServiceName = "IQCare Service";// this.GetServiceName();// "IQCare Service";
            serviceInstaller.Description = "This service controls the Server Operations of Futures Group PMM System.";

			Installers.Add(serviceInstaller);
			Installers.Add(processInstaller);
		}
        string GetServiceName()
        {
            Process theProc = Process.GetCurrentProcess();
            string Config = theProc.MainModule.FileName;
            Config = Config + ".config";
            var s = ConfigurationManager.AppSettings.Get("ServiceName");
            return s;
        }
        /// <summary>
        /// When overridden in a derived class, performs the installation.
        /// </summary>
        /// <param name="stateSaver">An <see cref="T:System.Collections.IDictionary" /> used to save information needed to perform a commit, rollback, or uninstall operation.</param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            string assemblypath = this.Context.Parameters["assemblypath"];
            string targetdir = this.Context.Parameters["TARGETDIR"];

            ////get the configuration setting
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string appConfigPath = assemblypath + ".config";
            // Write the path to the app.config file
            XmlDocument doc = new XmlDocument();
            doc.Load(appConfigPath);

            XmlNode nodeConnectionString = doc.SelectSingleNode("//appSettings/add[@key='ConnectionString']");
            if (nodeConnectionString != null)
            {
                nodeConnectionString.ParentNode.RemoveChild(nodeConnectionString);
                //nodeDIR.Attributes["value"].Value = targetdir;
            }
            XmlNode nodeIQTools = doc.SelectSingleNode("//appSettings/add[@key='IQToolsConnectionString']");
            if (nodeIQTools != null)
            {
                nodeIQTools.ParentNode.RemoveChild(nodeIQTools);                
            }
            XmlNode nodeAppointment = doc.SelectSingleNode("//appSettings/add[@key='AppointmentNextUpdate']");
            if (nodeAppointment != null)
            {
                nodeAppointment.Attributes["value"].Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "AppointmentNextUpdate");
                node.SetAttribute("value", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"));
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            //Save changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            doc.Save(appConfigPath);
        }
    }
}
