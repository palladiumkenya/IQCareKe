using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi;

namespace DataAccess.WebApi.Repository
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
