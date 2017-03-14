using System;
using System.Web;

namespace IQCare.Web.Laboratory
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Guid g = Guid.NewGuid();            
             Response.Redirect(string.Format("~/Laboratory/Request/FindLabOrder.aspx?key={0}", g.ToString()));
        }
        
    }
}