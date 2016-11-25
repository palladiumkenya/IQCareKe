using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using Application.Common;
using System.Collections.Generic;

namespace IQCare.Service
{
    public partial class frmConnection : Form
    {
        public string ConfigPath;
        string sqlServer;
        string databaseName;
        string sqlLogin;
        string sqlPassword;
        public frmConnection()
        {
            InitializeComponent();

        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            init_fields();
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
          //  this.Height = ConfigIQtools.Checked ? 465 : 465 - 189;
           // panelIQCare.Height = ConfigIQtools.Checked ? 400 : 400 - 189;
            //set css end
        }

        #region "User Functions"
        private void init_fields()
        {
            txtServernm.Text = "";
            txtDBnm.Text = "";
            txtUsernm.Text = "";
            txtPassword.Text = "";
            txtconfirmpass.Text = "";
            txtServernm.Focus();
        }
        /// <summary>
        /// Checks the data.
        /// </summary>
        /// <returns></returns>
        private int CheckIQToolsData()
        {
            sqlServer = databaseName = sqlLogin = sqlPassword = "";
            if (IQToolServer.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Server Name for IQtools.");
                IQToolServer.Focus();
                return 0;
            }
            sqlServer = this.IQToolServer.Text.Trim();

            if (IQToolsDatabase.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid IQTools DataBase Name.");
                IQToolsDatabase.Focus();
                return 0;
            }
            databaseName = IQToolsDatabase.Text.Trim();

            if (IQToolsDBUser.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid IQTools Database User Name.");
                IQToolsDBUser.Focus();
                return 0;
            };
            sqlLogin = IQToolsDBUser.Text.Trim();
            if (IQToolsDBPass.Text.Trim() == "")
            {
                MessageBox.Show("Enter a I Valid QTools Database Password.");
                IQToolsDBPass.Focus();
                return 0;
            };

            if (IQToolsDBPass.Text.Trim() != IQToolsDBPassConfirm.Text.Trim())
            {
                MessageBox.Show("Invalid IQtools Password. Reenter..");
                IQToolsDBPassConfirm.Text = "";
                IQToolsDBPass.Text = "";
                IQToolsDBPass.Focus();
                return 0;
            };
            sqlPassword = IQToolsDBPass.Text.Trim();

            return 1;

        }
        /// <summary>
        /// Checks the data.
        /// </summary>
        /// <returns></returns>
        private int CheckData()
        {
            sqlServer = databaseName = sqlLogin = sqlPassword = "";
            if (this.txtServernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Server Name.");
                this.txtServernm.Focus();
                return 0;
            }
            sqlServer = this.txtServernm.Text.Trim();
            if (this.txtDBnm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid DataBase Name.");
                this.txtDBnm.Focus();
                return 0;
            }
            databaseName = this.txtDBnm.Text.Trim();
            if (this.txtUsernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid User Name.");
                this.txtUsernm.Focus();
                return 0;
            }
            sqlLogin = this.txtUsernm.Text.Trim();
            if (this.txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Password.");
                this.txtPassword.Focus();
                return 0;
            }
            if (this.txtconfirmpass.Text.Trim() != this.txtPassword.Text.Trim())
            {
                MessageBox.Show("Invalid Password. Reenter..");
                this.txtPassword.Text = "";
                this.txtconfirmpass.Text = "";
                this.txtPassword.Focus();
                return 0;
            }
            sqlPassword = this.txtPassword.Text.Trim();
            return 1;
        }
        #endregion

