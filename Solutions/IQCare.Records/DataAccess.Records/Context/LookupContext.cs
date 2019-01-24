using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using DataAccess.Base;
using System.Data.Entity;
using DataAccess.Context;
using Entities.Records;


namespace DataAccess.Records.Context
{
    public class LookupContext:BaseContext
    {
        public LookupContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {

            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            // Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<LookupContext>(null);
        }

        public DbSet<LookupFacility> LookupFacility { get; set; }
        public DbSet<LookupCounty> LookupCounties { get; set; }

        public DbSet<LookupItem> LookupItems { get; set; }

        public DbSet <LookupItemView>  Lookups{ get;set; }


        public DbSet<LookupMaster> LookupMasters { get; set; }
        public DbSet<LookupMasterItem> LookupMasterItems { get; set; }

        public DbSet<PatientRegistrationLookup> PatientRegistrationLookups { get; set; }



        public DbSet<PatientLookup> PatientLookups { get; set; }
        public DbSet<PersonLookUp> PersonLookUps { get; set; }

        public DbSet<PersonContactLookUp> PersonContactLookups { get; set; }


        //p
    }
}
