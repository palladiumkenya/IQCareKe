using DataAccess.Context;
using IQCare.Web.API.Model;
using IQCare.Web.API.Infrastructure.Context;
using IQCare.Web.API.Infrastructure.Core.Interface;

namespace IQCare.Web.API.Infrastructure.Core.Repository
{
    public class ApiInteropSystemRepository :BaseRepository<ApiInteropSystem>,IApiInteropSystemsRepository
    {
        private readonly ApiContext _context;
        public ApiInteropSystemRepository() :this(new ApiContext())
        {

        }

        public ApiInteropSystemRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}
