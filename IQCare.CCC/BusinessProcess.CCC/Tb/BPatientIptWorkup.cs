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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientIptWorkup(PatientIptWorkup p)
        {
            _unitOfWork.PatientIptWorkupRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientIptWorkup GetPatientIptWorkup(int id)
        {
            PatientIptWorkup patientIptWorkup = _unitOfWork.PatientIptWorkupRepository.GetById(id);
            return patientIptWorkup;
        }

        public void DeletePatientIptWorkup(int id)
        {
            PatientIptWorkup icf = _unitOfWork.PatientIptWorkupRepository.GetById(id);
            _unitOfWork.PatientIptWorkupRepository.Remove(icf);
            _unitOfWork.Complete();
        }

        public int UpdatePatientIptWorkup(PatientIptWorkup p)
        {
            _unitOfWork.PatientIptWorkupRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientIptWorkup> GetByPatientId(int patientId)
        {
            List<PatientIptWorkup> patientIptWorkups = _unitOfWork.PatientIptWorkupRepository.GetByPatientId(patientId);
            return patientIptWorkups;
        }
    }
}