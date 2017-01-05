using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DataAccess.Lookup
{
    public abstract class Repository<TEntity> : MarshalByRefObject where TEntity : class
    {
        internal LookupContext context;
        protected Repository()
        {

            context = new LookupContext();
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

        
        protected virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().Where(filter);
        }
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public abstract TEntity Find(int id, string lookname, string lookcategory);
        

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TEntity> GetAll(string lookname, string lookcategory);
        public abstract IEnumerable<TEntity> GetAll(string lookname);
    }
}
