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
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientBaselineAssessmentRepository.Add(patientBaselineAssessment);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }         
        }

        public int UpdatePatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientBaseline =
                    unitOfWork.PatientBaselineAssessmentRepository.FindBy(
                        x => x.PatientId == patientBaselineAssessment.PatientId & !x.DeleteFlag).FirstOrDefault();
                if (patientBaseline != null)
                {
                    patientBaseline.Breastfeeding = patientBaselineAssessment.Breastfeeding;
                   if(patientBaselineAssessment.CD4Count>0) { patientBaseline.CD4Count = patientBaselineAssessment.CD4Count;}
                    patientBaseline.HBVInfected = patientBaselineAssessment.HBVInfected;
                    patientBaseline.Height = patientBaselineAssessment.Height;
                    patientBaseline.MUAC = patientBaselineAssessment.MUAC;
                    patientBaseline.Pregnant = patientBaselineAssessment.Pregnant;
                    patientBaseline.TBInfected = patientBaselineAssessment.TBInfected;
                    patientBaseline.Weight = patientBaselineAssessment.Weight;
                    patientBaseline.WHOStage = patientBaselineAssessment.WHOStage;
                    unitOfWork.PatientBaselineAssessmentRepository.Update(patientBaseline);
                    Result = unitOfWork.Complete();
                }
                unitOfWork.Dispose();
                return Result;
            }

        }

        public int DeletePatientBaselineAssessment(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientArt = unitOfWork.PatientBaselineAssessmentRepository.GetById(id);
                unitOfWork.PatientBaselineAssessmentRepository.Remove(patientArt);
                 Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
    
        }

        public List<PatientBaselineAssessment> GetPatientBaselineAssessment(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientBaseline =
          unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
              .Take(1)
              .Distinct()
              .OrderByDescending(x => x.Id)
              .ToList();

                unitOfWork.Dispose();
                return patientBaseline;
            }

        }

        public int CheckIfPatientBaselineExists(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var recordExists =
    unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
        .Select(x => x.Id)
        .FirstOrDefault();

                unitOfWork.Dispose();
                return Convert.ToInt32(recordExists);
            }
        }
    }
}
