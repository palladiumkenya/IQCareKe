using System;
using System.Web;
using System.Web.UI;
using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;


namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientTriage : UserControl
    {
        public int PatientId;
        public int PatientMasterVisitId;
        IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
             PatientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
             PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            if (PatientMasterVisitId == 0)
            {
                PatientMasterVisit visit = new PatientMasterVisit()
                {
                    PatientId = PatientId,
                    Active = true,
                };
            }
        }
    }
}