using System;
using System.Web.Services;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Visit;
using Newtonsoft.Json;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientMasterVisitService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientMasterVisitService : System.Web.Services.WebService
    {
        private string _jsonMessage = "";

        [WebMethod(EnableSession = true)]
        public int PatientCheckin()
        {
            int result = 0;
            try
            {
                int patientId = Convert.ToInt32(Session["patientId"]);
                int userId = Convert.ToInt32(Session["AppUserId"]);
                PatientMasterVisitManager patientMasterVisit = new PatientMasterVisitManager();
                result = patientMasterVisit.PatientMasterVisitCheckin(patientId,userId);

                /* Assign to patientMsterVisitId session*/
                Session["EncounterStatusId"] = 1;
                Session["PatientMasterVisitId"] = result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        public int PatientCheckout(int id,int visitSchedule, int visitBy, int visitType, DateTime visitDate)
        {
            int result = 0;
            try
            {
                int patientId = Convert.ToInt32(Session["patientId"]);
                int visitId = Convert.ToInt32(Session["patientMasterVisitId"]);
                PatientMasterVisitManager patientMasterVisit = new PatientMasterVisitManager();
                result = patientMasterVisit.PatientMasterVisitCheckout(visitId, patientId,visitSchedule,visitBy,visitType,visitDate);
                Session["EncounterStatusId"] = 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;

        }

        [WebMethod(EnableSession = true)]
        public string PatientCareEndingStatus()
        {
            try
            {
                int patientId = Convert.ToInt32(Session["patientId"]);
                PatientCareEndingManager patientCareEnding=new PatientCareEndingManager();
                _jsonMessage = JsonConvert.SerializeObject(patientCareEnding.GetPatientCareEndings(patientId));
            }
            catch (Exception e)
            {
                _jsonMessage=e.Message;
            }
            return _jsonMessage;
        }
    }
}
