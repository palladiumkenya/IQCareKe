using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientIptWorkup : ProcessBase, IPatientIptWorkup
    {
        private int _result;

        public int AddPatientIptWorkup(PatientIptWorkup p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptWorkupRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientIptWorkup GetPatientIptWorkup(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIptWorkup patientIptWorkup = _unitOfWork.PatientIptWorkupRepository.GetById(id);
                _unitOfWork.Dispose();
                return patientIptWorkup;
            }
        }

        public void DeletePatientIptWorkup(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIptWorkup icf = _unitOfWork.PatientIptWorkupRepository.GetById(id);
                _unitOfWork.PatientIptWorkupRepository.Remove(icf);
                _unitOfWork.Dispose();
                _unitOfWork.Complete();
            }
        }

        public int UpdatePatientIptWorkup(PatientIptWorkup p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptWorkupRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientIptWorkup> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientIptWorkup> patientIptWorkups =
                _unitOfWork.PatientIptWorkupRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientIptWorkups;
            }
        }
    }
}