using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientConsent : System.Web.UI.Page
    {
        public int PatientId;
        public int PatientMasterVisitId;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSessionDetails();
        }
        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
        }
    }
}