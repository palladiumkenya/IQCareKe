using System.Collections.Generic;
using Entities.CCC.pharmacy;

namespace Interface.CCC.Pharmacy
{
    public interface IPatientPharmacyDispenseManager
    {
        int AddPatientPharmacyDispense(PatientPharmacyDispense p);
        int UpdatePatientPharmacyDispense(PatientPharmacyDispense p);
        void DeletePatientPharmacyDispense(int id);
        PatientPharmacyDispense GetPatientPharmacyDispense(int id);
        List<PatientPharmacyDispense> GetByPharmacyOrderId(int pharmacyOrderId);
    }
}