using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientTreatmentInitiationManager:ProcessBase,IPatientTreatmentInitiationManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        public int AddPatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            _unitOfWork.PatientTreatmentInitiationRepository.Add(patientTreatmentInitiation);
            return _unitOfWork.Complete();
        }

        public int UpdatePatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            _unitOfWork.PatientTreatmentInitiationRepository.Update(patientTreatmentInitiation);
            return _unitOfWork.Complete();
        }

        public int DeletePatientTreatmentInitiation(int id)
        {
            var item = _unitOfWork.PatientTreatmentInitiationRepository.GetById(id);
            _unitOfWork.PatientTreatmentInitiationRepository.Remove(item);
            return _unitOfWork.Complete();
        }

        public List<PatientTreatmentInitiation> GetPatientTreatmentInitiation(int patientId)
        {
           return  _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId).ToList();
        }
    }
}
