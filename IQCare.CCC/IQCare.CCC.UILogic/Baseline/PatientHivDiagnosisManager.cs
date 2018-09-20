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
        private int _recordId=0;
        private int _result = 0;

        public int ManagePatientHivDiagnosis(int id,int patientId,int patientMasterVisitId,DateTime hivDiagnosisDate,DateTime enrollmentDate,int enrollmentWhoStage,string artInitiationDate,int userId, int historyARTUse)
        {
            _recordId = _patientHivDiagnosisManager.CheckIfDiagnosisExists(patientId);

            DateTime? artDate = null;
            if (!String.IsNullOrEmpty(artInitiationDate))
            {
                artDate = DateTime.Parse(artInitiationDate);
            }
            else
            {
                artDate = null;
            }

            var patienHivDiagnosisInsert = new PatientHivDiagnosis
            {
                Id = 0,
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                HivDiagnosisDate = hivDiagnosisDate,
                EnrollmentDate = enrollmentDate,
                EnrollmentWhoStage = enrollmentWhoStage,
                ArtInitiationDate = artDate,
                CreatedBy = userId,
                HistoryARTUse = historyARTUse
            };

            //if (artDate.HasValue) patienHivDiagnosisInsert.ArtInitiationDate = artDate.Value;
            //DateTime temp;

            //if (DateTime.TryParse(artInitiationDate.ToString("yy-mm-dd"), out temp) == true)
            //{
            //    patienHivDiagnosisInsert.ArtInitiationDate = temp;
            //}
  
           
            _result =(_recordId>0)? _patientHivDiagnosisManager.UpdatePatientHivDiagnosis(patienHivDiagnosisInsert) : _patientHivDiagnosisManager.AddPatientHivDiagnosis(patienHivDiagnosisInsert);
            return _result;
        }

        public int UpdatePatientHivDiagnosis(int id, int patientId, int patientMasterVisitId, DateTime hivDiagnosisDate, DateTime enrollmentDate, int enrollmentWhoStage, DateTime artInitiationDate)
        {
            var patientHivDiagnosisUpdate = new PatientHivDiagnosis
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
