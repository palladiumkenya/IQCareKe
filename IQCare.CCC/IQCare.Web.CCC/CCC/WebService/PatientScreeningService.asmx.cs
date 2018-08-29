using System;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;
using System.Web.Script.Serialization;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
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
        private string Msg { get; set; }
        private int Result { get; set; }
        [WebMethod(EnableSession = true)]
        public string AddUpdateScreeningData(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                Result = PSM.AddUpdatePatientScreening(patientId, patientMasterVisitId, screeningType, screeningCategory, screeningValue, userId);
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
        [WebMethod(EnableSession = true)]
        public string AddUpdateScreeningDataByVisitId(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                Result = PSM.AddUpdatePatientScreeningByVisitId(Convert.ToInt32(Session["PatientPK"]), patientMasterVisitId, screeningType, screeningCategory, screeningValue, userId);
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
        [WebMethod(EnableSession = true)]
        public string addCancellingStatus(string status)
        {
            try
            {
                var PSM = new PatientScreeningManager();
                Result = PSM.AddUpdatePatientScreening(Convert.ToInt32(Session["PatientPK"]), Convert.ToInt32(Session["PatientMasterVisitId"]), Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookUpMasterId("EnhanceAdherenceCounselling")), Convert.ToInt32(LookupLogic.GetLookupItemId(status)), Convert.ToInt32(Session["AppUserId"]));
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
