using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi;

namespace DataAccess.WebApi.Repository
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
