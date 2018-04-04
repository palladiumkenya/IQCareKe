using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Pharmacy;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Repository.Pharmacy
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        private readonly GreencardContext _context;
        public DrugRepository() : this(new GreencardContext())
        {
        }

        public DrugRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
