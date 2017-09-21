using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public  class DrugPrescriptionMessage 
    {
        private readonly IDrugPrescriptionManager _drugPrescriptionManager  = (IDrugPrescriptionManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BDrugPrescriptionManager, BusinessProcess.CCC");

        public List<DrugPrescriptionEntity> GetPrescriptionMessage(int orderId,int ptntpk)
        {
            return _drugPrescriptionManager.GetPatientPrescriptionMessage(orderId, ptntpk);
        }
    }
}
