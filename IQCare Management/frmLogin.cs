using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using Interface.Security;


namespace IQCare.Management
    {
        /// <summary>
        /// Login form
        /// </summary>
    public partial class frmLogin : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmLogin"/> class.
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks the version.
        /// </summary>
      private void CheckVersion()
        {
            IUser objLoginManager;
            objLoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataSet theDS = objLoginManager.GetFacilitySettings();
            if (theDS.Tables[1].Rows[0]["AppVer"].ToString() != GblIQCare.AppVersion || ((DateTime)theDS.Tables[1].Rows[0]["RelDate"]).ToString("dd-MMM-yyyy") != GblIQCare.ReleaseDate)
            {
                IQCareWindowMsgBox.ShowWindow("CheckVersion", this);
                btnLogin.Enabled = false;
                return;
                
            }

        }
      /// <summary>
      /// Handles the Load event of the frmLogin control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
           
            lblStatus.Text = GblIQCare.VersionName+ "       Release Date: " + GblIQCare.ReleaseDate;
            lblCopyRight.Text = "©2016 Palladium";
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end 
            txtLoginName.Focus();
            //ddLocation.Items.Add("<--Select-->");
            CheckVersion();
            BindCombo();
        }

        /// <summary>
        /// writes the code for user login  and enter in to main form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin() == false)
            {
                return;
            }

            try
            {
                IUser objLoginManager;
                objLoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
                DataSet theDs = objLoginManager.GetUserCredentials(txtLoginName.Text.Trim(), Convert.ToInt32(ddLocation.SelectedValue), Convert.ToInt32(GblIQCare.SystemId));
                if (theDs.Tables.Count > 0)
                {
                    Utility theUtil = new Utility();
                    if (theDs.Tables[0].Rows.Count > 0)
                    {
                        if (theUtil.Decrypt(Convert.ToString(theDs.Tables[0].Rows[0]["Password"])) != txtPassword.Text.Trim())
                        {

                            IQCareWindowMsgBox.ShowWindow("PasswordNotMatch", this);
                            txtPassword.Focus();
                            return;
                        }
                    }
                    else
                    {
                         IQCareWindowMsgBox.ShowWindow("InvalidLogin", this);
                         txtLoginName.Focus();
                         return;

                    }
                    GblIQCare.AppUserId = Convert.ToInt32(theDs.Tables[0].Rows[0]["UserId"]);
                    GblIQCare.AppUserName = (theDs.Tables[0].Rows[0]["UserFirstName"] + "" + theDs.Tables[0].Rows[0]["UserLastName"]).ToString();
                    GblIQCare.AppUName = theDs.Tables[0].Rows[0]["UserName"].ToString();
                    GblIQCare.EnrollFlag = Convert.ToInt32(theDs.Tables[1].Rows[0]["EnrollmentFlag"]);
                    GblIQCare.dtUserRight = theDs.Tables[1].Copy();
                    DataTable theDT = theDs.Tables[2];
                    GblIQCare.SystemId = Convert.ToInt32(theDT.Rows[0]["SystemId"].ToString());
                    GblIQCare.AppCountryId = theDT.Rows[0]["Currency"].ToString();
                    GblIQCare.AppDateFormat = theDT.Rows[0]["DateFormat"].ToString();
                    GblIQCare.AppGracePeriod = theDT.Rows[0]["AppGracePeriod"].ToString();
                    GblIQCare.AppLocationId = Convert.ToInt32(theDT.Rows[0]["FacilityID"].ToString());
                    GblIQCare.AppLocation = theDT.Rows[0]["FacilityName"].ToString();
                    GblIQCare.AppPosID = theDT.Rows[0]["PosID"].ToString();
                    GblIQCare.AppSatelliteId = theDT.Rows[0]["SatelliteID"].ToString();
                    GblIQCare.BackupDrive = theDT.Rows[0]["BackupDrive"].ToString();
                    GblIQCare.CurrentDate = String.Format("{0:dd-MMM-yyyy}", theDs.Tables[4].Rows[0]["CurrentDate"]);
                    GblIQCare.dtModules = theDs.Tables[3];
                    
                    objLoginManager = null;

                    frmMain objForm = new frmMain();
                    objForm.Show();
                  

                    this.Hide();
            }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("InvalidLogin", this);
                    txtLoginName.Focus();
                    return;
                }
            }

            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Binds the combo.
        /// </summary>
        private void BindCombo()
        {

            Interface.Security.IUser UserManager;
            UserManager = (Interface.Security.IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            DataTable theDT = UserManager.GetFacilityList();
            BindFunctions theBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            if (chkPref.Checked == true)
            {                
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "Preferred = 1";
                theDT = theDV.ToTable();
                if (GblIQCare.pwd != null)
                {
                    txtPassword.Text = GblIQCare.pwd.ToString();
                    GblIQCare.pwd = null;
                }    
            }
            if (theDT.Rows.Count == 1)
            {
                ddLocation.DataSource = theDT;
                ddLocation.DisplayMember = "FacilityName";
                ddLocation.ValueMember = "FacilityId";
                ddLocation.DataSource = theDT;
            }
            //else if (chkPref.Checked == true)
            //{

            //    DataView theDV = new DataView(theDT);
            //    theDV.RowFilter = "Preferred = 1";
            //    ddLocation.DataSource = theDV;
            //    DataTable DT = new DataTable();
            //    DT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            //    theBind.Win_BindCombo(ddLocation, DT, "FacilityName", "FacilityId");
            //    if ( GblIQCare.pwd != null)
            //    {
            //        txtPassword.Text = GblIQCare.pwd.ToString() ;
            //        GblIQCare.pwd = null;
            //    }     
            //}
            else
            {

                theBind.Win_BindCombo(ddLocation, theDT, "FacilityName", "FacilityId");
                GblIQCare.pwd = null;
            }
            GblIQCare.pwd = null; 
        
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Validates the login.
        /// </summary>
        /// <returns></returns>
        
        private bool ValidateLogin()
        {
      
                if (txtLoginName.Text.Trim() == "")
                {
                 IQCareWindowMsgBox.ShowWindow("BlankUserName", this);
                
                txtLoginName.Focus();
                return false;   
                
                }
                if (txtPassword.Text == "")
                {
                    
                    IQCareWindowMsgBox.ShowWindow("BlankPassword", this); 
                    txtPassword.Focus();
                    return false;
                }
                if (Convert.ToInt32(ddLocation.SelectedValue) < 1)
                {
                    IQCareWindowMsgBox.ShowWindow("BlankLocation", this);
                    return false;
                }

                return true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkPref control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkPref_CheckedChanged(object sender, EventArgs e)
        {
            GblIQCare.pwd = txtPassword.Text.ToString();
            BindCombo();
        }

        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
    }
