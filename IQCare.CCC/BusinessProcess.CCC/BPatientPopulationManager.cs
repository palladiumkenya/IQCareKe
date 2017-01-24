using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPatientPopulationManager:IPatientPopuationManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        public void AddPatientPopulation(PatientPopulation patientPopulation)
        {
           _unitOfWork.PatientPopulationRepository.Add(patientPopulation);
            _unitOfWork.Complete();
        }

        public void UpdatePatientPopulation(PatientPopulation patientPopulation)
        {
            _unitOfWork.PatientPopulationRepository.Update(patientPopulation);
            _unitOfWork.Complete();
        }

        public void DeletePatientPopulation(int id)
        {
          PatientPopulation patientPopulation=  _unitOfWork.PatientPopulationRepository.GetById(id);
            _unitOfWork.PatientPopulationRepository.Remove(patientPopulation);
            _unitOfWork.Complete();
        }

        public List<PatientPopulation> GetAllPatientPopulations(int patientId)
        {
           List<PatientPopulation> patientPopulations= _unitOfWork.PatientPopulationRepository.FindBy(x => x.PatientId == patientId & x.DeleteFlag == false)
                .OrderByDescending(x => x.Id)
                .ToList();
            return patientPopulations;
        }

        public List<PatientPopulation> GetCurrentPatientPopulations(int patientId)
        {
           List<PatientPopulation> patientPopulations =
                    _unitOfWork.PatientPopulationRepository.FindBy(
                            x => x.PatientId == patientId & x.DeleteFlag == false & x.Active)
                            .OrderByDescending(x=>x.Id)
                            .Take(1)
                        .ToList();
            return patientPopulations;
        }
    }
}
