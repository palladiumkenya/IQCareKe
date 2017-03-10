using System;
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
        private int Result = 0;

        public int AddPatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            _unitOfWork.PatientTreatmentInitiationRepository.Add(patientTreatmentInitiation);
            return _unitOfWork.Complete();
        }

        public int UpdatePatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            var patientTreatment =
                _unitOfWork.PatientTreatmentInitiationRepository.FindBy(
                    x => x.PatientId == patientTreatmentInitiation.PatientId & !x.DeleteFlag).FirstOrDefault();
            if (patientTreatment != null)
            {
                patientTreatment.BaselineViralload = patientTreatmentInitiation.BaselineViralload;
                patientTreatment.BaselineViralloadDate = patientTreatmentInitiation.BaselineViralloadDate;
                patientTreatment.Cohort = patientTreatmentInitiation.Cohort;
                patientTreatment.DateStartedOnFirstline = patientTreatmentInitiation.DateStartedOnFirstline;
                patientTreatment.Regimen = patientTreatmentInitiation.Regimen;

                _unitOfWork.PatientTreatmentInitiationRepository.Update(patientTreatmentInitiation);
                Result = _unitOfWork.Complete();
            }

            return Result;
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

        public int CheckIfPatientTreatmentExists(int patientId)
        {
            var recordExists =
                _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            return Convert.ToInt32(recordExists);
        }
    }
}
