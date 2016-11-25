using System;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    /// <summary>
    ///
    /// </summary>
    public partial class Home : BasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Master.ExecutePatientLevel = false;
            if (CurrentSession.Current == null || !CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.BillingModule))
            {
                string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                CurrentSession.Logout();
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);
            }
            CurrentSession.Current.ResetCurrentPatient();
            if (!IsPostBack)
            {
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Home | Dashboard";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
                Session.Remove("PayControl");
                LoadMenu();
            }
        }
        private void LoadDashBoard()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("IndicatorName", typeof(String));
            theDT.Columns.Add("IndicatorDescription", typeof(String));
            theDT.Columns.Add("IndicatorValue", typeof(Double));

            theDT.Rows.Add(new object[]
            {
            "Unbilled Items",
            "Total value of unbilled items",
            7800.0           
            });
            theDT.Rows.Add(new object[]
            {
            "Outstanding invoices",
            "Total value of outstanding invoices",
            9800.00
            });

            //repeaterDashBoard.DataSource = theDT;
            //repeaterDashBoard.DataBind();
        }
        /// <summary>
        /// Loads the menu.
        /// </summary>
        private void LoadMenu()
        {
            Color[] colors = { Color.Firebrick, Color.BlueViolet, Color.DarkCyan, Color.Chocolate, Color.DarkSlateBlue, Color.DarkMagenta, Color.CornflowerBlue };
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ResourceName", typeof(String));             
            theDT.Columns.Add("ResourceUrl", typeof(String));
            theDT.Columns.Add("ResourceDescription", typeof(String));
            theDT.Columns.Add("ResourceColor", typeof(String));
            theDT.Columns.Add("IconFont", typeof(String));
            theDT.Columns.Add("PermReference", typeof(String));
            theDT.Rows.Add(new object[]
            {
            "Patient Bill",
            "frmBillingFindAddBill.aspx",
            "Patient Billing",
            "firebrick"   ,
            "fa fa-money fa-3x",
            ApplicationAccess.BillingFeature.BillingModule
            });
            theDT.Rows.Add(new object[]
            {
            "Billing Reports",
            "frmBillingReportPage.aspx",
            "Billing Reports",
            "blueviolet"      ,
            "fa fa-line-chart fa-3x",
            ApplicationAccess.BillingFeature.Report
            });

            theDT.Rows.Add(new object[]
            {
            "Reversals Approval",
            "frmBilling_ReversalApproval.aspx",
            "Billing Reports",
            "darkcyan"   ,
            "fa fa-exchange fa-3x",
               ApplicationAccess.BillingFeature.ApproveReversals
            });

            theDT.Rows.Add(new object[]
            {
            "Quick Panel",
            "frmFindPatient.aspx?FormName=QuickPanel&mnuClicked=QuickPanel",
            "Quick Panel",
            "chocolate"    ,
            "fa fa-arrows-alt fa-3x",
               ApplicationAccess.BillingFeature.QuickPanel
            });

            theDT.Rows.Add(new object[]
            {
            "Payment Methods",
            "frmBillingAdmin_PaymentType.aspx",
            "Payment Methods",
            "darkslateblue"   ,
            "fa fa-cc-visa fa-3x",
            ApplicationAccess.BillingFeature.Configuration
            });

            theDT.Rows.Add(new object[]
            {
            "Price List",
            "frmBilling_PriceList.aspx",
            "Price List",
            "darkmagenta" ,
            "fa fa-list fa-3x",
            ApplicationAccess.BillingFeature.Configuration
            });
            theDT.Rows.Add(new object[]
            {
            "Manage Discounts",
            "frmBilling_Discount.aspx",
            "Discounts",
            "cornflowerblue"   ,
            "fa fa-arrow-down fa-3x",
            ApplicationAccess.BillingFeature.Configuration
            });

            theDT.Rows.Add(new object[]
            {
            "Credit Knock Off",
            "BillKnockOff.aspx",
            "Credit Knock Off",
            "firebrick"    ,
            "fa fa-anchor fa-3x",
             ApplicationAccess.BillingFeature.CreditKnockOff
            });

            repeaterNavi.DataSource = theDT;
            repeaterNavi.DataBind();
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.SetStyle();
        }

        /// <summary>
        /// Formats the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected string formatUrl(object url)
        {
            return String.Format(@"./{0}", url.ToString());
        }

        protected string showIcon(string iconfont)
        {
            return String.Format(@"<i class='{0} iq-tile'></i>", iconfont);
        }


        /// <summary>
        /// Sets the style.
        /// </summary>
        private void SetStyle()
        {
            HtmlGenericControl facilityBanner = (Master.FindControl("facilityBanner") as HtmlGenericControl);
            if (facilityBanner != null) facilityBanner.Style.Add("display", "inline");

            HtmlGenericControl patientBanner = (Master.FindControl("patientBanner") as HtmlGenericControl);
            if (patientBanner != null) patientBanner.Style.Add("display", "none");
            HtmlGenericControl username1 = (Master.FindControl("username1") as HtmlGenericControl);
            if (username1 != null)
                username1.Attributes["class"] = "usernameLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl currentdate1 = (Master.FindControl("currentdate1") as HtmlGenericControl);
            if (currentdate1 != null) currentdate1.Attributes["class"] = "currentdateLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl facilityName = (Master.FindControl("facilityName") as HtmlGenericControl);
            if (facilityName != null) facilityName.Attributes["class"] = "facilityLevel1"; //Style.Add("display", "inline");
            //userNameLevel2.Style.Add("display", "none");
            //currentDateLevel2.Style.Add("display", "none");
            HtmlGenericControl imageFlipLevel2 = (Master.FindControl("imageFlipLevel2") as HtmlGenericControl);
            if (imageFlipLevel2 != null) imageFlipLevel2.Style.Add("display", "none");
            //facilityLevel2.Style.Add("display", "none");
            HtmlGenericControl level2Navigation = (Master.FindControl("level2Navigation") as HtmlGenericControl);
            if (level2Navigation != null) level2Navigation.Style.Add("display", "none");
        }

        protected void tabBillHome_ActiveTabChanged(object sender, EventArgs e)
        {
            //this.GetContextTabData(tabBillHome.ActiveTabIndex);
        }

        void GetContextTabData(int TabIndex) //This is Page 
        {
            //  string text = "text" + TabIndex.ToString();

            if (TabIndex == 0)
            {
                this.LoadMenu();

            }
            else if (TabIndex == 1)
            {
                this.LoadDashBoard();
            }
           
            //return text;
        }
        AuthenticationManager authMgr = new AuthenticationManager();

        //protected void repeaterDashBoard_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
            
        //}

        protected string validateResource(object permRef, object url)
        {
            if (authMgr.HasFeatureRight(permRef.ToString(), (DataTable)Session["UserRight"]) == true)
            {
                return String.Format(@"javascript:window.location ='{0}'; return false;", url.ToString());
            }
            return "javascript:return false;";
        }
    }
}