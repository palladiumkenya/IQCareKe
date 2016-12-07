using System.Linq;
using Common.Data;
using Common.Data.Repository;
using Config.Core.Interfaces;
using Config.Core.Model;

namespace Config.Data.Repository
{
    public class ServiceAreaRepository:BaseRepository<ServiceArea>, IServiceAreaRepository
    {
        private readonly ConfigContext _context;

        public ServiceAreaRepository(ConfigContext context) : base(context)
        {
            _context = context;
        }

        public ServiceArea GetByCode(string code)
        {
            return _context
                .ServiceAreas
                .FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }
    }
}