        private void txtServernm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtServernm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid Server Name.");
                    txtServernm.Focus();
                }
            }
        }

        private void txtDBnm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtDBnm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid DataBase Name.");
                    txtDBnm.Focus();
                }
            }
        }

        private void txtUsernm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsernm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid User Name.");
                    txtUsernm.Focus();

                }
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid Password.");
                    txtPassword.Focus();
                }
            }
        }

        private void txtconfirmpass_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtPassword.Text.Trim() != txtconfirmpass.Text.Trim())
                {
                    MessageBox.Show("Invalid Password. Reenter..");
                    txtPassword.Text = "";
                    txtconfirmpass.Text = "";
                    txtPassword.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filepath = ConfigPath + "\\RemServer.Service.exe.config";
            Utility objUtil = new Utility();
            XmlDocument doc = new XmlDocument();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            doc.Load(filepath);          
            string strIQToolsDB;
            string strIQCareDB;
            if (CheckData() == 1)
            {

                #region "TempConfigSettings"
                //string theConString = objUtil.Encrypt(string.Format("data source = {0};uid = {1};pwd = {2};initial catalog = {3}", txtServernm.Text.Trim(), txtUsernm.Text.Trim(), txtPassword.Text.Trim(), txtDBnm.Text.Trim()));
                //ConfigurationManager.AppSettings.Set("ConnectionString", theConString);

                //doc.SelectSingleNode("//appSettings/add[@key='ConnectionString']").Attributes["value"].Value = theConString;
                //  ConfigurationSettings.AppSettings.Set("ConnectionString", theConString);
                string connectionString = "Application Name=IQCare_EMR;";
                connectionString += "Server=" + sqlServer + ";";
                connectionString += "Type System Version=SQL Server 2005;";
                connectionString += "Database=" + databaseName + ";";
                connectionString += "Integrated Security=false;";
                connectionString += "User ID=" + sqlLogin + ";";
                connectionString += "Password=" + sqlPassword + ";";
                connectionString += "Persist Security Info=True;";
                strIQCareDB = databaseName;

                XmlNode nodeIQCare = doc.SelectSingleNode("//appSettings/add[@key='ConnectionString']");
                if (nodeIQCare != null)
                {
                    nodeIQCare.Attributes["value"].Value = objUtil.Encrypt(connectionString);
                }
                else
                {
                    var node = doc.CreateElement("add");
                    node.SetAttribute("key", "ConnectionString");
                    node.SetAttribute("value", objUtil.Encrypt(connectionString));
                    doc.SelectSingleNode("//appSettings").AppendChild(node);
                }
                if (ConfigurationManager.AppSettings["ConnectionString"] != null)
                {
                    ConfigurationManager.AppSettings.Set("ConnectionString", objUtil.Encrypt(connectionString));
                }
                else
                {
                    config.AppSettings.Settings.Add("ConnectionString", objUtil.Encrypt(connectionString));
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
            //////
            //XmlDocument doc = new XmlDocument();
            //doc.Load(filepath);
            //XmlNode Node = doc.DocumentElement.ChildNodes.Item(1);
            //Node = Node.ChildNodes.Item(0);
            //Node.Attributes["value"].Value = theConString;
            //doc.Save(filepath);

                #endregion
            #region IQToolsConnstring

            if (ConfigIQtools.Checked = true && CheckIQToolsData() == 1)
            {
                string connectionString = "Application Name=IQTools_EMR;";
                connectionString += "Server=" + sqlServer + ";";
                connectionString += "Type System Version=SQL Server 2005;";
                connectionString += "Database=" + databaseName + ";";
                connectionString += "Integrated Security=false;";
                connectionString += "User ID=" + sqlLogin + ";";
                connectionString += "Password=" + sqlPassword + ";";
                connectionString += "Persist Security Info=True;MultipleActiveResultSets=true;";
              
                strIQToolsDB = databaseName;

                XmlNode nodeIQTools = doc.SelectSingleNode("//appSettings/add[@key='IQToolsConnectionString']");
                if (nodeIQTools != null)
                {
                    nodeIQTools.Attributes["value"].Value = objUtil.Encrypt(connectionString);
                }
                else
                {
                    var node = doc.CreateElement("add");
                    node.SetAttribute("key", "IQToolsConnectionString");
                    node.SetAttribute("value", objUtil.Encrypt(connectionString));
                    doc.SelectSingleNode("//appSettings").AppendChild(node);
                }
                if (ConfigurationManager.AppSettings["IQToolsConnectionString"] != null)
                {
                    ConfigurationManager.AppSettings.Set("IQToolsConnectionString", objUtil.Encrypt(connectionString));
                }
                else
                {
                    config.AppSettings.Settings.Add("IQToolsConnectionString", objUtil.Encrypt(connectionString));
                    config.Save(ConfigurationSaveMode.Modified);
                }

                XmlNode nodeIQToolsRefreshTime = doc.SelectSingleNode("//appSettings/add[@key='IQToolsNextRefreshDateTime']");
                if (nodeIQToolsRefreshTime != null)
                {
                    nodeIQToolsRefreshTime.Attributes["value"].Value = (DateTime.Now.AddMinutes(3)).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    var node = doc.CreateElement("add");
                    node.SetAttribute("key", "IQToolsNextRefreshDateTime");
                    node.SetAttribute("value", (DateTime.Now.AddMinutes(3)).ToString("o"));
                    doc.SelectSingleNode("//appSettings").AppendChild(node);
                }

                //add the sps which runs on IQToolsRefresh - encrypt them
                XmlNode nodeIQToolsInit = doc.SelectSingleNode("//appSettings/add[@key='IQToolsInitializationProcedures']");
                if (nodeIQToolsInit != null)
                {
                        //do nothing
                }
                else
                {

                    //set this procedures to run in this order, separate by comma;
                    string strProcedures ;
                    //= ";;;";
                    List<string> procedures = new List<string>()
                    {
                    "pr_CreateSiteDetails_IQTools",
                    "pr_CreatePatientMaster_IQTools",
                    "pr_CreatePharmacyMaster_IQTools",
                    "pr_CreateLabMaster_IQTools",
                    "pr_CreateClinicalEncountersMaster_IQTools",
                    "pr_CreateLastStatusMaster_IQTools",
                    "pr_CreateARTPatientsMaster_IQTools",
                    "pr_CreatePregnanciesMaster_IQTools",
                    "pr_CreateOIsMaster_IQTools",
                    "pr_CreateTBPatientsMaster_IQTools",
                    "pr_CreateHEIMaster_IQTools",
                    "pr_CreateANCMothersMaster_IQTools",
                    "pr_CreateFamilyInfoMaster_IQTools"
                    };

                    strProcedures = string.Join(";", procedures.ToArray());
                    var node = doc.CreateElement("add");
                    node.SetAttribute("key", "IQToolsInitializationProcedures");
                    node.SetAttribute("value", objUtil.Encrypt(strProcedures));
                    doc.SelectSingleNode("//appSettings").AppendChild(node);
                }

            }
            #endregion

            //////
            doc.Save(filepath);
            ConfigurationManager.RefreshSection("appSettings");
            //XmlNode Node = doc.DocumentElement.ChildNodes.Item(1);
            //Node = Node.ChildNodes.Item(0);
            //Node.Attributes["value"].Value = theConString;


            this.Close();

        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Leave event of the txtServernm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtServernm_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IQToolServer.Text.Trim()) && !string.IsNullOrEmpty(txtServernm.Text.Trim()))
                IQToolServer.Text = txtServernm.Text;
        }

        private void txtUsernm_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IQToolsDBUser.Text.Trim()) && !string.IsNullOrEmpty(txtUsernm.Text.Trim()))
                IQToolsDBUser.Text = txtUsernm.Text;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IQToolsDBPass.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                IQToolsDBPass.Text = txtPassword.Text;
        }

        /// <summary>
        /// Handles the Leave event of the txtconfirmpass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtconfirmpass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IQToolsDBPassConfirm.Text.Trim()) && !string.IsNullOrEmpty(txtconfirmpass.Text.Trim()))
                IQToolsDBPassConfirm.Text = txtconfirmpass.Text;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ConfigIQtools control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ConfigIQtools_CheckedChanged(object sender, EventArgs e)
        {

            grpbIQtools.Enabled = ConfigIQtools.Checked;
           // this.Height = ConfigIQtools.Checked ? 459 : 459-189;
           // grpbIQtools.Enabled = ConfigIQtools.Checked;
        }




    }
}