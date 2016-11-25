using System;
using System.Data;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Xml;
//using BusinessLayer;
using Application.Common;
using System.Configuration;


namespace IQCare.Service
{
    public partial class frmService : Form
    {
        ServiceController theControl = new ServiceController();
        public frmService()
        {
            InitializeComponent();
        }

        private void Fill_Grid()
        {
            FillServiceGrid();
        }
        private void FillServiceGrid()
        {
            //int i = 0;
            //int theNoRows = 0;
            DataTable theDT = MakeServiceTable();
            XmlDocument theXML = new XmlDocument();
            theXML.Load(txtServicePath.Text + "\\RemServer.Service.exe.config");
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
            grdServiceConfig.DataSource = theDT;
        }

        private DataTable MakeServiceTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Item", System.Type.GetType("System.String"));
            theDT.Columns.Add("Value", System.Type.GetType("System.String"));
            return theDT;
        }

        private void Clear_Fields(Control theContainer)
        {
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType().ToString() == "System.Windows.Forms.TabPage" || x.GetType().ToString() == "System.Windows.Forms.Panel" || x.GetType().ToString() == "System.Windows.Forms.GroupBox")
                {
                    Clear_Fields(x);
                }
                else
                {
                    if (x.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        if (((TextBox)x).Name != "txtSrvNm" && ((TextBox)x).Name != "txtServicePath")
                            ((TextBox)x).Text = "";

                    }
                }
            }
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            try
            {
                SetFormProperty();

                //Clear_Fields(tbIQCare);
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string s = config.AppSettings.Settings["ServiceName"].Value;
                string path = config.AppSettings.Settings["ServicePath"].Value;
                theControl.ServiceName = s; //"NjungeService";

                txtSrvNm.Text = theControl.DisplayName.ToString();
                txtStatus.Text = theControl.Status.ToString();
                txtServicePath.Text = path;// "C:\\HMIS\\tfs2\\IQCare\\Package\\Service";
                //tbIQCare.SelectedIndex = 0;
                if (theControl.Status.ToString() == "Running")
                {
                    btnConString.Enabled = false;
                    btnConfigChanges.Enabled = false;
                }
                Fill_Grid();

                //set css begin
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                //set css end
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(),"IQTool",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetFormProperty()
        {
            //////clsForm theFrm = new clsForm();
            //////this.Top = theFrm.Top;
            //////this.Left = theFrm.Left;
            //////this.Height = theFrm.Height;
            //////this.Width = theFrm.Width;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                theControl.Start();
                grdServiceConfig.ReadOnly = true;
                btnConString.Enabled = false;
                btnConfigChanges.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "IQTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConString_Click(object sender, EventArgs e)
        {
            frmConnection thefrm = new frmConnection();
            thefrm.ConfigPath = txtServicePath.Text;
            thefrm.ShowDialog();
            Fill_Grid();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                theControl.Stop();
                grdServiceConfig.ReadOnly = false;
                btnConString.Enabled = true;
                btnConfigChanges.Enabled = true;
                //theNotifier.Text = "IQCare [" + theControl.Status.ToString() + "]";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "IQTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfigChanges_Click(object sender, EventArgs e)
        {
            int i = 0;
          //  int theNoRows = 0;
            XmlDocument theXML = new XmlDocument();
            theXML.Load(txtServicePath.Text + "\\RemServer.Service.exe.config");
            XmlElement theNodeElem = theXML.DocumentElement["appSettings"];
            DataTable theDT = (DataTable)grdServiceConfig.DataSource;
            foreach (XmlNode theNode in theNodeElem.ChildNodes)
            {
                if (theNode.Attributes != null)
                {
                    DataRow theDR = theDT.NewRow();
                    theNode.Attributes[0].Value = theDT.Rows[i][0].ToString();
                    theNode.Attributes[1].Value = theDT.Rows[i][1].ToString();
                    i = i + 1;
                }
            }
            theXML.Save(txtServicePath.Text + "\\RemServer.Service.exe.config");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                theControl.Refresh();
                txtStatus.Text = theControl.Status.ToString();
               // System.Drawing.Icon theIcon;
               
            }
            catch
            {
            }
        }

        private void theNotifier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Clear_Fields(tbIQCare);
            this.Show();
            this.WindowState = FormWindowState.Normal; 
        }

        private void cmd_Minimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}