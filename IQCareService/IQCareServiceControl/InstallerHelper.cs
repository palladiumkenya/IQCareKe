using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Configuration;
using System.Xml;


namespace IQCareServiceControl
{
    [RunInstaller(true)]
    public partial class InstallerHelper : System.Configuration.Install.Installer
    {
        public InstallerHelper()
        {
            InitializeComponent();
            this.Committed += new InstallEventHandler(InstallerHelper_Committed);
        }

        /// <summary>
        /// Handles the Committed event of the InstallerHelper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="InstallEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void InstallerHelper_Committed(object sender, InstallEventArgs e)
        {
            try
            {
                string assemblypath = this.Context.Parameters["assemblypath"];
                System.Diagnostics.Process.Start(assemblypath);
            }
            catch { }
        }
        /// <summary>
        /// When overridden in a derived class, removes an installation.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after the installation was complete.</param>
        public override void Uninstall(IDictionary savedState)
        {

            base.Uninstall(savedState);

        }
        /// <summary>
        /// When overridden in a derived class, restores the pre-installation state of the computer.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the pre-installation state of the computer.</param>
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
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

            //get the configuration setting
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string appConfigPath = assemblypath + ".config";
            // Write the path to the app.config file
            XmlDocument doc = new XmlDocument();
            doc.Load(appConfigPath);
            //applicationName
            XmlNode nodeDIR = doc.SelectSingleNode("//appSettings/add[@key='ApplicationPath']");
            if (nodeDIR != null)
            {
                nodeDIR.Attributes["value"].Value = targetdir;
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "ApplicationPath");
                node.SetAttribute("value", targetdir);
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }            
            //defaultPath
            XmlNode nodeDefaultPath= doc.SelectSingleNode("//appSettings/add[@key='DefaultPath']");
            if (nodeDefaultPath != null)
            {
                nodeDefaultPath.Attributes["value"].Value = "C:\\IQCareService";
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "DefaultPath");
                node.SetAttribute("value", "C:\\IQCareService");
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            //ServiceName
            XmlNode nodeServiceName = doc.SelectSingleNode("//appSettings/add[@key='ServiceName']");
            if (nodeServiceName != null)
            {
                nodeServiceName.Attributes["value"].Value = "IQCare Service";
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "ServiceName");
                node.SetAttribute("value", "IQCare Service");
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            //ServiceConfigFileName
            XmlNode nodeServiceConfig = doc.SelectSingleNode("//appSettings/add[@key='ServiceConfigFileName']");
            if (nodeServiceConfig != null)
            {
                nodeServiceConfig.Attributes["value"].Value = "RemServer.Service.exe.config";
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "ServiceConfigFileName");
                node.SetAttribute("value", "RemServer.Service.exe.config");
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            //Save changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            doc.Save(appConfigPath);
        }

        /// <summary>
        /// When overridden in a derived class, completes the install transaction.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers in the collection have run.</param>
        public override void Commit(IDictionary savedState)
        {

            base.Commit(savedState);
        }


    }
}
