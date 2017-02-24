using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientHivDiagnosis:ProcessBase,IPatientHivDiagnosisManager
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
           _unitOfWork.PatientDiagnosisHivHistoryRepository.Update(patientHivDiagnosis);
            return Result = _unitOfWork.Complete();
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
    }
}
