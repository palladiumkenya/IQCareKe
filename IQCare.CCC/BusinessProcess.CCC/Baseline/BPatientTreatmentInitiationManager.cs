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
      //  private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int Result = 0;

        public int AddPatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientTreatmentInitiationRepository.Add(patientTreatmentInitiation);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
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

                    _unitOfWork.PatientTreatmentInitiationRepository.Update(patientTreatment);
                    Result = _unitOfWork.Complete();
                }
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientTreatmentInitiation(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var item = _unitOfWork.PatientTreatmentInitiationRepository.GetById(id);
                _unitOfWork.PatientTreatmentInitiationRepository.Remove(item);
                 _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientTreatmentInitiation> GetPatientTreatmentInitiation(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientInitiation = _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId).ToList();
                _unitOfWork.Dispose();
                return patientInitiation;
            }
        }

        public int CheckIfPatientTreatmentExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var recordExists =_unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id)
                    .FirstOrDefault();
                _unitOfWork.Dispose();
                return Convert.ToInt32(recordExists);
            }
        }
    }
}
