using DataAccess.Context;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.Core.Repository
{
    public class ApiOutboxRepository : BaseRepository<ApiOutbox>, IApiOutboxRepository
    {
        private readonly ApiContext _context;
        public ApiOutboxRepository() :this(new ApiContext())
        {

        }

        public ApiOutboxRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}
