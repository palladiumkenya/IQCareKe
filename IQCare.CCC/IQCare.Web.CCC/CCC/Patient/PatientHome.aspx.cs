using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = HttpContext.Current.Session["PatientId"];
            Session["PatientMasterVisitId"] = HttpContext.Current.Session["PatientMasterVisitId"];
        }
    }
}