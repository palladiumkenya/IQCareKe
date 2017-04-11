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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientIptOutcome(PatientIptOutcome p)
        {
            _unitOfWork.PatientIptOutcomeRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientIptOutcome GetPatientIptOutcome(int id)
        {
            PatientIptOutcome patientIptOutcome = _unitOfWork.PatientIptOutcomeRepository.GetById(id);
            return patientIptOutcome;
        }

        public void DeletePatientIptOutcome(int id)
        {
            PatientIptOutcome icf = _unitOfWork.PatientIptOutcomeRepository.GetById(id);
            _unitOfWork.PatientIptOutcomeRepository.Remove(icf);
            _unitOfWork.Complete();
        }

        public int UpdatePatientIptOutcome(PatientIptOutcome p)
        {
            _unitOfWork.PatientIptOutcomeRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public List<PatientIptOutcome> GetByPatientId(int patientId)
        {
            List<PatientIptOutcome> patientIptOutcomes = _unitOfWork.PatientIptOutcomeRepository.GetByPatientId(patientId);
            return patientIptOutcomes;
        }
    }