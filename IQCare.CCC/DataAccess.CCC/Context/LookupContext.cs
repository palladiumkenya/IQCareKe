using System.Data.Entity;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System.Data.Common;
using DataAccess.Base;

namespace DataAccess.CCC.Context
{
    public class LookupContext :BaseContext
    {

        public LookupContext() :  base((DbConnection)DataMgr.GetConnection(), true) {

            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            // Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<LookupContext>(null);
        }
        //public LookupContext(string connection) : base(connection)
        //{

        //}

        public DbSet<LookupItemView> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }
        public DbSet<LookupMaster> LookupMasters { get; set; } 
        public DbSet<LookupMasterItem> LookupMasterItems { get; set; }
        public DbSet<LookupCounty> LookupCounties { get; set; }
        public DbSet<LookupLabs> LookupLaboratories { get; set; }
        public DbSet<LookupPreviousLabs> LookupPreviousLaboratories { get; set; }
        public DbSet<PatientLookup> PatientLookups { get; set; }
    }
}
