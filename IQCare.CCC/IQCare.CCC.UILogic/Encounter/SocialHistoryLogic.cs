using System;
using System.Collections.Generic;
using Interface.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Screening;
using Interface.CCC.Screening;
using Entities.CCC.Screening;

namespace IQCare.CCC.UILogic.Encounter
{
    public class SocialHistoryLogic
    {
        ISocialHistory socialHistory = (ISocialHistory)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BSocialHistory, BusinessProcess.CCC");
        private IPatientScreeningManager _patientScreening = (IPatientScreeningManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Screening.BPatientScreeningManager, BusinessProcess.CCC");

        public int addSocialHistory(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                //(screening>0) ? update:add
                int screeningResult = _patientScreening.checkScreeningByScreeningCategoryId(patientId, screeningType, screeningCategory);
                if(screeningResult > 0)
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId,
                        Id = screeningResult
                    };
                    return _patientScreening.updatePatientScreeningById(PS);
                }
                else
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId
                    };
                    return _patientScreening.AddPatientScreening(PS);
                }     
            }
            catch (Exception)
            {
                throw;
            }
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
