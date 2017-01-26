using DataAccess.Base;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess.Context
{ 
//    interface IDbContext : IDisposable
//    {
//        DbSet<TEntity> Set<TEntity>() where TEntity : class;

//    }
    public class BaseContext : DbContext //, IDbContext
    {
        public BaseContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            // Configuration.LazyLoadingEnabled = true;
            //Database.SetInitializer<LabContext>(null);
            Database.SetInitializer<BaseContext>(null);
        }
        //public BaseContext(string connection) : base(connection)
        //{
        //    Configuration.ProxyCreationEnabled = false;
        //    // DataMgr.OpenDecryptedSession(base.Database.Connection);
        //    // Configuration.LazyLoadingEnabled = true;
        //   // Database.SetInitializer<LabContext>(null);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
