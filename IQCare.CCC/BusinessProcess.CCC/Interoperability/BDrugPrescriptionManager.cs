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
        public List<DrugPrescriptionEntity> GetPatientPrescriptionMessage(int orderId, int ptnpk)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
 
               var entityPrescription=unitOfWork.DrugPrescriptionMessageRepository.FindBy(x => x.ptn_pharmacy_pk == orderId && x.PatientId == ptnpk);
                unitOfWork.Dispose();
                return entityPrescription.ToList();
                // return ;
            }
        }
    }
}
