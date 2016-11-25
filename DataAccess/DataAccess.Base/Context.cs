using System.Data.Entity;
using System.Data.Common;

namespace DataAccess.Base
{
    public class Context<T> : DbContext ,IUnitOfWork
    {
       
        public Context()
            : base((DbConnection)DataMgr.GetConnection(), true)
        {
           
            Configuration.ProxyCreationEnabled = false;
            
        }
           
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }

        public void Commit()
        {
            
        }

        public new IDbSet<T> Set<T>() where T : class
        {

            return this.Set<T>();
        }
    }
    public interface IUnitOfWork
    {
        void Commit();
        IDbSet<T> Set<T>() where T : class;
    }
}
