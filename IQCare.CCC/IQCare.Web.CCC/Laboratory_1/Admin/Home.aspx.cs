using System;

namespace IQCare.Web.Laboratory.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Home/Default.aspx");
        }
    }
}