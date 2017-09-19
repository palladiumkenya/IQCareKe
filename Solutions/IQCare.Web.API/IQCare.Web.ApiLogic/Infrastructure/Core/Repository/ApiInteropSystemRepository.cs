using DataAccess.Context;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Core.Repository
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
