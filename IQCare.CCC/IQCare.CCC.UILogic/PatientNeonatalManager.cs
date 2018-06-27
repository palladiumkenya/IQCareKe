using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Neonatal;
using Interface.CCC;
using IQCare.Events;
using Entities.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public class PatientNeonatalManager
    {
        IPatientNeonatal _neonatal = (IPatientNeonatal)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientNeonatal, BusinessProcess.CCC");
        public int AddPatientNeonatal(PatientMilestone p)
        {
            PatientMilestone patientNeonatal = new PatientMilestone()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                CreatedBy = p.CreatedBy,
                milestoneAssessedId = p.milestoneAssessedId,
                milestoneDate = p.milestoneDate,
                milestoneAchievedId = p.milestoneAchievedId,
                milestoneStatusId = p.milestoneStatusId,
                milestoneComments = p.milestoneComments
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

        public int addPatientNeonatalHistory(int patientId, int patientMasterVisitId, string notes, int recordNeonatalHistory)
        {
            PatientNeonatalHistory patientNeonatal = new PatientNeonatalHistory()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                NeonatalHistoryNotes = notes,
                RecordNeonatalHistory = recordNeonatalHistory
            };
            int patientNeonatalId = _neonatal.AddPatientNeonatalHistory(patientNeonatal);
            return patientNeonatalId;
        }
        public void DeleteImmunization(int Id)
        {
            _neonatal.DeleteImmunization(Id);
        }
    }
}
