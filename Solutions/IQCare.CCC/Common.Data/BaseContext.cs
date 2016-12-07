using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Common.Data.Util;
using DataAccess.Base;

namespace Common.Data
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext() : base(ConnectionManager.GetConnection())
        {
        }
        public BaseContext(string connection) : base(connection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}