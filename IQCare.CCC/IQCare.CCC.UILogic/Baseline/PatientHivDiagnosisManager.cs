using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientHivDiagnosisManager
    {
        private readonly IPatientHivDiagnosisManager _patientHivDiagnosisManager = (IPatientHivDiagnosisManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivDiagnosisManager, BusinessProcess.CCC");

        public int AddPatientHivDiagnosis(int id,int patientId,int patientMasterVisitId,DateTime hivDiagnosisDate,DateTime enrollmentDate,int enrollmentWhoStage,DateTime artInitiationDate,int userId)
        {
            var patienHivDiagnosisInsert = new PatientHivDiagnosis
            {
                Id=0,
				PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                HivDiagnosisDate = hivDiagnosisDate,
                EnrollmentDate = enrollmentDate,
                EnrollmentWhoStage = enrollmentWhoStage,
                ArtInitiationDate = artInitiationDate,
                CreatedBy = userId
            };

            return _patientHivDiagnosisManager.AddPatientHivDiagnosis(patienHivDiagnosisInsert);
        }

        public int UpdatePatientHivDiagnosis(int id, int patientId, int patientMasterVisitId, DateTime hivDiagnosisDate,DateTime enrollmentDate, int enrollmentWhoStage, DateTime artInitiationDate)
        {
            var patientHivDiagnosisUpdate= new PatientHivDiagnosis
            {
                Id = id,
                HivDiagnosisDate = hivDiagnosisDate,
                EnrollmentDate = enrollmentDate,
                EnrollmentWhoStage = enrollmentWhoStage,
                ArtInitiationDate = artInitiationDate
            };

            return _patientHivDiagnosisManager.UpdatePatientHivDiagnosis(patientHivDiagnosisUpdate);
        }

        public int DeletePatientHivDiagnosis(int id)
        {
            return _patientHivDiagnosisManager.DeletePatientHivDiagnosis(id);
        }

        public List<PatientHivDiagnosis> GetPatientHivDiagnosisList(int patientId)
        {
            return _patientHivDiagnosisManager.GetPatientHivDiagnosis(patientId);
        }
    }
}
