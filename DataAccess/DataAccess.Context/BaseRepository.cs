using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Context
{

    public abstract class BaseRepository<T> : MarshalByRefObject, IRepository<T> where T : class
    {
        protected internal BaseContext _baseContext;
        internal IDbSet<T> _dbSet;

        public BaseRepository(BaseContext context)
        {
            _baseContext = context;
            _dbSet = _baseContext.Set<T>();
        }
        public BaseRepository() 
        {
            _baseContext = new BaseContext();
            _dbSet = _baseContext.Set<T>();
        }
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter) {         
            return _baseContext.Set<T>().Where(filter);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual int AddRange(IEnumerable<T> entity)
        {
            return entity.Count();
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
                _dbSet.Attach(entity);
                _baseContext.Entry(entity).State = EntityState.Modified;            
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> results = _dbSet.AsNoTracking()
               .Where(predicate).ToList();
            return results;
        }
    }
}


