using System;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic;
using Interface.CCC.Visit;
using Entities.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System.Collections.Generic;
using IQCare.Web.Laboratory;
using Entities.CCC.Visit;

namespace IQCare.Web.CCC.WebService
{
    public class PatientViralLoad
    {
        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public int LabOrderTestId { get; set; }
        public int ParameterId { get; set; }
        public decimal? ResultValue { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? Undetectable { get; set; }
        public decimal? DetectionLimit { get; set; }
        public int Month { get; set; }
    }

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LabService : System.Web.Services.WebService
    {

        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLabOrderManager _lookupData = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");
      
        private int _labTestId;      
        private string Msg { get; set; }
        private int Result { get; set; }
        private int _ptnPk;
        int moduleId = 203;

        int patientPk = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);

        int patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
        int userId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
        int facilityId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
        //int moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);


        [WebMethod(EnableSession = true)]
        public string AddLabOrder(int patientPk, int patientMasterVisitId, string labOrderDate, string orderNotes, string patientLabOrder)
        {



            var labOrder = new PatientLabOrderManager();
            if (patientPk > 0)
            {
                labOrder.savePatientLabOrder(patientId, patientPk, userId, facilityId, moduleId, patientMasterVisitId, labOrderDate, orderNotes, patientLabOrder);

                Msg = "Patient Lab Order Recorded Successfully .";


            }
            else
            {

                Msg = "Patient Lab Order Not Recorded Successfully .";
               
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetLookupPreviousLabsList()
        {
           
            string jsonObject = LookupLogic.GetLookupPreviousLabsListJson(patientId);
            return jsonObject;
        }

        [WebMethod(EnableSession = true)]
        public string ExtruderCompleteLabsList()
        {

            string jsonObject = LookupLogic.LookupExtruderCompleteLabs(patientId);
            return jsonObject;
        }
        [WebMethod(EnableSession = true)]
        public string GetLookupPendingLabsList()
        {
                        
            string jsonObject = LookupLogic.GetLookupPendingLabsListJson(patientId);
            return jsonObject;
        }

        [WebMethod(EnableSession = true)]
        public string ExtruderPendingLabsList()
        {

            string jsonObject = LookupLogic.LookupExtruderPendingLabs(patientId);
            return jsonObject;
        }
        [WebMethod(EnableSession = true)]
        public string GetvlTests()
        {

    
            string jsonObject = LookupLogic.GetvlTestsJson(patientId);
            return jsonObject;
        }

        [WebMethod(EnableSession = true)]
        public string GetPendingvlTests()
        {


            string jsonObject = LookupLogic.GetPendingvlTestsJson(patientId);
            return jsonObject;
        }

        [WebMethod(EnableSession = true)]
        public List<PatientViralLoad> GetViralLoad()
        {

            PatientLabTracker TestId = _lookupData.GetPatientLabTestId(patientId);
            if (TestId != null)
            {
                _labTestId = TestId.LabTestId;
            }

            List<PatientViralLoad> patientViralDetails = new List<PatientViralLoad>();
            List<PatientLabTracker> list_vl = _lookupData.GetPatientVL(_labTestId);

            if (list_vl != null)
            {
                foreach (var item in list_vl)
                {
                    PatientViralLoad vl = new PatientViralLoad();

                    vl.ResultValue = item.ResultValues;
                    vl.Month = item.CreateDate.Month;

                    patientViralDetails.Add(vl);
                }
            }
            return patientViralDetails;

        }
        [WebMethod(EnableSession = true)]
        public int GetFacilityVLPendingCount(int facilityId)
        {
            int count = 0;        

            List<PatientLabTracker> facilityVLPending = new List<PatientLabTracker>();
            try
            {
                var facilityPendingVL = new PatientLabOrderManager();
                facilityVLPending = facilityPendingVL.GetVlPendingCount(facilityId);
                count = facilityVLPending.Count;
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return count;
        }
        [WebMethod(EnableSession = true)]
        public int GetFacilityVLCompleteCount(int facilityId)
        { 
        int count = 0;          

            List<PatientLabTracker> facilityVLComplete = new List<PatientLabTracker>();
            try
            {
                var facilityCompleteVL = new PatientLabOrderManager();
                facilityVLComplete = facilityCompleteVL.GetVlCompleteCount(facilityId);
                count = facilityVLComplete.Count;
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return count;
        }
    }

}

