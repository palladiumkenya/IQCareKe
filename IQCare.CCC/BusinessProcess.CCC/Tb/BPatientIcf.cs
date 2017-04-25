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
        private int _result;

        public int AddPatientIcf(PatientIcf p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIcfRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientIcf GetPatientIcf(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIcf patientIcf = _unitOfWork.PatientIcfRepository.GetById(id);
                _unitOfWork.Dispose();
                return patientIcf;
            }
        }

        public void DeletePatientIcf(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIcf icf = _unitOfWork.PatientIcfRepository.GetById(id);
                _unitOfWork.PatientIcfRepository.Remove(icf);
                _unitOfWork.Dispose();
                _unitOfWork.Complete();
            }
        }

        public int UpdatePatientIcf(PatientIcf p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIcfRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientIcf> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientIcf> patientIcfs = _unitOfWork.PatientIcfRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientIcfs;
            }
        }
    }
}