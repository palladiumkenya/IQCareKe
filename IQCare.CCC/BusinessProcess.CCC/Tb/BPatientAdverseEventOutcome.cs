using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System.Linq;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientAdverseEventOutcome : ProcessBase, IPatientAdverseEventOutcomeManager
    {
        int result;

        public int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               result= unitOfWork.patientAdverseEventOutcomeRepository.FindBy(x => x.PatientId == patientId && x.PatientAdverseEventId==adverseEventId).Count();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePatientAdverseEventOutcome(int id)
        {
            throw new System.NotImplementedException();
        }

        public int SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            throw new System.NotImplementedException();
        }

        public int UpdatePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            throw new System.NotImplementedException();
        }
    }
}
