using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC
{
    public partial class Home : System.Web.UI.Page
    {
        public int AppLocationId;
        protected void Page_Load(object sender, EventArgs e)
        {
            AppLocationId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
        }
    }
}