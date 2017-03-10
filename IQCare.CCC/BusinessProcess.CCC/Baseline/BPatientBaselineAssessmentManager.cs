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
    public class BPatientBaselineAssessmentManager:ProcessBase,IPatientBaselineAssessmentManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        internal int Result;

        public int AddPatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            _unitOfWork.PatientBaselineAssessmentRepository.Add(patientBaselineAssessment);
            return Result=_unitOfWork.Complete();
        }

        public int UpdatePatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            var patientBaseline =
                _unitOfWork.PatientBaselineAssessmentRepository.FindBy(
                    x => x.PatientId == patientBaselineAssessment.PatientId & !x.DeleteFlag).FirstOrDefault();
            if (patientBaseline != null)
            {
                patientBaseline.Breastfeeding = patientBaselineAssessment.Breastfeeding;
                patientBaseline.CD4Count = patientBaselineAssessment.CD4Count;
                patientBaseline.HBVInfected = patientBaselineAssessment.HBVInfected;
                patientBaseline.Height = patientBaselineAssessment.Height;
                patientBaseline.MUAC = patientBaselineAssessment.MUAC;
                patientBaseline.Pregnant = patientBaselineAssessment.Pregnant;
                patientBaseline.TBInfected = patientBaselineAssessment.TBInfected;
                patientBaseline.Weight = patientBaselineAssessment.Weight;
                patientBaseline.WHOStage = patientBaselineAssessment.WHOStage;
                _unitOfWork.PatientBaselineAssessmentRepository.Update(patientBaselineAssessment);
                 Result = _unitOfWork.Complete();
            }
            return Result;
        }

        public int DeletePatientBaselineAssessment(int id)
        {
            var patientArt = _unitOfWork.PatientBaselineAssessmentRepository.GetById(id);
            _unitOfWork.PatientBaselineAssessmentRepository.Remove(patientArt);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientBaselineAssessment> GetPatientBaselineAssessment(int patientId)
        {
            var patientBaseline =
                _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
                    .Take(1)
                    .Distinct()
                    .OrderByDescending(x => x.Id)
                    .ToList();
            return patientBaseline;
        }

        public int CheckIfPatientBaselineExists(int patientId)
        {
            var recordExists =
                _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            return Convert.ToInt32(recordExists);
        }
    }
}
