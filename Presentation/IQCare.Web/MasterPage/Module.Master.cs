using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using IQCare.Web.UILogic;

namespace IQCare.Web.MasterPage
{
    public partial class Module : System.Web.UI.MasterPage
    {
        private String strPathAndQuery;
        private String strUrl;
        public ScriptManager PageScriptManager
        {
            get
            {
                return this.masterScriptManager;
            }
        }

        public string SessionExpireDestinationUrl
        {
            get { return Page.ResolveUrl("~/frmLogin.aspx"); }
        }

        public int SessionLengthMinutes
        {
            get { return 60; }
        }
        public string CurrentModuleName
        {

            set
            {
                levelOneNavigationUserControl1.CurrentModule = value;
            }
            get
            {
                return levelOneNavigationUserControl1.CurrentModule;
            }
        }
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmLogOff.aspx");
        }

        protected void lnkPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminForms/frmAdmin_ChangePassword.aspx");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //string path = HttpContext.Current.Request.Url.AbsolutePath;
            strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            this.pageHead.Controls.Add(new LiteralControl(String.Format("<meta http-equiv='refresh' content='{0};url={1}' />", SessionLengthMinutes * 60, SessionExpireDestinationUrl)));
        }
        protected string ShowPatientInfo
        {
            get
            {
                return CurrentSession.Current.CurrentPatient != null ? "" : "none";
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            if (Session["AppLocation"] == null)
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx");
            }
            if ((Session["AppUserID"] == null && Session["AppUserID"].ToString() == "") || CurrentSession.Current == null)
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx");
            }
            lblTitle.Text = "International Quality Care Patient Management and Monitoring System [" + Session["AppLocation"].ToString() + "]";
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            //string pageName = this.Page.ToString();
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string pageName = fileinfo.Name;

            if (Session["PatientID"] != null)
            {
                if (int.Parse(Session["PatientID"].ToString()) > 0)
                {
                    //VY added 2014-10-14 for changing level one navigation Menu depending on whether patient has been selected or not
                    if (Session["TechnicalAreaId"] != null)
                    {
                        MenuItem facilityHome = (levelOneNavigationUserControl1.FindControl("mainMenu") as Menu).FindItem("Facility Home");
                        facilityHome.Text = "<i class='fa fa-search-plus fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong> Find/Add Patient</strong></span>";
                        facilityHome.NavigateUrl = String.Format("~/Patient/FindAdd.aspx?srvNm={0}&mod={1}", Session["TechnicalAreaName"], Session["TechnicalAreaId"]);
                        MenuItem facilityStats = (levelOneNavigationUserControl1.FindControl("mainMenu") as Menu).FindItem("Facility Statistics");
                        facilityStats.Text = "<i class='fa fa-cubes fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong>Select Service</strong></span>";
                        facilityStats.NavigateUrl = "~/frmFacilityHome.aspx";
                    }
                    
                }
                else
                {
                   
                    MenuItem facilityHome = (levelOneNavigationUserControl1.FindControl("mainMenu") as Menu).FindItem("Facility Home");
                    facilityHome.Text = "<i class='fa fa-cubes fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong>Select Service</strong></span>";
                    facilityHome.NavigateUrl = "~/frmFacilityHome.aspx";
                    MenuItem facilityStats = (levelOneNavigationUserControl1.FindControl("mainMenu") as Menu).FindItem("Facility Statistics");
                    facilityStats.Text = "<i class='fa fa-line-chart fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong>Facility Statistics</strong></span>";
                    facilityStats.NavigateUrl = "~/frmFacilityStatistics.aspx";
                }
            }
            

            if (Session["AppUserName"] != null)
            {
                lblUserName.Text = Session["AppUserName"].ToString();
            }
            CurrentSession session = CurrentSession.Current;
            //if (Session["AppLocation"] != null)
            //{
                lblLocation.Text = session.Facility.Name.ToString();
           // }
            //CurrentSession session = CurrentSession.Current;

            this.CurrentModuleName = session.CurrentServiceArea.Name;

            if (session.CurrentPatient != null)
            {
                lblpatientname.Text = session.CurrentPatient.FullName;
                lblSex.Text = session.CurrentPatient.Sex;
                lblAge.Text = session.CurrentPatient.Age.ToString() + " Years";
                lblDOB.Text = session.CurrentPatient.DateOfBirth.ToString("dd-MMM-yyyy");
                lblIQnumber.Text = session.CurrentPatient.UniqueFacilityId;
            }

            DateTime sysDate=SystemSetting.SystemDate;
            if (session.Facility.DateFormat.ToString() != "")
            {
                lblDate.Text = sysDate.ToString(session.Facility.DateFormat);
                    
            }
            else
            {
                lblDate.Text = sysDate.ToString("dd-MMM-yyyy");
            }
            if (Session.Count == 0)
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx");
            }
            if (Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx");
            }

            lblversion.Text = GblIQCare.VersionName;// AuthenticationManager.AppVersion;
            lblrelDate.Text = GblIQCare.ReleaseDate;//AuthenticationManager.ReleaseDate;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
    }
}