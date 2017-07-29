using Interface.CCC.assessment;
using System.Linq;
using Entities.CCC.Assessment;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Collections.Generic;

namespace BusinessProcess.CCC.assessment
{
    public class BPatientPsychosocialCriteriaManager :ProcessBase, PatientPsychosicialCriteriaManager
    {
        private int result;

        public int AddPreparation(PatientPsychoscialCriteria _patientPsychoscialCriteria)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientPsychosocialCriteriaRepository.Add(_patientPsychoscialCriteria);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int checkIfARTPreparationExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                result = _unitOfWork.PatientPsychosocialCriteriaRepository.FindBy(x => x.PatientId == patientId).Select(x => x.Id).FirstOrDefault();
                _unitOfWork.Dispose();
            }

            return result;
        }

        public int DeletePreparation(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var artprep = _unitOfWork.PatientPsychosocialCriteriaRepository.GetById(Id);
                _unitOfWork.PatientPsychosocialCriteriaRepository.Remove(artprep);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int EditPreparation(PatientPsychoscialCriteria _patientPsychoscialCriteria)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientPsychosocialCriteriaRepository.Update(_patientPsychoscialCriteria);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public List<PatientPsychoscialCriteria> GetPatientPsychosocialCriteriaDetails(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PsychosocialCriteria = _unitOfWork.PatientPsychosocialCriteriaRepository.FindBy(x => x.PatientId == patientId).ToList();
                return PsychosocialCriteria;
            }
        }
    }
}
