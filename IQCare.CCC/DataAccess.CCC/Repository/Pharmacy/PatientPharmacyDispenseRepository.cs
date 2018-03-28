using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Pharmacy;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Repository.Pharmacy
{
    public class PatientPharmacyDispenseRepository : BaseRepository<PatientPharmacyDispense>, IPatientPharmacyDispenseRepository
    {
        private readonly GreencardContext _context;

        public PatientPharmacyDispenseRepository() : this(new GreencardContext())
        {
        }

        public PatientPharmacyDispenseRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}