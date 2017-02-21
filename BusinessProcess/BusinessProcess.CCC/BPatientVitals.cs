using DataAccess.Base;
using Entities.CCC.Triage;
using Interface.CCC;
using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC
{
    public class BPatientVitals : ProcessBase, IPatientVitals
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;
        public int AddPatientVitals(PatientVital p)
        {
            _unitOfWork.PatientVitalsRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientVital GetPatientVitals(int id)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetById(id);
            return vital;
        }

        public void DeletePatientVitals(int id)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetById(id);
            _unitOfWork.PatientVitalsRepository.Remove(vital);
            _unitOfWork.Complete();
        }

        public int UpdatePatientVitals(PatientVital p)
        {
            _unitOfWork.PatientVitalsRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientVital GetByPatientId(int patientId)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetByPatientId(patientId);
            return vital;
        }
    }
}