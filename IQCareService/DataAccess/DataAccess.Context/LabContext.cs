using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.Lab;
namespace DataAccess.Context
{
    public class LabContext:DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabContext"/> class.
        /// </summary>
        public LabContext()
            : base((DbConnection)DataMgr.GetConnection(), true)
        {
            
            Configuration.ProxyCreationEnabled = false;
           // DataMgr.OpenDecryptedSession(base.Database.Connection);
           // Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<LabContext>(null);
        }
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<LabTest>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //});
            //modelBuilder.Entity<LabOrder>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //});

        }

        /// <summary>
        /// Gets or sets the lab test.
        /// </summary>
        /// <value>
        /// The lab test.
        /// </value>
        public DbSet<LabTest> LabTest { get; set; }
        /// <summary>
        /// Gets or sets the test parameter.
        /// </summary>
        /// <value>
        /// The test parameter.
        /// </value>
        public DbSet<TestParameter> TestParameter { get; set; }
        /// <summary>
        /// Gets or sets the parameter results.
        /// </summary>
        /// <value>
        /// The parameter results.
        /// </value>
        public DbSet<LabTestParameterResult> ParameterResults { get; set; }
        /// <summary>
        /// Gets or sets the lab order tests.
        /// </summary>
        /// <value>
        /// The lab order tests.
        /// </value>
        public DbSet<LabOrderTest> LabOrderTests { get; set; }
        /// <summary>
        /// Gets or sets the lab order.
        /// </summary>
        /// <value>
        /// The lab order.
        /// </value>
        public DbSet<LabOrder> LabOrder { get; set; }
        /// <summary>
        /// Gets or sets the parameter configuration.
        /// </summary>
        /// <value>
        /// The parameter configuration.
        /// </value>
        public DbSet<ParameterResultConfig> ParameterConfig { get; set; }
    }
}
