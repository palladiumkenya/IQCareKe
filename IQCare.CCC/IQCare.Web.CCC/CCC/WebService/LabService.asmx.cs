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
        private int facilityId { get; set; }      
        private int _labOrderId;      
        private string Msg { get; set; }
        private int Result { get; set; }
        private int _ptnPk;

        int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
        int userId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);


        [WebMethod(EnableSession = true)]
        public string AddLabOrder(int patientPk, int patientMasterVisitId, string patientLabOrder)
        {
            
            LookupFacility facility = _lookupManager.GetFacility();
            facilityId = facility.FacilityID;

            try
            {

                var labOrder = new PatientLabOrderManager();
                Result = labOrder.savePatientLabOrder(patientId, patientPk, userId, facilityId, patientMasterVisitId, patientLabOrder);
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
           
            PatientLookup ptpk = _lookupManager.GetPatientPtn_pk(patientId);
            if (ptpk.ptn_pk != null)
            {
                _ptnPk = ptpk.ptn_pk.Value;
            }

            LabOrderEntity labId = _lookupData.GetPatientLabOrder(_ptnPk);
            if (labId != null)
            {
                _labOrderId = labId.Id;
            }

            List<PatientViralLoad> patientViralDetails = new List<PatientViralLoad>();
            List<LabResultsEntity> list_vl = _lookupData.GetPatientVL(_labOrderId);

            if (list_vl != null)
            {
                foreach (var item in list_vl)
                {
                    PatientViralLoad vl = new PatientViralLoad();

                    vl.ResultValue = item.ResultValue;
                    vl.Month = item.CreateDate.Month;

                    patientViralDetails.Add(vl);
                }
            }
            return patientViralDetails;

        }
        [WebMethod(EnableSession = true)]
        public int GetFacilityVLPendingCount()
        {
            int count = 0;
            LookupFacility facility = _lookupManager.GetFacility();
            facilityId = facility.FacilityID;

            List<LabOrderEntity> facilityVLPending = new List<LabOrderEntity>();
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
        public int GetFacilityVLCompleteCount()
        { 
        int count = 0;
            LookupFacility facility = _lookupManager.GetFacility();
            facilityId = facility.FacilityID;

            List<LabOrderEntity> facilityVLComplete = new List<LabOrderEntity>();
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

