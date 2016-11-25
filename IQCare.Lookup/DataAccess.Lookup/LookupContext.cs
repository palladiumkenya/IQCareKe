using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.Lookup;


namespace DataAccess.Lookup
{
    public class LookupContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LabContext"/> class.
        /// </summary>
        public LookupContext()
            : base((DbConnection)DataMgr.GetConnection(), true)
        {

            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<LookupContext>(null);
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
        public DbSet<Item> Item { get; set; }
    }
}
