using DataAccess.Context;
using IQ.ApiLogic.Model;
using IQ.ApiLogic.Infrastructure.Core.Interface;
using IQ.ApiLogic.Infrastructure.Context;

namespace IQ.ApiLogic.Infrastructure.Core.Repository
{
    public class ApiInboxRepository :BaseRepository<ApiInbox>,IApiInboxRepository
    {
        private readonly ApiContext _context;

        public ApiInboxRepository() :this(new ApiContext())
        {

        }

        public ApiInboxRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}
