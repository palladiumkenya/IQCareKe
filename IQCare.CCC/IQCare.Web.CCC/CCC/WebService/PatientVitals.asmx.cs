using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.WebService
{
    public class PatientDetailsVitals
    {
        public decimal Temperature { get; set; }
        public decimal RespiratoryRate { get; set; }
        public decimal HeartRate { get; set; }
        public int Bpdiastolic { get; set; }
        public int BpSystolic { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Muac { get; set; }
        public decimal SpO2 { get; set; }
        public decimal BMI { get; set; }
        public decimal HeadCircumference { get; set; }
        public int Month { get; set; }
    }
    /// <summary>
    /// Summary description for PatientVitals
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientVitals : System.Web.Services.WebService
    {
        IPatientVitals _vitals =
         (IPatientVitals)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientVitals, BusinessProcess.CCC");

        private int _patientId;
     

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public List<PatientDetailsVitals> GetVitals()
        {
            _patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);

            List<PatientDetailsVitals> patientDetailsVitalses = new List<PatientDetailsVitals>();
            //List<PatientVital> listVitals = _vitals.GetCurrentPatientVital(_patientId);
            List<PatientVital> listVitals = _vitals.GetAllPatientVitals(_patientId);

            if (listVitals != null )
            {
                foreach (var item in listVitals)
                {
                    PatientDetailsVitals vitals = new PatientDetailsVitals();
                    vitals.Height = item.Height;
                    vitals.Weight = item.Weight;
                    vitals.BMI = item.BMI;
                    vitals.Month = item.CreateDate.Month;
                    
                    patientDetailsVitalses.Add(vitals);
                }
            }
            return patientDetailsVitalses;
        }

        [WebMethod(EnableSession = true)]
        public PatientVital GetPatientVitalsByMasterVisitId()
        {
            var patientVitalsManager = new PatientVitalsManager();
            int patient = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["PatientMasterVisitId"].ToString());
            return patientVitalsManager.GetPatientVitalsByMasterVisitId(patient, patientMasterVisitId);
        }

        [WebMethod(EnableSession = true)]
        public PatientVital GetByPatientId()
        {
            var patientVitalsManager = new PatientVitalsManager();
            int patient = Convert.ToInt32(Session["PatientPK"].ToString());
            return patientVitalsManager.GetByPatientId(patient);
        }


        [WebMethod(EnableSession = true)]
        public string GetCurrentPatientVitalsByPatientId()
        {
            var patientVitalsManager = new PatientVitalsManager();
            int patient = Convert.ToInt32(Session["PatientPK"].ToString());
           var vitals=  patientVitalsManager.GetByPatientId(patient);
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(vitals);
        }
    }
}
