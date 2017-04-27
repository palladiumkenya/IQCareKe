using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientAdherenceAssessment : ProcessBase, IPatientAdherenceAssessessment
    {
        public int AddPatientAdherenceAssessment(PatientAdherenceAssessment patientAdherenceAssessment)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAdherenceAssessmentRepository.Add(patientAdherenceAssessment);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientAdherenceAssessment.Id;
            }
        }

        public PatientAdherenceAssessment GetPatientCurrentAdheranceStatus(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var adherance=unitOfWork.PatientAdherenceAssessmentRepository.FindBy(x => x.PatientId == patientId)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return adherance;
            }
        }

        public List<PatientAdherenceAssessment> GetAdherenceAssessmentsList(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var adherance = unitOfWork.PatientAdherenceAssessmentRepository.FindBy(x => x.PatientId == patientId)
                    .OrderByDescending(x => x.Id)
                    .ToList();
                unitOfWork.Dispose();
                return adherance;
            }
        }
    }
}
