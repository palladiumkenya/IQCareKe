using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Pharmacy;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Repository.Pharmacy
{
    public class PharmacyOrderRepository : BaseRepository<PharmacyOrder>, IPharmacyOrderRepository
    {
        private readonly GreencardContext _context;

        public PharmacyOrderRepository() : this(new GreencardContext())
        {
        }

        public PharmacyOrderRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}