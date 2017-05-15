using System;
using System.Web;
using System.Web.UI;
using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic.Triage;


namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientTriage : UserControl
    {
   

        IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");

        public int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPk"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return  Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]); }
        }
        protected string PatientGender
        {
            get { return Convert.ToString(Session["Gender"]); }
        }

        protected int PatientAge
        {
            get { return Convert.ToInt32(Session["Age"]); }
        }

        protected int PregnancyStatus
        {
            get
            {
                var pgStatus=new PatientPregnancyManager();
                return pgStatus.CheckIfPatientPregnancyExisists(PatientId);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
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