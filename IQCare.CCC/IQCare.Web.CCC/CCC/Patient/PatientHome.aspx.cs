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
            int patientId = Convert.ToInt32(Request.QueryString["patient"]);
            if (patientId != null && patientId != 0)
            {
                Session["PatientId"] = patientId;
            }
            else
            {
                Session["PatientId"] = HttpContext.Current.Session["PatientId"];
                Session["PatientMasterVisitId"] = HttpContext.Current.Session["PatientMasterVisitId"];
            }
        }
    }
}