using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Base;

namespace Common.Data
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}