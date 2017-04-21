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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientIcfAction(PatientIcfAction p)
        {
            _unitOfWork.PatientIcfActionRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientIcfAction GetPatientIcfAction(int id)
        {
            PatientIcfAction patientIcfAction = _unitOfWork.PatientIcfActionRepository.GetById(id);
            return patientIcfAction;
        }

        public void DeletePatientIcfAction(int id)
        {
            PatientIcfAction icf = _unitOfWork.PatientIcfActionRepository.GetById(id);
            _unitOfWork.PatientIcfActionRepository.Remove(icf);
            _unitOfWork.Complete();
        }

        public int UpdatePatientIcfAction(PatientIcfAction p)
        {
            _unitOfWork.PatientIcfActionRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientIcfAction> GetByPatientId(int patientId)
        {
            List<PatientIcfAction> patientIcfActions = _unitOfWork.PatientIcfActionRepository.GetByPatientId(patientId);
            return patientIcfActions;
        }
    }
}