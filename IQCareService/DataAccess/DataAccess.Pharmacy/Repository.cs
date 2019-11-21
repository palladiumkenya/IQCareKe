using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

using System.Data.Entity;
using Entities.Common;
namespace DataAccess.Pharmacy
{
    public abstract class Repository<TEntity> : MarshalByRefObject where TEntity : class
    {
        internal PharmacyContext context;
        protected Repository()
        {

            context = new PharmacyContext();
        }
        /// <summary>
        /// Gets the total records.
        /// </summary>
        /// <value>
        /// The total records.
        /// </value>
        public virtual int TotalRecords
        {
            get { return context.Set<TEntity>().Count(); }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(object id)
        {
            var entityToDelete = context.Set<TEntity>().Find(id);
            context.Set<TEntity>().Remove(entityToDelete);
            context.SaveChanges();
        }
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().Where(filter);
        }
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity Find(object id)
        {
            return context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Save(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            context.Database.Log = Console.Write;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            context.Database.Log = Console.Write;
        }
        public abstract List<TEntity> GetAllFilterd(IFilter filter);
    }
}
