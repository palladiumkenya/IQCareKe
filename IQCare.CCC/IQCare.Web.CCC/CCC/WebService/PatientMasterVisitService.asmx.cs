using System;
using System.Web.Services;
using IQCare.CCC.UILogic.Visit;

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
                throw new Exception(e.Message + ' ' + e.InnerException);
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
                throw new Exception(e.Message + ' ' + e.InnerException);
            }

            return result;

        }
    }
}
