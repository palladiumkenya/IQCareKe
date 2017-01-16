using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.Lookup;
using DataAccess.Context;

namespace DataAccess.Lookup
{
    public class LookupContext : BaseContext
    {

        
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
        public DbSet<LookupItem> Item { get; set; }
    }
}
