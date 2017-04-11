using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess.Context
{
    public  class BaseContext : DbContext
    {
        public BaseContext(DbConnection connection,bool flag) :  base(connection, flag)
        {
        }
        public BaseContext(string connection) : base(connection)
        {
        }

        public BaseContext()
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
