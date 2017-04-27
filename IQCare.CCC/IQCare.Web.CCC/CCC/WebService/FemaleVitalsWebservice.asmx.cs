using IQCare.CCC.UILogic.Screening;
using IQCare.CCC.UILogic.Triage;
using System;
using System.Web;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for TestWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

    [System.Web.Script.Services.ScriptService]
    public class TestWebservice : System.Web.Services.WebService
    {
        private string jsonMessage;
        private int result=0;
        private int patientId;
        private int userId;
        private int patientMasterVisitId;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession =true)]
        public string AddPatientPregnancyIndicator(int patientId, int patientMasterVisitId, DateTime lmp, DateTime edd, int pregnancyStatusId, bool ancProfile, DateTime ancProfileDate, int userId)
        {
            try
            {
                var patientPreganancyIndicator = new PatientPregnancyIndicatorManager();
               // userId= Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                result = patientPreganancyIndicator.AddPregnancyIndicator(patientId, patientMasterVisitId, lmp, edd, pregnancyStatusId, ancProfile, ancProfileDate, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]));
                jsonMessage=(result>0)? "Pregnancy Indicator added successfully" : "";
            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientPregnancy(int patientId, int patientMasterVisitId, DateTime LMP, DateTime EDD, string gravidae, string parity, int outcome, DateTime dateOfOutcome, int userId)
        {
            try
            {
                var patientPregnancy = new PatientPregnancyManager();
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                result = patientPregnancy.AddPatientPregnancy(patientId, patientMasterVisitId, LMP, EDD, gravidae, parity, outcome, dateOfOutcome, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]));
                jsonMessage = (result > 0) ? "Patient Pregnancy Added successfully!" : "";
            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return jsonMessage;
        }

        [WebMethod(EnableSession = true)]

        public string AddPatientFamilyPlanning(int patientId, int patientMasterVisitId, int FamilyPlanningStatusId, int ReasonNoOnFp, int userId)
        {
            try
            {
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                var familyPlanning = new PatientFamilyPlanningManager();
                result = familyPlanning.AddFamilyPlanningStatus(patientId, patientMasterVisitId, FamilyPlanningStatusId, ReasonNoOnFp, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]));
                jsonMessage = (result > 0) ? "Family planning status added successfuly" : "";

            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientFamilyPlanningMethod(int patientId, int PatientFPId, int userId)
        {
            try
            {
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                var fpMethod = new PatientFamilyPlanningMethodManager();
                result = fpMethod.AddFamilyPlanningMethod(patientId,PatientFPId, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]));
                jsonMessage = (result > 0) ? "family planning status addedd successfully" : "";
            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientScreening(int patientId, int patientMasterVisitid, int screeningTypeId, int screeningDone, DateTime screeningDate, int screeningCategoryId, int screeningValueId, string comment, int userId)
        {
            try
            {
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                var patientScreening = new PatientScreeningManager();
                result = patientScreening.AddPatientScreening(patientId, patientMasterVisitId, screeningTypeId, screeningDone, screeningDate, screeningCategoryId, screeningValueId, comment, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]));
                jsonMessage = (result > 0) ? "Patient screening addedd successfully!" : "";
            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public int PregnancyExists(int patientId)
        {
            try
            {
                var patientPregnancy = new PatientPregnancyManager();
                patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                result = patientPregnancy.CheckIfPatientPregnancyExisists(patientId);
            }
            catch (Exception e)
            {
                jsonMessage = e.Message;
            }
            return result;
        }
    }
}
