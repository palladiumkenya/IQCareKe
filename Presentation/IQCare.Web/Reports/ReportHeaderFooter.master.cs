using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Application.Presentation;
using Interface.Security;
using Application.Common;
namespace IQCare.Web.Reports
{

    public partial class ReportHeaderFooter : System.Web.UI.MasterPage
    {
        #region "User Functions"
        private void AuthenticateRights()
        {
            AuthenticationManager Authentication = new AuthenticationManager();

            if (Authentication.HasFeatureRight(ApplicationAccess.FacilitySetup, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminFacility.Visible = false;
            }
            //if (Authentication.HasFeatureRight(ApplicationAccess.CustomiseDropDown, (DataTable)Session["UserRight"]) == false)
            //{
            //    mnuAdminCustom.Visible = false;
            //}
            if (Authentication.HasFeatureRight(ApplicationAccess.UserAdministration, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminUser.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.UserGroupAdministration, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminUserGroup.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.DeletePatient, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminDeletePatient.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.CustomReports, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminCustomReport.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.FacilityReports, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminFacilityReport.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.DonorReports, (DataTable)Session["UserRight"]) == false)
            {
                mnuAdminDonorReport.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, (DataTable)Session["UserRight"]) == false)
            {
                mnuSchedular.Visible = false;
            }

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AppLocation"] != null)
                {
                    //lblTitle.InnerText = "International Quality Care Patient Management and Monitoring System [" + Session["AppLocation"].ToString() + "]";

                    lblUserName.Text = Session["AppUserName"].ToString();
                    lblLocation.Text = Session["AppLocation"].ToString();

                    IIQCareSystem AdminManager;
                    AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                    lblDate.Text = AdminManager.SystemDate().ToString(Session["AppDateFormat"].ToString());

                    //####### Delete Patient #############
                    string theUrl;
                    theUrl = string.Format("{0}?mnuClicked={1}", "../frmFindAddPatient.aspx", "DeletePatient");
                    mnuAdminDeletePatient.HRef = theUrl;

                    //theUrl = string.Format("{0}?mnuClicked={1}", "../frmFindAddPatient.aspx", "DeletePatient");
                    //mnuAdminDeletePatient.HRef = theUrl;

                    lblversion.Text = GblIQCare.AppVersion;//AuthenticationManager.AppVersion;
                    lblrelDate.Text = GblIQCare.ReleaseDate;// AuthenticationManager.ReleaseDate;

                    AuthenticateRights();
                }
            }
        }
    }
}