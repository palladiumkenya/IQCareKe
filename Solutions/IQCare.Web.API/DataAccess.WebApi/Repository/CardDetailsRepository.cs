using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class CardDetailsRepository : BaseRepository<CARDDETAILS>, ICardDetailsRepository
    {
        private readonly PsmartContext _context;

        public CardDetailsRepository():this(new PsmartContext())
        {
        }

        public CardDetailsRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}