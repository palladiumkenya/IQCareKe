using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public class PatientCareEndingManager
    {
        private IPatientCareEnding mgr =
            (IPatientCareEnding)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientCareEnding, BusinessProcess.CCC");

        public int AddPatientCareEndingDeath(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, DateTime dateOfDeath, string careEndingNotes)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                //TransferOutFacility = transferOutFacility,
                DateOfDeath = dateOfDeath,
                CareEndingNotes = careEndingNotes
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int AddPatientCareEndingOther(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, string careEndingNotes)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                //TransferOutFacility = transferOutFacility,
                //DateOfDeath = dateOfDeath,
                CareEndingNotes = careEndingNotes
            };

            return mgr.AddPatientCareEnding(patientCareEnding);
        }

        public int AddPatientCareEndingTransferOut(int patientId, int patientMasterVisitId, int patientEnrollmentId, int exitReason, DateTime exitDate, string transferOutFacility, string careEndingNotes)
        {
            PatientCareEnding patientCareEnding = new PatientCareEnding()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientEnrollmentId = patientEnrollmentId,
                ExitReason = exitReason,
                ExitDate = exitDate,
                TransferOutFacility = transferOutFacility,
                //DateOfDeath = dateOfDeath,
                CareEndingNotes = careEndingNotes
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
