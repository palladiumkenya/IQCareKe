using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Neonatal;
using Interface.CCC;
using IQCare.Events;
using Entities.CCC.Encounter;
using Interface.CCC.Screening;
using Entities.CCC.Screening;

namespace IQCare.CCC.UILogic
{
    public class PatientNeonatalManager
    {
        IPatientNeonatal _neonatal = (IPatientNeonatal)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientNeonatal, BusinessProcess.CCC");
        private IPatientScreeningManager _patientScreening = (IPatientScreeningManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Screening.BPatientScreeningManager, BusinessProcess.CCC");
        public int AddPatientNeonatal(PatientMilestone p)
        {
            PatientMilestone patientNeonatal = new PatientMilestone()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                CreatedBy = p.CreatedBy,
                TypeAssessedId = p.TypeAssessedId,
                 DateAssessed = p.DateAssessed,
                AchievedId = p.AchievedId,
                 StatusId = p.StatusId,
                Comment = p.Comment
            };
            int neonatalId = _neonatal.AddPatientNeonatal(patientNeonatal);
            return neonatalId;
        }

        public int AddImmunizationHistory(PatientImmunizationHistory I)
        {
            PatientImmunizationHistory immunizationHistory = new PatientImmunizationHistory()
            {
                PatientId = I.PatientId,
                PatientMasterVisitId = I.PatientMasterVisitId,
                CreatedBy = I.CreatedBy,
                ImmunizationPeriodId = I.ImmunizationPeriodId,
                ImmunizationGivenId = I.ImmunizationGivenId,
                ImmunizationDate = I.ImmunizationDate
            };
            int immunizationHistoryId = _neonatal.AddImmunizationHistory(immunizationHistory);
            return immunizationHistoryId;
        }

        public int updateNeonatalScreeningData(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                //(screening>0) ? update:add
                int screeningResult = _patientScreening.checkScreeningByScreeningCategoryId(patientId, screeningType, screeningCategory);
                if (screeningResult > 0)
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
        public void DeleteImmunization(int Id)
        {
            _neonatal.DeleteImmunization(Id);
        }
    }
}
