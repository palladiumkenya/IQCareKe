using System;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;



namespace IQCare.Web.CCC.WebService
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LabService : System.Web.Services.WebService
    {

        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");

        private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        private int facilityID { get; set; }
        private string Msg { get; set; }
        private int Result { get; set; }



        [WebMethod(EnableSession = true)]
        public string AddLabOrder(int patient_ID, int patientMasterVisitId, string patientLabOrder)
        {
            if (patientMasterVisitId == 0)
            {
                PatientMasterVisit visit = new PatientMasterVisit()
                {
                    PatientId = patient_ID,
                    Start = DateTime.Now,
                    Active = true,
                };
                patientMasterVisitId = _visitManager.AddPatientmasterVisit(visit);
            }
            // Get Facility ID service
            LookupFacility facility = _lookupManager.GetFacility();
            facilityID = facility.FacilityID;

            try
            {
                //conversion error                 
                // int patient_ID = 18;
                var labOrder = new PatientLabOrderManager();
                Result = labOrder.savePatientLabOrder(patient_ID, facilityID, patientMasterVisitId, patientLabOrder);
                if (Result > 0)
                {
                    Msg = "Patient Lab Order Recorded Successfully .";
                }

            }
            catch (Exception e)
            {
                Msg = "Error Message: " + e.Message + ' ' + " Exception: " + e.InnerException;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string GetLookupPreviousLabsList()
        {


            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);

            string jsonObject = LookupLogic.GetLookupPreviousLabsListJson(patientId);

            return jsonObject;
        }
        [WebMethod(EnableSession = true)]
        public string GetLookupPendingLabsList(string patient_ID)
        {


            int patientId = Convert.ToInt32(patient_ID);
            string jsonObject = LookupLogic.GetLookupPendingLabsListJson(patientId);

            return jsonObject;
        }


        [WebMethod(EnableSession = true)]
        public string GetvlTests(string patient_ID)
        {


            int patientId = Convert.ToInt32(patient_ID);
            string jsonObject = LookupLogic.GetvlTestsJson(patientId);

            return jsonObject;
        }

        [WebMethod(EnableSession = true)]
        public string GetPendingvlTests(string patient_ID)
        {


            int patientId = Convert.ToInt32(patient_ID);
            string jsonObject = LookupLogic.GetPendingvlTestsJson(patientId);

            return jsonObject;
        }



    }

}

