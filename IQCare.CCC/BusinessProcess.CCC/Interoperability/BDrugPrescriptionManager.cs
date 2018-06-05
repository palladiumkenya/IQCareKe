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
        public List<DrugPrescriptionSourceEntity>  GetPatientPrescriptionMessage(int ptnpk,int orderId,int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var drugPrescriptionMessage = unitOfWork.DrugPrescriptionMessageRepository.FindBy(x =>
                    x.PatientMasterVisitId == patientMasterVisitId && x.ptn_pharmacy_pk == orderId &&
                    x.PatientId == ptnpk).ToList();
                unitOfWork.Dispose();
                return drugPrescriptionMessage;
            }
        }
    }
}
