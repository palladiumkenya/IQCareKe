using DataAccess.Context;
using IQCare.Web.API.Infrastructure.Context;
using IQCare.Web.API.Infrastructure.Core.Interface;
using IQCare.Web.API.Model;

namespace IQCare.Web.API.Infrastructure.Core.Repository
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
