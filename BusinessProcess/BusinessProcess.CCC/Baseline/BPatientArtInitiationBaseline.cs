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
    public class BPatientArtInitiationBaseline:ProcessBase,IPatientArtInitiationBaselineManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddArtInitiationBaseline(PatientArtInitiationBaseline patientArtInitiationBaseline)
        {
            _unitOfWork.PatientTreatmentInitiationRepository.Add(patientArtInitiationBaseline);
            return Result=_unitOfWork.Complete();
        }

        public int UpdateArtInitiationBaseline(PatientArtInitiationBaseline patientArtInitiationBaseline)
        {
            _unitOfWork.PatientTreatmentInitiationRepository.Update(patientArtInitiationBaseline);
            return Result = _unitOfWork.Complete();
        }

        public int DeleteArtInitiationBaseline(int id)
        {
            var patientArt = _unitOfWork.PatientTreatmentInitiationRepository.GetById(id);
            _unitOfWork.PatientTreatmentInitiationRepository.Remove(patientArt);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientArtInitiationBaseline> GetPatientArtInitiationBaseline(int patientId)
        {
            var patientArtInitiationBaseline =
                _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Take(1)
                    .Distinct()
                    .OrderByDescending(x => x.Id)
                    .ToList();
            return patientArtInitiationBaseline;
        }
    }
}
