using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using Application.Common;
using DataAccess.Common;
using DataAccess.Entity;

namespace RemServer
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
        }
        string sqlServer;
        string databaseName;
        string sqlLogin;
        string sqlPassword;
        private void frmConnection_Load(object sender, EventArgs e)
        {
            init_fields();
        }

        #region "User Functions"
        /// <summary>
        /// Init_fieldses this instance.
        /// </summary>
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
        private int CheckData()
        {
            if (txtServernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Server Name.");
                txtServernm.Focus();
                return 0;
            }
            sqlServer = this.txtServernm.Text.Trim();
            if (txtDBnm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid DataBase Name.");
                txtDBnm.Focus();
                return 0;
            }
            databaseName = this.txtDBnm.Text.Trim();
            if (txtUsernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid User Name.");
                txtUsernm.Focus();
                return 0;
            }
            sqlLogin = this.txtUsernm.Text.Trim();
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Password.");
                txtPassword.Focus();
                return 0;
            }
            else if (txtconfirmpass.Text.Trim() != txtPassword.Text.Trim())
            {
                MessageBox.Show("Invalid Password. Reenter..");
                txtPassword.Text = "";
                txtconfirmpass.Text = "";
                txtPassword.Focus();
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
            if (CheckData() == 1)
            {

                #region "TempConfigSettings"

                Utility objUtil = new Utility();
                string theConString = objUtil.Encrypt(string.Format("data source = {0};uid = {1};pwd = {2};initial catalog = {3}", txtServernm.Text.Trim(), txtUsernm.Text.Trim(), txtPassword.Text.Trim(), txtDBnm.Text.Trim()));
              //  ConfigurationSettings.AppSettings.Set("ConnectionString", theConString);
              // ConfigurationManager.AppSettings[].

                string connectionString = "Application Name=IQCare_EMR;";
                connectionString += "Server=" + sqlServer + ";";
                connectionString += "Type System Version=SQL Server 2005;";
                connectionString += "Database=" + databaseName + ";";
                connectionString += "Integrated Security=false;";
                connectionString += "User ID=" + sqlLogin + ";";
                connectionString += "Password=" + sqlPassword + ";";
                connectionString += "Persist Security Info=True;";

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); ;//ConfigurationManager.AppSettings["ConnectionString"]
                theConString = objUtil.Encrypt(connectionString);
                config.AppSettings.Settings["ConnectionString"].Value = objUtil.Encrypt(connectionString);

               // config.AppSettings.Settings.Add("nonencrypted", connectionString);
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");   
                string filepath = System.Windows.Forms.Application.StartupPath + @"\RemServer.exe.config";

                //////
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                XmlNode Node = doc.DocumentElement.ChildNodes.Item(1);
                Node = Node.ChildNodes.Item(0);
                Node.Attributes["value"].Value = theConString;
                doc.Save(filepath);
                
                #endregion

                ClsObject obj = new ClsObject();
                Hashtable htParams = new Hashtable();
                htParams.Clear();
                DataTable theDT = (DataTable)obj.ReturnObject(htParams, "select top 1 * from Sysobjects", ClsUtility.ObjectEnum.DataTable);
                if (theDT.Rows.Count > 0)
                {
                    MessageBox.Show("Connection Established.");
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Connection Failed. Try Again..");
                    ConfigurationSettings.AppSettings.Set("ConnectionString", "");
                    this.Close();
                }
            }
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}