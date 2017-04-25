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
        private int _result;

        public int AddPatientIpt(PatientIpt p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientIpt GetPatientIpt(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIpt patientIpt = _unitOfWork.PatientIptRepository.GetById(id);
                _unitOfWork.Dispose();
                return patientIpt;
            }
        }

        public void DeletePatientIpt(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIpt icf = _unitOfWork.PatientIptRepository.GetById(id);
                _unitOfWork.PatientIptRepository.Remove(icf);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public int UpdatePatientIpt(PatientIpt p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientIpt> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientIpt> patientIpts = _unitOfWork.PatientIptRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientIpts;
            }
        }
    }
}