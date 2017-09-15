using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Assessment;
using Interface.CCC.assessment;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessProcess.CCC.assessment
{
    class BPatientSupportCriteriaManager : ProcessBase, PatientPsychosicialCriteriaManager
    {
        private int result;

        public int AddPreparation(PatientSupportSystemCriteria _patientSupportSystemCriteria)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientSupportSystemCriteriaRepository.Add(_patientSupportSystemCriteria);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int AddPreparation(PatientPsychoscialCriteria p)
        {
            throw new NotImplementedException();
        }

        public int checkIfARTPreparationExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                result = _unitOfWork.PatientSupportSystemCriteriaRepository.FindBy(x => x.PatientId == patientId).Select(x => x.Id).Count();
                _unitOfWork.Dispose();
            }

            return result;
        }

        public int DeletePreparation(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var artprep = _unitOfWork.PatientSupportSystemCriteriaRepository.GetById(Id);
                _unitOfWork.PatientSupportSystemCriteriaRepository.Remove(artprep);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
            return result;
        }

        public int EditPreparation(PatientSupportSystemCriteria _patientSupportSystemCriteria)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientSupportSystemCriteriaRepository.Update(_patientSupportSystemCriteria);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int EditPreparation(PatientPsychoscialCriteria p)
        {
            throw new NotImplementedException();
        }

        public List<PatientPsychoscialCriteria> GetPatientPsychosocialCriteriaDetails(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientSupportSystemCriteria> GetPatientSupportSystemCriteriaDetails(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                return _unitOfWork.PatientSupportSystemCriteriaRepository.FindBy(x => x.PatientId == patientId).ToList();

            }
        }
    }
}
