using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Interface.Pharmacy
{
    public interface IPatientPharmacyDispenseRepository : IRepository<PatientPharmacyDispense>
    {
        List<PatientPharmacyDispense> GetByPharmacyOrder(int pharmacyOrderId);
    }
}