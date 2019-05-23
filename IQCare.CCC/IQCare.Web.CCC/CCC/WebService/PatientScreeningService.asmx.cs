using System;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;
using System.Web.Script.Serialization;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using System.Collections.Generic;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Visit;
using System.Collections;

namespace IQCare.Web.CCC.WebService
{

    
    /// <summary>
    /// Summary description for PatientScreeningService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientScreeningService : System.Web.Services.WebService
    {
        public int MasterVisitId;
        public class ScreeningData
        {
            public int? ItemId { get; set; }
            public string value { get; set; }
            
        }

      
        public class OutCome {
            public string Msg { get; set; }
            public int Result { get; set; }

        }


        public class ScreeningRecord
        {
            public string Id { get; set; }

            public int screeningType { get; set; }
            public  int screeningCategory { get; set; }
             public int screeningValue { get; set; }
              public int userId { get; set; }

        }
        private string Msg { get; set; }
        private int Result { get; set; }

        [WebMethod(EnableSession = true)]
        public string AddUpdateScreeningRecord(int patientId, int patientMasterVisitId,string ScreeningData)
        {
            try
            {


                ScreeningRecord[] result = new JavaScriptSerializer().Deserialize<ScreeningRecord[]>(ScreeningData);

                //var  result = new JavaScriptSerializer().Deserialize<List<>(ScreeningData).ToArray();


                if (result != null)
                {
                    if (result.Length > 0)
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            int patId = patientId;
                            int patiMasterId = patientMasterVisitId;
                            
                            try
                            {
                                int screeningValue = result[i].screeningValue;
                                var PSM = new PatientScreeningManager();
                                if (screeningValue > 0)
                                {
                                    Result = PSM.AddUpdatePatientScreening(patId, patiMasterId, result[i].screeningType, result[i].screeningCategory, result[i].screeningValue, result[i].userId);
                                    if (Result > 0)
                                    {
                                        Msg = "Screening Added";
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Msg = e.Message;
                            }
                          

                        }
                    }

                }
                
            }
           catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
           
        }
        [WebMethod(EnableSession = true)]
        public string AddUpdateScreeningData(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                if (screeningValue > 0)
                {
                    Result = PSM.AddUpdatePatientScreening(patientId, patientMasterVisitId, screeningType, screeningCategory, screeningValue, userId);
                    if (Result > 0)
                    {
                        Msg = "Screening Added";
                    }
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string AddUpdateScreeningDataByVisitId(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                if (screeningValue > 0)
                {
                    Result = PSM.AddUpdatePatientScreeningByVisitId(Convert.ToInt32(Session["PatientPK"]), patientMasterVisitId, screeningType, screeningCategory, screeningValue, userId);
                    if (Result > 0)
                    {
                        Msg = "Screening Added";
                    }
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string getPatientScreening(int PatientId)
        {
            PatientScreening[] patientScreeningData = (PatientScreening[])Session["patientScreeningData"];
            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(patientScreeningData);
            return jsonScreeningObject;
        }
        [WebMethod(EnableSession =true)]
        public string getScreeningByIdandMasterVisit(int PatientId, int PatientMasterVisitId)
        {
            var PSM = new PatientScreeningManager();
            PatientScreening[] patientScreeningData = PSM.GetPatientScreeningByVisitId(PatientId,PatientMasterVisitId).ToArray();
            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(patientScreeningData);
            return jsonScreeningObject;
        }
        [WebMethod(EnableSession =true)]
        public string getScreeningByStatus(string Status)
        {
            var PSM = new PatientScreeningManager();
            int statusId = Convert.ToInt32(LookupLogic.GetLookupItemId(Status));
            int patientId = Convert.ToInt32(Session["PatientPK"]);
            PatientScreening[] patientScreeningData = PSM.GetPatientScreeningStatus(patientId, statusId).ToArray();
            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(patientScreeningData);
            return jsonScreeningObject;
        }
        [WebMethod(EnableSession =true)]
        public string addCancellingVisitStatus(string status,int PatientMasterVisitId)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                
                int patientmasterVisitId = PatientMasterVisitId;
                if (patientmasterVisitId <=0)
                {
                    patientmasterVisitId= Convert.ToInt32(Session["PatientMasterVisitId"]);
                }
                
                Result = PSM.AddUpdatePatientScreening(Convert.ToInt32(Session["PatientPK"]), patientmasterVisitId, Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookupItemId(status)), Convert.ToInt32(Session["AppUserId"]));
                if (Result > 0)
                {
                    Msg = "Screening Added";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;

        }
        [WebMethod(EnableSession =true)]
        public string GetDepressionScreeningData(int PatientMasterVisitId,int PatientId)
        {
            var PSM = new PatientScreeningManager();
            LookupLogic lookUp = new LookupLogic();
           
            List<PatientScreening> screeningList = PSM.GetPatientScreeningByVisitId(PatientId, PatientMasterVisitId);
            List<LookupItemView> DepressionQuestions = new List<LookupItemView>();
            List<LookupItemView> lasttwoweeksqlist = lookUp.getQuestions("DepressionScreeningQuestions");
            List<LookupItemView> ph9qlist = lookUp.getQuestions("PHQ9Questions");
            DepressionQuestions.AddRange(ph9qlist);
            DepressionQuestions.AddRange(lasttwoweeksqlist);
            var DepressionSeverity = IQCare.CCC.UILogic.LookupLogic.GetLookupItemId("DepressionSeverity");
           var DepressionTota= IQCare.CCC.UILogic.LookupLogic.GetLookupItemId("DepressionTotal");
            var  RecommendManagement=IQCare.CCC.UILogic.LookupLogic.GetLookupItemId("ReccommendedManagement");

            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotesByVisitId(PatientId, PatientMasterVisitId);
            List <ScreeningData> ScreeningDepressionData = new List<ScreeningData>();
            if (notesList !=null)
            {
                if(DepressionSeverity !=null)
                {
                    var item = notesList.Find(x => x.NotesCategoryId == Convert.ToInt32(DepressionSeverity));
                    ScreeningData sc = new ScreeningData();
                    if (item != null)
                    {
                        sc.ItemId = item.NotesCategoryId;
                        sc.value = item.ClinicalNotes;
                        ScreeningDepressionData.Add(sc);
                    }
                }
                
            }

            List<PatientScreening> DepressionData = new List<PatientScreening>();
       

           
            foreach (var depression in DepressionQuestions)
            {
                var item = screeningList.Find(x => x.ScreeningCategoryId == depression.ItemId);
                if(item!=null)
                {
                    ScreeningData sc = new ScreeningData();

                    sc.ItemId = item.ScreeningCategoryId;
                    sc.value = item.ScreeningValueId.ToString();
                    ScreeningDepressionData.Add(sc);

                }

            }


            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(ScreeningDepressionData);
            return jsonScreeningObject;


        }

        [WebMethod(EnableSession = true)]
        public string GetPatientMasterVisitId(int PatientId, DateTime visitDate, string EncounterType, int ServiceAreaId, int UserId)
        {
            PatientMasterVisitManager pmvManager = new PatientMasterVisitManager();
            List<PatientMasterVisit> pmv = new List<PatientMasterVisit>();
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            OutCome ResultOutcome = new OutCome();
            pmv = pmvManager.GetPatientMasterVisitBasedonVisitDate(PatientId, visitDate);
            if (pmv!=null)
            {
                if (pmv.Count > 0)
                {
                    MasterVisitId = pmv[0].Id;
                }
            }

            
            if(MasterVisitId > 0)
            {
                
                Result = patientEncounter.SavePatientPreviousEncounter(PatientId, MasterVisitId, EncounterType, ServiceAreaId, Convert.ToInt32(Session["AppUserId"]),visitDate);
                if (Result > 0)
                {
                    ResultOutcome.Result = MasterVisitId;
                    ResultOutcome.Msg = EncounterType + " Encounter Saved";
                }
            }
            else
            {
                var lookupLogic = new LookupLogic();
                var facility = lookupLogic.GetFacility(Session["AppPosID"].ToString());
                if (facility == null)
                {
                    facility = lookupLogic.GetFacility();
                }

                PatientMasterVisit pm = new PatientMasterVisit();
                pm.ServiceId = 1;
                pm.VisitDate = visitDate;
                pm.VisitBy = UserId;
                pm.Start = visitDate;
                pm.End = visitDate;
                pm.PatientId = PatientId;
                pm.CreatedBy = UserId;
                pm.Active = true;
                pm.Status = 2;
                pm.FacilityId = facility.FacilityID;
                
                int  PatientMasterVisitId= pmvManager.AddPatientMasterVisit(pm);

                 int res = patientEncounter.SavePatientPreviousEncounter(PatientId, PatientMasterVisitId, EncounterType, ServiceAreaId, Convert.ToInt32(Session["AppUserId"]), visitDate);
                if (res > 0 )
                {
                    Result = PatientMasterVisitId;
                    ResultOutcome.Result = Result;
                    ResultOutcome.Msg = EncounterType +  "Encounter Saved";

                }
            }
            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(ResultOutcome);
            return jsonScreeningObject;


        }


        [WebMethod(EnableSession = true)]
        public string addCancellingStatus(string status)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                int PatientMasterVisitId = Convert.ToInt32(Session["PatientMasterVisitId"]);
                Result = PSM.AddUpdatePatientScreening(Convert.ToInt32(Session["PatientPK"]), PatientMasterVisitId, Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookupItemId(status)), Convert.ToInt32(Session["AppUserId"]));
                if (Result > 0)
                {
                    Msg = "Screening Added";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}
