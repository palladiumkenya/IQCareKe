using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Triage;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientAdverseEventOutcome
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class PatientAdverseEventOutcome : System.Web.Services.WebService
    {
        private string msg;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public int AdverseEventOutcomeExisists(int patientId,int id,int patientMasterVisitId)
        {
            PatientAdverseEventOutcomeManager adverseEventOutcomeManager = new PatientAdverseEventOutcomeManager();
            return adverseEventOutcomeManager.CheckIfPatientAdverseEventOutcomeExists(patientId, id,patientMasterVisitId);
        }

        [WebMethod(EnableSession = true)]
        public string AddAdverseEventOutcome(int patientId,int patientMasterVisitId,int adverseEventId,int outcomeId,DateTime outcomeDate)
        {
            PatientAdverseEventOutcomeManager adverseEventOutcomeManager=new PatientAdverseEventOutcomeManager();
            var adverseOutcome = new Entities.CCC.Triage.PatientAdverseEventOutcome()
            {
                PatientId = patientId,
                PatientMasterVisitid = patientMasterVisitId,
                AdverseEventId = adverseEventId,
                OutcomeId = outcomeId,
                OutcomeDate = outcomeDate,
                UserId = Convert.ToInt32(Session["AppUserId"])
            };
           return  adverseEventOutcomeManager.SavePatientAdverseEventOutcome(adverseOutcome);
        }

        [WebMethod(EnableSession = true)]
        public string EditAdverseEventOutcome(int id,int patientId, int patientMasterVisitId, int adverseEventId, int outcomeId,DateTime outcomeDate)
        {
            PatientAdverseEventOutcomeManager adverseEventOutcomeManager = new PatientAdverseEventOutcomeManager();
            var eventOutcome = new Entities.CCC.Triage.PatientAdverseEventOutcome()
            {
                Id = id,
                PatientId = patientId,
                PatientMasterVisitid = patientMasterVisitId,
                AdverseEventId = adverseEventId,
                OutcomeId = outcomeId,
                OutcomeDate = outcomeDate
            };

            return adverseEventOutcomeManager.UpdatePatientAdverseEventOutcome(eventOutcome);
        }

        [WebMethod(EnableSession = true)]
        public string DeleteAdverseEventOutcome(int id)
        {
            PatientAdverseEventOutcomeManager adverseEventOutcomeManager = new PatientAdverseEventOutcomeManager();

            return adverseEventOutcomeManager.DeletePatientAdverseEventOutcome(id);
        }

        [WebMethod(EnableSession = true)]
        public List<Entities.CCC.Triage.PatientAdverseEventOutcome> GetAdverseEventOutcome(int adverseId,int patientMasterVisitId,int patientId)
        {
            PatientAdverseEventOutcomeManager adverseEventOutcomeManager = new PatientAdverseEventOutcomeManager();
            return adverseEventOutcomeManager.GetAdverseEventOutcome(adverseId,patientMasterVisitId,patientId);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvent()
        {
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            var result = LookupLogic.GetLookUpItemViewByMasterName("AdverseEvents");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var adverseEventOutcome = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < adverseEventOutcome.Count; i++)
            {
                string[] j = new string[2] { adverseEventOutcome[i].ItemId + "~" + adverseEventOutcome[i].DisplayName, adverseEventOutcome[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

    }
}
