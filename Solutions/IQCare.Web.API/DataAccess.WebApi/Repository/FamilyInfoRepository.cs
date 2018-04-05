using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class FamilyInfoRepository : BaseRepository<FamilyInfo>, IFamilyInfoRepository
    {
        private readonly PsmartContext _context;

        public FamilyInfoRepository():this(new PsmartContext())
        {
        }

        public FamilyInfoRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}