using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientIcf : ProcessBase, IPatientIcf
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientIcf(PatientIcf p)
        {
            _unitOfWork.PatientIcfRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientIcf GetPatientIcf(int id)
        {
            PatientIcf patientIcf = _unitOfWork.PatientIcfRepository.GetById(id);
            return patientIcf;
        }

        public void DeletePatientIcf(int id)
        {
            PatientIcf icf = _unitOfWork.PatientIcfRepository.GetById(id);
            _unitOfWork.PatientIcfRepository.Remove(icf);
            _unitOfWork.Complete();
        }

        public int UpdatePatientIcf(PatientIcf p)
        {
            _unitOfWork.PatientIcfRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientIcf> GetByPatientId(int patientId)
        {
            List<PatientIcf> patientIcfs = _unitOfWork.PatientIcfRepository.GetByPatientId(patientId);
            return patientIcfs;
        }
    }
}