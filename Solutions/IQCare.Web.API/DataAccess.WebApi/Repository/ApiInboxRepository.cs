using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi;

namespace DataAccess.WebApi.Repository
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
