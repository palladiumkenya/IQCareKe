using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Encounter;

namespace IQCare.CCC.UILogic.Encounter
{
    public class SocialHistoryLogic
    {
        ISocialHistory socialHistory = (ISocialHistory)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BSocialHistory, BusinessProcess.CCC");
        public int addSocialHistory(int patientId, int patientMasterVisitId, int createdBy, int drinkAlcohol, int smoke, int useDrugs, string socialHistoryNotes,int recordSocialHistory)
        {
            PatientSocialHistory sHtx = new PatientSocialHistory()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                DrinkAlcoholId = drinkAlcohol,
                SmokeId = smoke,
                UseDrugsId = useDrugs,
                SocialHistoryNotes = socialHistoryNotes,
                RecordSocialHistory = recordSocialHistory
            };
            //check if social history exists
            List<PatientSocialHistory> SocialHistoryList = new List<PatientSocialHistory>();
            int SocialHistoryResult=0;
            SocialHistoryList = socialHistory.getSocialHistory(patientId,patientMasterVisitId);
            if (!SocialHistoryList.Any()){
                SocialHistoryResult = socialHistory.AddSocialHistory(sHtx);
            }
            else
            {
                SocialHistoryResult = socialHistory.updateSocialHistory(sHtx);
            }

            return SocialHistoryResult;
        }

        public int updateSocialHistory(int patientId, int patientMasterVisitId, int createdBy, int drinkAlcohol, int smoke, int useDrugs, string socialHistoryNotes, int socialHistoryId, int recordSocialHistory)
        {
            PatientSocialHistory sHtx = new PatientSocialHistory()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                DrinkAlcoholId = drinkAlcohol,
                SmokeId = smoke,
                UseDrugsId = useDrugs,
                SocialHistoryNotes = socialHistoryNotes,
                RecordSocialHistory = recordSocialHistory,
                Id = socialHistoryId
            };
            //check if social history exists
            List<PatientSocialHistory> SocialHistoryList = new List<PatientSocialHistory>();
            int SocialHistoryResult = 0;
            SocialHistoryResult = socialHistory.updateSocialHistory(sHtx);
            return SocialHistoryResult;
        }

        public List<PatientSocialHistory> getSocialHistory(int PatientID, int PatientMasterVisitID)
        {
            List<PatientSocialHistory> SocialHistoryList = new List<PatientSocialHistory>();
            try
            {
                SocialHistoryList = socialHistory.getSocialHistory(PatientID, PatientMasterVisitID);
            }
            catch (Exception)
            {
                throw;
            }
            return SocialHistoryList;
        }
    }
}
