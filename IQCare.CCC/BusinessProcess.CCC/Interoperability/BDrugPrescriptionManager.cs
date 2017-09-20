using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BDrugPrescriptionManager :ProcessBase, IDrugPrescriptionManager
    {
        public List<DrugPrescriptionMessage> GetPatientPrescriptionMessage(int orderId, int ptnpk)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var drugPrescription = unitOfWork.DrugPrescriptionMessageRepository.FindBy(x => x.NUMBER == orderId && x.ptnpk == ptnpk).ToList();
                unitOfWork.Dispose();
                return drugPrescription;
            }
        }
    }
}
