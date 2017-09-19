using DataAccess.Context;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Core.Repository
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
