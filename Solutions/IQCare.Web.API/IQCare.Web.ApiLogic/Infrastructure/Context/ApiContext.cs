using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using DataAccess.Context;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Context
{
    public class ApiContext : BaseContext
    {
        public ApiContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ApiContext>(null);
        }

        public DbSet<ApiInbox> ApiInboxes { get; set; }
        public DbSet<ApiOutbox> ApiOutboxes { get; set; }
        public DbSet<ApiInteropSystem> ApiInteropSystems { get; set; }

    }
}
