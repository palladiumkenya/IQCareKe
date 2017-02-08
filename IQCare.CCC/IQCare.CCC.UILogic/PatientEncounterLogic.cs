using Application.Presentation;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.CCC.UILogic
{
    public class PatientEncounterLogic
    {
        public int savePatientEncounterPresentingComplaints(string patientMasterVisitID, string patientID, string serviceID, string VisitDate, string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus, string lmp, string PregStatus, string edd, string ANC, int OnFP, int fpMethod, string CaCx, string STIScreening, string STIPartnerNotification, string adverseEvent)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var advEvent = parser.Deserialize<List<AdverseEvents>>(adverseEvent);
            int val = patientEncounter.savePresentingComplaints(patientMasterVisitID, patientID, serviceID,VisitDate,VisitScheduled,VisitBy, Complaints, TBScreening, NutritionalStatus, lmp,PregStatus,edd,ANC, OnFP, fpMethod, CaCx,STIScreening,STIPartnerNotification, advEvent);
            return val;
        }

        public void savePatientEncounterChronicIllness(string masterVisitID, string patientID, string chronicIllness, string Vaccines)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var chrIllness = parser.Deserialize<List<ChronicIlness>>(chronicIllness);
            var vacc = parser.Deserialize<List<Vaccines>>(Vaccines);
            int val = patientEncounter.saveChronicIllness(masterVisitID, patientID, chrIllness, vacc);
        }
      
    }
}
