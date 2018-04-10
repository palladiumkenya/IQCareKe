using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi.PSmart;
using System.Data.Entity;

namespace DataAccess.WebApi.Repository
{
    public class PsmartRepository:BaseRepository<Psmart_Store>,IPsmartStoreRepository
    {
        private readonly PsmartContext _context;

        public PsmartRepository() : this(new PsmartContext())
        {

        }

        public PsmartRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
       
    }

    //public class PSmartAuthRepository : BaseRepository<UserAuth>, IPSmartAuthRepository
    //{
    //    private readonly PsmartContext _context;

    //    public PSmartAuthRepository() : this(new PsmartContext())
    //    {

    //    }

    //    public PSmartAuthRepository(PsmartContext context) : base(context)
    //    {
    //        _context = context;
    //    }
    //   // public DbSet<UserAuth> IQUser { get; set; }
    //    public void Add(PsmartStore psmartStore)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}