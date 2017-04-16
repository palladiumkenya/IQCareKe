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
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        internal int Result;

        public int AddPatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientBaselineAssessmentRepository.Add(patientBaselineAssessment);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }         
        }

        public int UpdatePatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
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
                    _unitOfWork.PatientBaselineAssessmentRepository.Update(patientBaseline);
                    Result = _unitOfWork.Complete();
                }
                _unitOfWork.Dispose();
                return Result;
            }

        }

        public int DeletePatientBaselineAssessment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientArt = _unitOfWork.PatientBaselineAssessmentRepository.GetById(id);
                _unitOfWork.PatientBaselineAssessmentRepository.Remove(patientArt);
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
    
        }

        public List<PatientBaselineAssessment> GetPatientBaselineAssessment(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientBaseline =
          _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
              .Take(1)
              .Distinct()
              .OrderByDescending(x => x.Id)
              .ToList();

                _unitOfWork.Dispose();
                return patientBaseline;
            }

        }

        public int CheckIfPatientBaselineExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var recordExists =
    _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
        .Select(x => x.Id)
        .FirstOrDefault();

                _unitOfWork.Dispose();
                return Convert.ToInt32(recordExists);
            }
        }
    }
}
