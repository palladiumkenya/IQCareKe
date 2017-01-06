using Application.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Context
{

    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected internal BaseContext _baseContext;
        internal IDbSet<T> _dbSet;
        public BaseRepository(BaseContext context)
        {
            _baseContext = context;
            _dbSet = _baseContext.Set<T>();
        }
        internal void CloseDecryptedSession()
        {
            _baseContext.Database.ExecuteSqlCommand("pr_CloseDecryptedSession");
        }
        internal void OpenDecryptedSession()
        {
            _baseContext.Database.ExecuteSqlCommand("pr_OpenDecryptedSession @Password",
                new SqlParameter("Password", ApplicationAccess.DBSecurity));
        }
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> results = _dbSet
                .Where(predicate).ToList();

            return results;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T AddRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
    }
}


