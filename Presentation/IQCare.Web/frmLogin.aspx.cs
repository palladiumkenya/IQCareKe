using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using IQCare.Web.UILogic;
namespace IQCare.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginWeb : BasePage
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 03rd Aug 2006
        // Modification Date : 13th May 2008
        // Description       : Login Form
        //
        /// <summary>
        /// Init_s the form.
        /// </summary>
        //

        #region "User Functions"
        private void Init_Form()
        {
            Session.Timeout = int.Parse(ConfigurationManager.AppSettings.Get("SessionTimeOut"));
            Session.Add("AppUserId", "");
            Session.Add("AppUserName", "");
            Session.Add("EnrollFlag", "");
            Session.Add("IdentifierFlag", "");
            Session.Add("AppLocationId", "");
            Session.Add("AppLocation", "");
            Session.Add("AppCountryId", "");
            Session.Add("AppPosID", "");
            Session.Add("AppSatelliteId", "");
            Session.Add("GracePeriod", "");
            Session.Add("AppDateFormat", "");
            Session.Add("UserRight", "");
            Session.Add("BackupDrive", "");
            Session.Add("SystemId", "");
            Session.Add("ModuleId", "");
            Application.Add("AppCurrentDate", "");
            Session.Add("Program", "");
            Session.Add("AppCurrency", "");
            Session.Add("AppUserEmployeeId", "");
            Session.Add("CustomfrmDrug", "");
            Session.Add("CustomfrmLab", "");
            Session.Add("DosageFrequency", "");
            Session.Add("SystemQueue", "");
            //Session.Add("PatientMasterVisitID", "0");

            Session.Add("PersonId", "0");
            Session.Add("EncounterId","0");
            Session.Add("PatientMasterVisitId","0");
            Session.Add("ExistingRecordPatientMasterVisitID", "0");
            Session.Add("PatientId","0");
            Session.Add("EncounterStatusId",0);
            Session.Add("Gender", "");
            Session.Add("Age", 0);
            Session.Add("DateofBirth",0);
            ////////////////////////////////////////

            lblDate.Text = "";
            lblUserName.Text = "";
            lblLocation.Text = "";
            txtuname.Text = "";
            txtpassword.Text = "";
            imgLogin.ImageUrl = "";
            chkPref.Checked = true;
            GetApplicationParameters();
            //lblLocation.Text = Session["AppLocation"].ToString();
          
            txtuname.Focus();


            lblversion.Text = GblIQCare.VersionName; //AuthenticationManager.AppVersion;
            lblrelDate.Text = GblIQCare.ReleaseDate;//AuthenticationManager.ReleaseDate;

        }

        /// <summary>
        /// Binds the combo.
        /// </summary>
        private void BindCombo()
        {
            //   IUser UserManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            // DataTable theDT = UserManager.GetFacilityList();
            BindFunctions theBind = new BindFunctions();

            List<Entities.Administration.Facility> facilities = SystemSetting.CurrentSystem.Facilities;
            if (chkPref.Checked == true)
            {
                facilities = facilities.Where(f => f.Preffered == true).ToList();

                //DataView theDV = new DataView(theDT);
                // theDV.RowFilter = "Preferred = 1";
                //theDT = theDV.ToTable();

                if (ViewState["pwd"] != null)
                { txtpassword.Attributes["value"] = ViewState["pwd"].ToString(); }

            }
            else
            {

                txtpassword.Attributes["value"] = ViewState["pwd"].ToString();
                ViewState["pwd"] = null;
            }
            ViewState["pwd"] = null;

            ddLocation.DataSource = facilities;
            ddLocation.DataTextField = "Description";
            ddLocation.DataValueField = "Id";
            ddLocation.DataBind();
            if (facilities.Count > 1)
            {
                ddLocation.Items.Insert(0, new ListItem("Select", "0"));
            }



        }

        /// <summary>
        /// Gets the application parameters.
        /// </summary>
        private void GetApplicationParameters()
        {
            //IUser ApplicationManager;
            //  ApplicationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            SystemSettingResponseCode response = SystemSetting.GetFacilitySettings();
            DateTime theDTime = SystemSetting.SystemDate;

            ViewState["theCurrentDate"] = theDTime;
            lblDate.Text = theDTime.ToString("dd-MMM-yyyy");
            Application["AppCurrentDate"] = theDTime.ToString("dd-MMM-yyyy");
            Session["AppCurrentDateClass"] = theDTime.ToString("dd-MMM-yyyy");


            #region "Version Control"


            if (response == SystemSettingResponseCode.WrongVersion)
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
                script += "var ans=true;\n";
                script += "alert('You are using a Wrong Version of Application. Please contact Support staff.');\n";
                script += "if (ans==true)\n";
                script += "{\n";
                script += "window.close() - y;\n";
                script += "}\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
                btnLogin.Enabled = false;
                return;
            }

            if (response == SystemSettingResponseCode.NoFacilityDefined)
            {
                btnLogin.Text = "Go to Setup";
                txtuname.Text = "Admin";
                txtpassword.Text = "SetupPassword";
                txtpassword.ReadOnly = txtuname.ReadOnly = true;
                ddLocation.Items.Insert(0, new ListItem("Select", "0"));
                ddLocation.SelectedIndex = 0;
                Session["AppUserId"] = "";
                Session["AppUserName"] = "";
                btnLogin.CommandName = "Setup";
                Session["AppLocationId"] = "";
                Session["AppLocation"] = "";
                btnLogin.UseSubmitBehavior = true;
                btnLogin.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", "../AdminForms/frmAdmin_FacilityList.aspx");
                //  string theUrl = string.Format("{0}", "./AdminForms/frmAdmin_FacilityList.aspx");

                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                // Response.Redirect(theUrl,false);
                return;
            }
            if (response == SystemSettingResponseCode.Success)
            {
                SystemSetting setting = SystemSetting.CurrentSystem;
                btnLogin.CommandName = "Login";
                if (!string.IsNullOrEmpty(setting.DefaultFacility.LoginImage))
                {
                    imgLogin.ImageUrl = string.Format("images/{0}", setting.DefaultFacility.LoginImage);
                }
                else
                {
                    imgLogin.ImageUrl = "~/Images/Login.jpg";
                }
                Session["SystemId"] = Convert.ToInt32(setting.DefaultFacility.SystemId);
                BindCombo();
            }
            #endregion


           

        }
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="chngpwdflag">The chngpwdflag.</param>
        [WebMethod(EnableSession = true), ScriptMethod]
        public void ChangePassword(int chngpwdflag) //1 for mandatory change 0 for optional
        {
            string theUrl = string.Format("{0}", "./AdminForms/frmAdmin_ChangePassword.aspx");
            Response.Redirect(theUrl);

        }
        /// <summary>
        /// Validates the login.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLogin()
        {
            if (txtuname.Text.Trim() == "")
            {
                IQCareMsgBox.Show("BlankUserName", this);
                return false;
            }
            if (txtpassword.Text.Trim() == "")
            {
                IQCareMsgBox.Show("BlankPassword", this);
                return false;
            }
            if (Convert.ToInt32(ddLocation.SelectedValue) < 1)
            {
                IQCareMsgBox.Show("BlankLocation", this);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Updates the appointment.
        /// </summary>
        private void UpdateAppointment()
        {
            //*******Update appointment status priviously missed, missed, careended and met from pending*******//                
            IUser LoginManager;
            LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            int theAffectedRows = LoginManager.UpdateAppointmentStatus(Convert.ToString(Application["AppCurrentDate"]), Convert.ToInt16(Session["AppLocationId"]));
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Ajax.Utility.RegisterTypeForAjax(typeof(frmLogin));
                if (Page.IsPostBack != true)
                {
                    //Thread theThread = new Thread(GenerateCache);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(SystemSetting.GenerateCache));
                    // theThread.Start();
                    Init_Form();

                }
                // Response.Redirect("Reports/frmIQToolsQuery.aspx");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        string RedirectURL
        {
            get
            {
                string landingPage = ConfigurationManager.AppSettings.Get("LandingPage");
                if (landingPage != null)
                    return landingPage;
                else return "frmFacilityHome.aspx";
            }
        }
        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.CommandName == "Setup")
            {
                 string theUrl = string.Format("{0}", "./AdminForms/frmAdmin_FacilityList.aspx");

                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl,false);
                return;
            }
            
            if (ValidateLogin() == false)
            {
                Init_Form();
                return;
            }

            // IUser LoginManager;
            string redirectURL = this.RedirectURL;
           

            try
            {
                //LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
                //DataSet theDS = LoginManager.GetUserCredentials(txtuname.Text.Trim(), Convert.ToInt32(ddLocation.SelectedValue), Convert.ToInt32(Session["SystemId"]));
                LoginResponseCode response = CurrentSession.Login(this, txtuname.Text.Trim(), Convert.ToInt32(ddLocation.SelectedValue), Convert.ToInt32(Session["SystemId"]), txtpassword.Text.Trim());
                if (response == LoginResponseCode.Success)
                {
                    DataSet theDS = CurrentSession.Current.UserDetail;
                    Session["AppUserId"] = Convert.ToString(theDS.Tables[0].Rows[0]["UserId"]);
                    Session["AppUserName"] = Convert.ToString(theDS.Tables[0].Rows[0]["UserFirstName"]) + " " + Convert.ToString(theDS.Tables[0].Rows[0]["UserLastName"]);
                    Session["EnrollFlag"] = theDS.Tables[1].Rows[0]["EnrollmentFlag"].ToString();
                    Session["CareEndFlag"] = theDS.Tables[1].Rows[0]["CareEndFlag"].ToString();
                    Session["IdentifierFlag"] = theDS.Tables[1].Rows[0]["IdentifierFlag"].ToString();
                    Session["UserRight"] = theDS.Tables[1];
                    DataTable theDT = theDS.Tables[2];
                    Session["AppLocationId"] = theDT.Rows[0]["FacilityID"].ToString();
                    Session["AppLocation"] = theDT.Rows[0]["FacilityName"].ToString();
                    Session["AppCountryId"] = theDT.Rows[0]["CountryID"].ToString();
                    Session["AppPosID"] = theDT.Rows[0]["PosID"].ToString();
                    Session["AppSatelliteId"] = theDT.Rows[0]["SatelliteID"].ToString();
                    Session["GracePeriod"] = theDT.Rows[0]["AppGracePeriod"].ToString();
                    Session["AppDateFormat"] = theDT.Rows[0]["DateFormat"].ToString();
                    Session["BackupDrive"] = theDT.Rows[0]["BackupDrive"].ToString();
                    Session["SystemId"] = theDT.Rows[0]["SystemId"].ToString();
                    Session["AppCurrency"] = theDT.Rows[0]["Currency"].ToString();
                    Session["AppUserEmployeeId"] = theDS.Tables[0].Rows[0]["EmployeeId"].ToString();
                    Session["DosageFrequency"] = theDT.Rows[0]["Frequency"].ToString();
                    Session["PatientTrace"] = "";
                    Session["SystemQueue"] = theDT.Rows[0]["SystemQueue"].ToString();

                    //Session["AppSystemId"] = theDT.Rows[0]["SystemId"].ToString();

                    #region "ModuleId"
                    Session["AppModule"] = theDS.Tables[3];
                    DataView theSCMDV = new DataView(theDS.Tables[3]);
                    theSCMDV.RowFilter = "ModuleId=201";
                    if (theSCMDV.Count > 0)
                        Session["SCMModule"] = theSCMDV[0]["ModuleName"];

                    DataView theSamePointDispenseDV = new DataView(theDS.Tables[3]);
                    theSamePointDispenseDV.RowFilter = "ModuleId=30";
                    if (theSamePointDispenseDV.Count > 0)
                        Session["SCMSamePointDispense"] = theSamePointDispenseDV[0]["ModuleName"];


                    Session["BillingON"] = theDS.Tables[3].Select("ModuleName = 'Billing'").Length > 0;
                    Session["AdmissionWardsON"] = theDS.Tables[3].Select("ModuleName = 'Ward Admission'").Length > 0;
                    #endregion
                    IQWebUtils theIQUtils = new IQWebUtils();
                    //theIQUtils.CreateSessionObject(Session.SessionID); 
                    Session["Paperless"] = theDT.Rows[0]["Paperless"].ToString();
                    Session["Program"] = "";
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    
                   // FormsAuthentication.SetAuthCookie(txtuname.Text.Trim(), false);
                    Response.Redirect(redirectURL, false);
                }
                else if (response == LoginResponseCode.PasswordNotMatch)
                {
                    if ((Request.Browser.Cookies))
                    {
                        HttpCookie theCookie = Request.Cookies[txtuname.Text];
                        if (theCookie == null)
                        {
                            HttpCookie theNCookie = new HttpCookie(txtuname.Text);
                            theNCookie.Value = txtuname.Text + ",1";
                            DateTime theNewDTTime = Convert.ToDateTime(ViewState["theCurrentDate"]).AddMinutes(5);
                            theNCookie.Expires = theNewDTTime;
                            Response.Cookies.Add(theNCookie);
                        }

                        else
                        {
                            string[] theVal = (theCookie.Value.ToString()).Split(',');
                            if (Convert.ToInt32(theVal[1]) >= 3 && theCookie.Name == txtuname.Text)
                            {
                                MsgBuilder theBuilder = new MsgBuilder();
                                theBuilder.DataElements["MessageText"] = "User Account Locked. Try again after 5 Mins.";
                                IQCareMsgBox.Show("#C1", theBuilder, this);
                                return;
                            }
                            else
                            {
                                theVal[1] = (Convert.ToInt32(theVal[1]) + 1).ToString();
                                theCookie.Value = txtuname.Text + "," + theVal[1];
                                DateTime theAddNewDTTime = Convert.ToDateTime(ViewState["theCurrentDate"]).AddMinutes(5);
                                theCookie.Expires = theAddNewDTTime;
                                Response.Cookies.Add(theCookie);
                            }
                        }
                    }
                    IQCareMsgBox.Show("PasswordNotMatch", this);
                    Init_Form();
                    return;
                }
                else if (response == LoginResponseCode.PasswordExpired)
                {
                    string theUrl = string.Format("{0}", "./AdminForms/frmAdmin_ChangePassword.aspx");
                    string msgString = "Your Password has expired. Please Change it now.\\n";
                    string script = "<script language = 'javascript' defer ='defer' id = 'changePwdfunction2'>\n";
                    script += "alert('" + msgString + "');\n";
                    string url = Request.RawUrl.ToString();
                    Application["PrvFrm"] = url;
                    Session["MandatoryChange"] = "1";
                    script += "window.location.href='" + theUrl + "'\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "changePwdfunction2", script);
                }
                else if (response == LoginResponseCode.InvalidLogin)
                {
                    IQCareMsgBox.Show("InvalidLogin", this);
                    Init_Form();
                    return;
                }
                
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                //LoginManager = null;
            }
        }
        /// <summary>
        /// Handles the CheckedChanged event of the chkPref control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void chkPref_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["pwd"] = txtpassword.Text;
            BindCombo();
        }
        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Calls the help.
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string CallHelp()
        {

            string theHlpFileNm = HttpContext.Current.Server.MapPath("//IQCareHelp//IQCareARUserManualSep2010.chm");
            System.Windows.Forms.Control theParent = new System.Windows.Forms.Control();
            System.Windows.Forms.Help.ShowHelp(theParent, theHlpFileNm);
            return theHlpFileNm;
        }
        /// <summary>
        /// Handles the AsyncPostBackError event of the ActionScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        protected void ActionScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            this.mst.AsyncPostBackErrorMessage = message;
        }
        /// <summary>
        /// Handles the Click event of the lnkBtnHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected string lnkBtnHelp_Click(object sender, EventArgs e)
        {
            string theHlpFileNm = Server.MapPath("//IQCareHelp//IQCareARUserManualSep2010.chm");
            //System.Windows.Forms.Control theParent = new System.Windows.Forms.Control();
            //System.Windows.Forms.Help.ShowHelp(theParent, theHlpFileNm);
            return theHlpFileNm;
            //IQWebUtils theUtils = new IQWebUtils();
            //theUtils.ShowFile(theHlpFileNm, Response); 
        }
    }
}