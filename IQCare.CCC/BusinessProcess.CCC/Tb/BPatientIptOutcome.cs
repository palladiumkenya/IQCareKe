using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientIptOutcome : ProcessBase, IPatientIptOutcome
    {
        private int _result;

        public int AddPatientIptOutcome(PatientIptOutcome p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptOutcomeRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientIptOutcome GetPatientIptOutcome(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIptOutcome patientIptOutcome = _unitOfWork.PatientIptOutcomeRepository.GetById(id);
                _unitOfWork.Dispose();
                return patientIptOutcome;
            }
        }

        public void DeletePatientIptOutcome(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientIptOutcome icf = _unitOfWork.PatientIptOutcomeRepository.GetById(id);
                _unitOfWork.PatientIptOutcomeRepository.Remove(icf);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public int UpdatePatientIptOutcome(PatientIptOutcome p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIptOutcomeRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientIptOutcome> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientIptOutcome> patientIptOutcomes =
                 _unitOfWork.PatientIptOutcomeRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return patientIptOutcomes;
            }
        }
    }
}