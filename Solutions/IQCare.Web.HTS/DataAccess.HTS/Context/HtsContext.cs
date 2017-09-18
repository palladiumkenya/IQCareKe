using System.Data.Common;
using DataAccess.Base;
using DataAccess.Context;
using System.Data.Entity;
using Entities.HTS;

namespace DataAccess.HTS.Context
{
    class HtsContext :BaseContext
    {
        public HtsContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<HtsContext>(null);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<EncounterResult> EncounterResults { get; set; }

       
    }
}
