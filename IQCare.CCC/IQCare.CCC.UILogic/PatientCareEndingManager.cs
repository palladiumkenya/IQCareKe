using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public class PatientCareEndingManager
    {
        private IPatientCareEnding mgr =
            (IPatientCareEnding)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientCareEnding, BusinessProcess.CCC");

        public int AddPatientCareEndingDeath(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, DateTime? dateOfDeath, string careEndingNotes, int? reasonsForDeath, int? specificCausesOfDeath)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                DateOfDeath = dateOfDeath,
                CareEndingNotes = careEndingNotes,
                ReasonsForDeath = reasonsForDeath,
                SpecificCausesOfDeath = specificCausesOfDeath
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int AddPatientCareEndingOther(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, string careEndingNotes, int? tracingOutome, int? reasonLostToFollowup)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                CareEndingNotes = careEndingNotes,
                TracingOutome = tracingOutome,
                ReasonLostToFollowup = reasonLostToFollowup
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int AddPatientCareEndingTransferOut(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, string transferOutFacility, string careEndingNotes, string reasonForTransferOut, DateTime? dateExpectedToReport)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                TransferOutFacility = transferOutFacility,
                CareEndingNotes = careEndingNotes,
                ReasonForTransferOut = reasonForTransferOut,
                DateExpectedToReport = dateExpectedToReport
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int UpdatePatientCareEnding(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, string transferOutFacility, DateTime dateOfDeath, string careEndingNotes)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                TransferOutFacility = transferOutFacility,
                DateOfDeath = dateOfDeath,
                CareEndingNotes = careEndingNotes
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int DeletePatientCareEnding(int id)
        {
            return mgr.DeletePatientCareEnding(id);
        }


        public List<PatientCareEnding>  GetPatientCareEndingByVisitId(int patientId,int patientmasterVisitId)
        {
         
                return mgr.GetPatientCareEndingsByVisitId(patientId, patientmasterVisitId);
            
        }

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {
            return mgr.GetPatientCareEndings(patientId);
        }


        public string PatientCareEndingStatus(int patientId)
        {
            return mgr.PatientCareEndingStatus(patientId);
        }

        public int ResetPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            try
            {
                return mgr.ResetPatientCareEnding(patientCareEnding);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
