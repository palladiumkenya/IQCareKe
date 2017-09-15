using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientIcfAction : ProcessBase, IPatientIcfAction
    {
        private int _result;

        public int AddPatientIcfAction(PatientIcfAction p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIcfActionRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientIcfAction GetPatientIcfAction(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIcfAction patientIcfAction = _unitOfWork.PatientIcfActionRepository.GetById(id);
                _unitOfWork.Dispose();
                return patientIcfAction;
            }
        }

        public void DeletePatientIcfAction(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIcfAction icf = _unitOfWork.PatientIcfActionRepository.GetById(id);
                _unitOfWork.PatientIcfActionRepository.Remove(icf);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public int UpdatePatientIcfAction(PatientIcfAction p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIcfActionRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientIcfAction> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientIcfAction> patientIcfActions =
                _unitOfWork.PatientIcfActionRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientIcfActions;
            }
        }
    }
}