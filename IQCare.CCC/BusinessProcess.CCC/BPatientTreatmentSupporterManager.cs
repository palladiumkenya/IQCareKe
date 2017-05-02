using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using Entities.PatientCore;
using Interface.CCC;
using DataAccess.CCC.Repository;
using DataAccess.Context;

namespace BusinessProcess.CCC
{

    public class BPatientTreatmentSupporterManager:ProcessBase,IPatientTreatmeantSupporterManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        private PatientTreatmentSupporter _patienPersonTreatmentSupporter;

        public int AddPatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {

                unitOfWork.PatientTreatmentSupporterRepository.Add(patientTreatmentSupporter);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdatePatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {

                unitOfWork.PatientTreatmentSupporterRepository.Add(patientTreatmentSupporter);
                 _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePatientTreatmentSupporter(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _patienPersonTreatmentSupporter = unitOfWork.PatientTreatmentSupporterRepository.GetById(id);
                unitOfWork.PatientTreatmentSupporterRepository.Remove(_patienPersonTreatmentSupporter);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientTreatmentSupporter> GetCurrentTreatmentSupporter(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                   unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                           x => x.PersonId == personId & x.DeleteFlag == false)
                       .OrderByDescending(x => x.Id)
                       .Take(1)
                       .ToList();
                unitOfWork.Dispose();
                return patientTreatmentSupporters;
            }
        }

        public List<PatientTreatmentSupporter> GetAllTreatmentSupporter(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                        x => x.PersonId == personId & x.DeleteFlag == false)
                    .OrderByDescending(x => x.Id)
                    .ToList();
                unitOfWork.Dispose();
                return patientTreatmentSupporters;
            }
        }
    }
}
