using System;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.Enrollment
{
    public partial class ServiceEnrollment : System.Web.UI.Page
    {
        public string patType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientType"] !=null && Session["PatientType"].ToString() != null)
            {
                var patientType = int.Parse(Session["PatientType"].ToString());
                patType = LookupLogic.GetLookupNameById(patientType);
            }
        }
    }
}