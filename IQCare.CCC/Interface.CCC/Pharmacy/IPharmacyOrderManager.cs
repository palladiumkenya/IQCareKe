using System.Collections.Generic;
using Entities.CCC.pharmacy;

namespace Interface.CCC.Pharmacy
{
    public interface IPharmacyOrderManager
    {
        int AddPharmacyOrder(PharmacyOrder p);
        int UpdatePharmacyOrder(PharmacyOrder p);
        void DeletePharmacyOrder(int id);
        PharmacyOrder GetPharmacyOrder(int id);
        List<PharmacyOrder> GetByPatientId(int patientId);
    }
}