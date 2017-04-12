using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Encounter
{
    public partial class VitalSigns : System.Web.UI.Page
    {
        protected string Gender
        {
            get { return Session["Gender"].ToString(); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}