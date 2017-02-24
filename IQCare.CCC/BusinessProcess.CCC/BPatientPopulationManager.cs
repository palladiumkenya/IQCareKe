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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;
        public int AddPatientPopulation(PatientPopulation patientPopulation)
        {
           _unitOfWork.PatientPopulationRepository.Add(patientPopulation);
          return _result= _unitOfWork.Complete();
        }

        public int UpdatePatientPopulation(PatientPopulation patientPopulation)
        {
            _unitOfWork.PatientPopulationRepository.Update(patientPopulation);
           return _result= _unitOfWork.Complete();
        }

        public int DeletePatientPopulation(int id)
        {
          PatientPopulation patientPopulation=  _unitOfWork.PatientPopulationRepository.GetById(id);
            _unitOfWork.PatientPopulationRepository.Remove(patientPopulation);
          return _result=  _unitOfWork.Complete();
        }

        public List<PatientPopulation> GetAllPatientPopulations(int patientId)
        {
           List<PatientPopulation> patientPopulations= _unitOfWork.PatientPopulationRepository.FindBy(x => x.PersonId == patientId & x.DeleteFlag == false)
                .OrderByDescending(x => x.Id)
                .ToList();
            return patientPopulations;
        }

        public List<PatientPopulation> GetCurrentPatientPopulations(int patientId)
        {
           List<PatientPopulation> patientPopulations =
                    _unitOfWork.PatientPopulationRepository.FindBy(
                            x => x.PersonId == patientId & x.Active  & !x.DeleteFlag)
                            .OrderByDescending(x=>x.Id)
                            .Take(1)
                        .ToList();
            return patientPopulations;
        }
    }
}
