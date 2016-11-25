using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Xml;


namespace IQCare_Management
{
    /// <summary>
    /// IQCare Management installer helper
    /// </summary>
    [RunInstaller(true)]
    public partial class InstallerHelper : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallerHelper"/> class.
        /// </summary>
        public InstallerHelper()
        {
            InitializeComponent();
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
            string iqcareUrl = this.Context.Parameters["IQCAREURL"];
            if (iqcareUrl == "")
            {
                iqcareUrl = "http://localhost/iqcare/frmlogin.aspx";
            }
            string serverlocation = this.Context.Parameters["SERVERLOCATION"];
            if (serverlocation == "")
            {
                serverlocation = "tcp://127.0.0.1:8001/BusinessProcess.rem";
            }
            else {
                serverlocation = string.Format("tcp://{0}:8001/BusinessProcess.rem",serverlocation);
            }
           
            //get the configuration setting
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string appConfigPath = assemblypath + ".config";
            // Write the path to the app.config file
            XmlDocument doc = new XmlDocument();
            doc.Load(appConfigPath);

            XmlNode nodeLoc = doc.SelectSingleNode("//appSettings/add[@key='ServerLocation']");
            if (nodeLoc != null)
            {
                nodeLoc.Attributes["value"].Value = serverlocation;
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "ServerLocation");
                node.SetAttribute("value", serverlocation);
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            config.Save(ConfigurationSaveMode.Modified);
            XmlNode nodeURL = doc.SelectSingleNode("//appSettings/add[@key='IQCareURL']");
            if (nodeURL != null)
            {
                nodeURL.Attributes["value"].Value = iqcareUrl;
            }
            else
            {
                var node = doc.CreateElement("add");
                node.SetAttribute("key", "IQCareURL");
                node.SetAttribute("value", iqcareUrl);
                doc.SelectSingleNode("//appSettings").AppendChild(node);
            }
            config.Save(ConfigurationSaveMode.Modified);
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
