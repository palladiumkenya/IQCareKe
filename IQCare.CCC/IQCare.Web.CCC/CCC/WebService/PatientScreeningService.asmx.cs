using System;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;

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
        [WebMethod]
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
    }
}
