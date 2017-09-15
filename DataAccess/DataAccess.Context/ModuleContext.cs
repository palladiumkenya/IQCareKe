using System.Data.Common;
using System.Data.Entity;
using System.Dynamic;
using DataAccess.Base;
using Entities.Administration;

namespace DataAccess.Context
{
    public class ModuleContext : BaseContext
    {
        public ModuleContext() : base((DbConnection) DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<ModuleContext>(null);
        }

        public DbSet<Module> Modules { get; set; }
    }
}
