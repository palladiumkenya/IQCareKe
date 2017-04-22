using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientIpt : ProcessBase, IPatientIpt
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientIpt(PatientIpt p)
        {
            _unitOfWork.PatientIptRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientIpt GetPatientIpt(int id)
        {
            PatientIpt patientIpt = _unitOfWork.PatientIptRepository.GetById(id);
            return patientIpt;
        }

        public void DeletePatientIpt(int id)
        {
            PatientIpt icf = _unitOfWork.PatientIptRepository.GetById(id);
            _unitOfWork.PatientIptRepository.Remove(icf);
            _unitOfWork.Complete();
        }

        public int UpdatePatientIpt(PatientIpt p)
        {
            _unitOfWork.PatientIptRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientIpt> GetByPatientId(int patientId)
        {
            List<PatientIpt> patientIpts = _unitOfWork.PatientIptRepository.GetByPatientId(patientId);
            return patientIpts;
        }
    }
}