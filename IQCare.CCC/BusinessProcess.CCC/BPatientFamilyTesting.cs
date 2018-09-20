using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC;
using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace BusinessProcess.CCC
{
    public class BPatientFamilyTesting : ProcessBase, IPatientFamilyTesting
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientFamilyTestings(PatientFamilyTesting p)
        {
            _unitOfWork.PatientFamilyTestingRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientFamilyTesting GetPatientFamilyTestings(int id)
        {
            PatientFamilyTesting familyTesting = _unitOfWork.PatientFamilyTestingRepository.GetById(id);
            return familyTesting;
        }

        public void DeletePatientFamilyTestings(int id)
        {
            PatientFamilyTesting familyTesting = _unitOfWork.PatientFamilyTestingRepository.GetById(id);
            _unitOfWork.PatientFamilyTestingRepository.Remove(familyTesting);
            _result = _unitOfWork.Complete();
        }

        public int UpdatePatientFamilyTestings(PatientFamilyTesting p)
        {
            _unitOfWork.PatientFamilyTestingRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientFamilyTesting> GetByPatientId(int patientId)
        {
            List<PatientFamilyTesting> familyTestings = _unitOfWork.PatientFamilyTestingRepository.GetByPatientId(patientId);
            return familyTestings;
        }
    }
}