using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Patient;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic.Visit;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientTriage : System.Web.UI.UserControl
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
                PatientMasterVisitId = _visitManager.AddPatientmasterVisit(visit);
            }
        }
    }
}