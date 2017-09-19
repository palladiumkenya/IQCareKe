using DataAccess.Context;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Core.Repository
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
