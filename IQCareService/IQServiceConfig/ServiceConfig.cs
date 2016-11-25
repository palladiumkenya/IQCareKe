using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.IO;

namespace IQServiceConfig
{
    public partial class ServiceConfig : Form
    {
        /// <summary>
        /// The serviceconfig instance
        /// </summary>
        static ServiceConfig serviceconfigInstance;

        static string _servicePath;
        static string _serviveName;
        static string _serviceConfigFileName;
        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <value>
        /// The service instance.
        /// </value>
        public static ServiceConfig ServiceInstance(string serviceName, string servicePath, string configFile)
        {
            
                if (serviceconfigInstance == null)
                {
                   

                    _servicePath = servicePath;
                    _serviveName = serviceName;
                    _serviceConfigFileName = configFile;
                    serviceconfigInstance = new ServiceConfig();
                    
                }
                return serviceconfigInstance;
          
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfig"/> class.
        /// </summary>
       public ServiceConfig()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Makes the service table.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeServiceTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Item", Type.GetType("System.String"));
            theDT.Columns.Add("Value", Type.GetType("System.String"));
            return theDT;
        }

        /// <summary>
        /// Sets the form property.
        /// </summary>
        private void SetFormProperty()
        {
        }


        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceConfig"/> class from being created.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="servicePath">The service path.</param>
        /// <param name="configFile">The configuration file.</param>
     
        /// <summary>
        /// Handles the MouseDoubleClick event of the theNotifier control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void theNotifier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.theControl.Refresh();
                this.txtStatus.Text = this.theControl.Status.ToString();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Handles the Click event of the btnConfigChanges control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnConfigChanges_Click(object sender, EventArgs e)
        {
            int i = 0;
            XmlDocument theXML = new XmlDocument();
            theXML.Load(this.txtServicePath.Text + @"\RemServer.Service.exe.config");
            XmlElement theNodeElem = theXML.DocumentElement["appSettings"];
            DataTable theDT = (DataTable)this.grdServiceConfig.DataSource;
            foreach (XmlNode theNode in theNodeElem.ChildNodes)
            {
                if (theNode.Attributes != null)
                {
                    theDT.NewRow();
                    theNode.Attributes[0].Value = theDT.Rows[i][0].ToString();
                    theNode.Attributes[1].Value = theDT.Rows[i][1].ToString();
                    i++;
                }
            }
            theXML.Save(this.txtServicePath.Text + @"\RemServer.Service.exe.config");
        }

        /// <summary>
        /// Handles the Click event of the btnConString control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnConString_Click(object sender, EventArgs e)
        {
            string fileName = _serviceConfigFileName;// ConfigurationManager.AppSettings["ServiceConfigFileName"].ToString();
            IQConnection.ConnectionConfig connectionForm = new IQConnection.ConnectionConfig(this.txtServicePath.Text.Trim()+"\\"+ fileName);
            //thefrm.ConfigPath = this.txtServicePath.Text;
           connectionForm.ShowDialog();
            this.Fill_Grid();
        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.theControl.Start();
                          this.grdServiceConfig.ReadOnly = true;
                this.btnConString.Enabled = false;
                this.btnConfigChanges.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "IQTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.theControl.Stop();
                this.grdServiceConfig.ReadOnly = false;
                this.btnConString.Enabled = true;
                this.btnConfigChanges.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "IQTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Clear_s the fields.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        private void Clear_Fields(Control theContainer)
        {
            foreach (Control x in theContainer.Controls)
            {
                if (((x.GetType().ToString() == "System.Windows.Forms.TabPage") || (x.GetType().ToString() == "System.Windows.Forms.Panel")) || (x.GetType().ToString() == "System.Windows.Forms.GroupBox"))
                {
                    this.Clear_Fields(x);
                    continue;
                }
                if (((x.GetType().ToString() == "System.Windows.Forms.TextBox") && (((TextBox)x).Name != "txtSrvNm")) && (((TextBox)x).Name != "txtServicePath"))
                {
                    ((TextBox)x).Text = "";
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the cmd_Minimise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmd_Minimise_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
            base.Hide();
        }

      

        private void Fill_Grid()
        {
            this.FillServiceGrid();
        }

        /// <summary>
        /// Fills the service grid.
        /// </summary>
        private void FillServiceGrid()
        {
            DataTable theDT = this.MakeServiceTable();
            XmlDocument theXML = new XmlDocument();
            theXML.Load(this.txtServicePath.Text + @"\RemServer.Service.exe.config");
            XmlElement theNodeElem = theXML.DocumentElement["appSettings"];
            foreach (XmlNode theNode in theNodeElem.ChildNodes)
            {
                if (theNode.Attributes != null)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theNode.Attributes[0].Value.ToString();
                    theDR[1] = theNode.Attributes[1].Value.ToString();
                    theDT.Rows.Add(theDR);
                }
            }
            this.grdServiceConfig.DataSource = theDT;
        }
        /// <summary>
        /// Gets the service path.
        /// </summary>
        /// <value>
        /// The service path.
        /// </value>
        //string ServicePath
        //{
        //    get
        //    {
        //        //string path = Path.Combine(Application.StartupPath, "..");
        //       // return Path.GetFullPath(path);// ConfigurationManager.AppSettings["ServicePath"].ToString();
        //        return _servicePath;
        //    }
        //}
        //   /// <summary>
        /// Handles the Load event of the ServiceConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServiceConfig_Load(object sender, EventArgs e)
        {
            try
            {

                string servicePath = _servicePath;// ConfigurationManager.AppSettings["ServicePath"].ToString();
                string serviceName = _serviveName;
                this.SetFormProperty();
                this.theControl.ServiceName = serviceName;// "NjungeService";// "IQCare Service";
                
                this.txtSrvNm.Text = this.theControl.DisplayName.ToString();
                this.txtStatus.Text = this.theControl.Status.ToString();

//                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);



                this.txtServicePath.Text = servicePath;// AppDomain.CurrentDomain.BaseDirectory; //@"C:\IQCareService";
                if (this.theControl.Status.ToString() == "Running")
                {
                    this.btnConString.Enabled = false;
                    this.btnConfigChanges.Enabled = false;
                }
                this.Fill_Grid();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "IQTool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.Close();
            }
        }
    }
}
