using System;
using System.Configuration;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;
namespace IQCareServiceControl
{
    public partial class IQServiceControl : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IQServiceControl"/> class.
        /// </summary>
        public IQServiceControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The serviceconfig instance
        /// </summary>
        static IQServiceControl serviceControl;
        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <value>
        /// The service instance.
        /// </value>
        public static IQServiceControl ServiceInstance
        {
            get
            {
                if (serviceControl == null)
                {
                    serviceControl = new IQServiceControl();
                }
                return serviceControl;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnMinimise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMinimise_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
            base.Hide();
            Application.Exit();
        }
        /// <summary>
        /// Handles the Click event of the btnProperty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProperty_Click(object sender, EventArgs e)
        {
            IQServiceConfig.ServiceConfig serviceConfig ;                      
            serviceConfig = IQServiceConfig.ServiceConfig.ServiceInstance(ServiceName,ServicePath,ServiceConfigFileName);          
            serviceConfig.ShowDialog();
        }
        /// <summary>
        /// Gets the name of the service configuration file.
        /// </summary>
        /// <value>
        /// The name of the service configuration file.
        /// </value>
        string ServiceConfigFileName
        {
            get
            {

                return ConfigurationManager.AppSettings["ServiceConfigFileName"].ToString();
            }
        }
        /// <summary>
        /// Gets the service path.
        /// </summary>
        /// <value>
        /// The service path.
        /// </value>
        string ServicePath
        {
            get
            {

              //  ServiceController ctl = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName);

                if (ConfigurationManager.AppSettings["ApplicationPath"] != null)
                {
                    return ConfigurationManager.AppSettings["ApplicationPath"].ToString();
                }
                else
                {
                    return ConfigurationManager.AppSettings["DefaultPath"].ToString();
                }
            }
        }
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        string ServiceName
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceName"].ToString();
            }
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
               this.theNotifier.Text = "IQCare [" + this.theControl.Status.ToString() + "]";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnStop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.theControl.Stop();
                this.theNotifier.Text = this.ServiceName+" IQCare [" + this.theControl.Status.ToString() + "]";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Handles the Load event of the IQServiceControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void IQServiceControl_Load(object sender, EventArgs e)
        {

            System.Diagnostics.Process theProc = System.Diagnostics.Process.GetCurrentProcess();
            string Config = theProc.MainModule.FileName;
            Config = Config + ".config";        
            this.theControl.ServiceName = this.ServiceName;//"IQCareService3531";//  "NjungeService";// "IQCare Service";
            
           
        }      
        /// <summary>
        /// Handles the Resize event of the IQServiceControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void IQServiceControl_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == base.WindowState)
            {
                base.Hide();
            }
        }

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
        /// Handles the Tick event of the thetimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void thetimer_Tick(object sender, EventArgs e)
        {
            Icon theIcon;
            this.theControl.Refresh();
            if (this.theControl.Status == ServiceControllerStatus.Running)
            {
                this.btnStart.Enabled = false;
                this.stsImage.Image = Image.FromFile(Application.StartupPath.ToString() + @"\SrvRunning.jpg");
                this.Text = this.theControl.ServiceName + "[" + this.theControl.Status + "]";
                theIcon = new Icon(Application.StartupPath.ToString() + @"\IQRunning.ico");
                this.theNotifier.Icon = theIcon;
            }
            else
            {
                this.btnStart.Enabled = true;
                this.stsImage.Image = Image.FromFile(Application.StartupPath.ToString() + @"\SrvStopped.jpg");
                this.Text = this.theControl.ServiceName + " [" + this.theControl.Status + "]";
                theIcon = new Icon(Application.StartupPath.ToString() + @"\IQStopped.ico");
                this.theNotifier.Icon = theIcon;
            }
        }

        private void IQServiceControl_Shown(object sender, EventArgs e)
        {
         
        }

    }
}
