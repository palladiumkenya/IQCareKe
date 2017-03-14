using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientHivDiagnosisManager:ProcessBase,IPatientHivDiagnosisManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis)
        {
           _unitOfWork.PatientDiagnosisHivHistoryRepository.Add(patientHivDiagnosis);
            return Result = _unitOfWork.Complete();
        }

        public int UpdatePatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis)
        {
            var patientDiagnosis =
                _unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(
                    x => x.PatientId == patientHivDiagnosis.PatientId & !x.DeleteFlag).FirstOrDefault();
            if (patientDiagnosis != null)
            {
                patientDiagnosis.ArtInitiationDate = patientHivDiagnosis.ArtInitiationDate;
                patientDiagnosis.EnrollmentDate = patientHivDiagnosis.EnrollmentDate;
                patientDiagnosis.EnrollmentWhoStage = patientHivDiagnosis.EnrollmentWhoStage;
                patientDiagnosis.HivDiagnosisDate = patientHivDiagnosis.HivDiagnosisDate;
                _unitOfWork.PatientDiagnosisHivHistoryRepository.Update(patientDiagnosis);
                Result = _unitOfWork.Complete();
            }
            return Result;
        }

        public int DeletePatientHivDiagnosis(int id)
        {
            var item=_unitOfWork.PatientDiagnosisHivHistoryRepository.GetById(id);
            _unitOfWork.PatientDiagnosisHivHistoryRepository.Remove(item);
            return Result=_unitOfWork.Complete();
        }

        public List<PatientHivDiagnosis> GetPatientHivDiagnosis(int patientId)
        {
            return _unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(x => x.PatientId == patientId).ToList();
        }

        public int CheckIfDiagnosisExists(int patientId)
        {
            int id = 0;
            var recordExists =
                _unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id).FirstOrDefault();
            recordExists=(recordExists>1)?id=recordExists:id=0;
            return id;
        }

    }
}
