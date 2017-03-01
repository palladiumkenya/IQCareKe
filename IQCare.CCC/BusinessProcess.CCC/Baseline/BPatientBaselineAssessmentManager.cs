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
            _unitOfWork.PatientBaselineAssessmentRepository.Update(patientBaselineAssessment);
            return Result = _unitOfWork.Complete();
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
    }
}
