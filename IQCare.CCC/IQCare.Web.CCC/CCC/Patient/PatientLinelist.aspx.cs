using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientLinelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["qtrace"] = Request.QueryString["q"].ToString();
            string question = HttpContext.Current.Session["qtrace"].ToString();
            HttpContext.Current.Session["qfrom"] = Request.QueryString["qfrom"].ToString();
            HttpContext.Current.Session["qto"] = Request.QueryString["qto"].ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SetSelectedPatient(int patientId, int personId)
        {
            HttpContext.Current.Session["PatientPK"] = patientId;
            HttpContext.Current.Session["PersonId"] = personId;
            HttpContext.Current.Session["PatientTrace"] = "yes";
            HttpContext.Current.Session["PatientInformation"] = null;
            return "success";
        }
    }
}