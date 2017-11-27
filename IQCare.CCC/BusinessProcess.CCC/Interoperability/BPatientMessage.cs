using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BPatientMessage : ProcessBase, IPatientMessageManager
    {
        public PatientMessage GetPatientMessageByEntityId(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientMessages = unitOfWork.PatientMessageRepository.FindBy(x => x.PatientId == entityId).FirstOrDefault();
                unitOfWork.Dispose();
                return patientMessages;
            }
        }
    }
}
