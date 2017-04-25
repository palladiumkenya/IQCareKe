using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPatientPopulationManager:ProcessBase,IPatientPopuationManager
    {
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;
        public int AddPatientPopulation(PatientPopulation patientPopulation)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PatientPopulationRepository.Add(patientPopulation);
                 _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdatePatientPopulation(PatientPopulation patientPopulation)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {

                _unitOfWork.PatientPopulationRepository.Update(patientPopulation);              
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePatientPopulation(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                PatientPopulation patientPopulation = _unitOfWork.PatientPopulationRepository.GetById(id);
                _unitOfWork.PatientPopulationRepository.Remove(patientPopulation);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientPopulation> GetAllPatientPopulations(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientPopulation> patientPopulations = _unitOfWork.PatientPopulationRepository.FindBy(x => x.PersonId == patientId & x.DeleteFlag == false)
                                                .OrderByDescending(x => x.Id)
                                                .ToList();
                _unitOfWork.Dispose();
                return patientPopulations;
            }
        }

        public List<PatientPopulation> GetCurrentPatientPopulations(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientPopulation> patientPopulations = _unitOfWork.PatientPopulationRepository.FindBy(
            x => x.PersonId == patientId & x.Active & !x.DeleteFlag)
                            .OrderByDescending(x => x.Id)
                            .Take(1)
                            .ToList();
                _unitOfWork.Dispose();
                return patientPopulations;
            }
        }
    }
}
