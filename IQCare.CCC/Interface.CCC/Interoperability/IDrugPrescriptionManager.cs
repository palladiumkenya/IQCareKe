using System.Collections.Generic;
using Entities.CCC.Interoperability;


namespace Interface.CCC.Interoperability
{
    public interface IDrugPrescriptionManager
   {
       List<DrugPrescriptionSourceEntity>  GetPatientPrescriptionMessage(int ptnpk,int orderId,int patientMasterVisitId);
         
   }
}
