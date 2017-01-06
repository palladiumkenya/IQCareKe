using DataAccess.Base;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess.Context
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbConnection connection,bool flag) :  base(connection, flag)
        {
        }
        //public BaseContext(string connection) : base(connection)
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
