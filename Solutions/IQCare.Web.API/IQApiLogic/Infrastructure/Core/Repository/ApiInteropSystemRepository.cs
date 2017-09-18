using DataAccess.Context;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.Core.Repository
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
