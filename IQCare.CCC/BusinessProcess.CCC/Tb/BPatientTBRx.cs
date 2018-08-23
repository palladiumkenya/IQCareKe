using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientTBRx : ProcessBase, IPatientTBRx
    {
        private int _result;
        public int AddPatientTBRx(PatientTBRx p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientTBRxRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }
        public List<PatientTBRx> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientTBRx> patientTBRx =
                 _unitOfWork.PatientTBRxRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientTBRx;
            }
        }
        public int UpdatePatientTBRx(PatientTBRx p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientTBRxRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }
    }
}
