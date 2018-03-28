using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Interface.Pharmacy
{
    public interface IPharmacyOrderRepository : IRepository<PharmacyOrder>
    {
        List<PharmacyOrder> GetByPatientId(int patientId);
    }
}