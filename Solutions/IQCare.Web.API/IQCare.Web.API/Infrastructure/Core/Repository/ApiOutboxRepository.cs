using DataAccess.Context;
using IQCare.Web.API.Infrastructure.Context;
using IQCare.Web.API.Infrastructure.Core.Interface;
using IQCare.Web.API.Model;

namespace IQCare.Web.API.Infrastructure.Core.Repository
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
