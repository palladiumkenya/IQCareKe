using DataAccess.Base;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess.Context
{
    public  class BaseContext : DbContext
    {
        public BaseContext() :  base((DbConnection)DataMgr.GetConnection(), true)
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
