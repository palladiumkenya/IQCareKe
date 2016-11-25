using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.PatientCore;
using Entities.Common;
using Entities.Administration;

namespace DataAccess.Context
{
    public class PatientHomePageContext : DbContext
    {
        public PatientHomePageContext()
            : base((DbConnection)DataMgr.GetConnection(), true)
        {

            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<PatientHomePageContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<PatientAlert>();

        }

        public DbSet<PatientAlert> PatientAlert { get; set; }
        public DbSet<QueryDefinition> Query { get; set; }
        public DbSet<HomePageSection> PatientHomePageSection { get; set; }
        public DbSet<HomePageConfig> PatientHomePageConfig { get; set; }
        public DbSet<ServiceArea> ServiceArea { get; set; }
    }
    public class PatientContext : DbContext
    {
        public PatientContext()
            : base((DbConnection)DataMgr.GetConnection(), true)
        {
            
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<PatientContext>(null);
        }
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
       
        }

        /// <summary>
        /// Gets or sets the lab test.
        /// </summary>
        /// <value>
        /// The lab test.
        /// </value>
        public DbSet<Patient> PatientCore { get; set; }
        public DbSet<PatientVisit> PatientVisitCore { get; set; }

    }
}
