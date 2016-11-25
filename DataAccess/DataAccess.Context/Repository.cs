using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using Entities.Common;
using System.Data.SqlClient;
using Application.Common;

namespace DataAccess.Context
{
    public abstract class Repository<TEntity> : MarshalByRefObject where TEntity : class
    {
        internal LabContext labContext;
        protected Repository()
        {
         
            labContext = new LabContext();
        }
        /// <summary>
        /// Closes the decrypted session.
        /// </summary>
        internal void CloseDecryptedSession()
        {
            labContext.Database.ExecuteSqlCommand("pr_CloseDecryptedSession");
        }
        
        internal void OpenDecryptedSession()
        {
            labContext.Database.ExecuteSqlCommand("pr_OpenDecryptedSession @Password",
                new SqlParameter("Password", ApplicationAccess.DBSecurity));
        }
        /// <summary>
        /// Gets the total records.
        /// </summary>
        /// <value>
        /// The total records.
        /// </value>
        public virtual int TotalRecords
        {
            get { return labContext.Set<TEntity>().Count(); }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(object id)
        {
            var entityToDelete = labContext.Set<TEntity>().Find(id);
            labContext.Set<TEntity>().Remove(entityToDelete);
            labContext.SaveChanges();
        }
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            return labContext.Set<TEntity>().Where(filter);
        }
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity Find(object id)
        {
            return labContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return labContext.Set<TEntity>();
        }
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Save(TEntity entity)
        {
            labContext.Set<TEntity>().Add(entity);
            labContext.SaveChanges();
            labContext.Database.Log = Console.Write;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            labContext.Set<TEntity>().Attach(entity);
            labContext.Entry(entity).State = EntityState.Modified;
            labContext.SaveChanges();
            labContext.Database.Log = Console.Write;
        }
        public abstract List<TEntity> GetAllFilterd(IFilter filter);
        
    }
}
