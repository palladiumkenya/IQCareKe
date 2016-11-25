using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IQCare.Web.Billing
{
    public partial class frmCustomReportPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if(IsPostBack) return;
            gridResult.DataSource = (DataTable)Session["CBillingReport"];
            gridResult.DataBind();

            lblUserName.Text ="Printed by: " + Session["AppUserName"];
            lblFacilityName.Text = Session["AppLocation"].ToString();
            lblReportName.Text = Session["ReportName"].ToString();
            lblReportParams.Text = Session["ReportParameters"].ToString();

            Page.ClientScript.RegisterStartupScript(HttpContext.Current.GetType(), "onLoadCall", "WindowPrint();", true);

    

        }
        
        
    }
}