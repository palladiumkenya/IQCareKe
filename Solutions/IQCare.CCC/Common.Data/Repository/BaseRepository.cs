using System;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Core.Interfaces;
using Common.Core.Model;

namespace Common.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected internal BaseContext Context;
        internal IDbSet<T> Dbset;


        public BaseRepository(BaseContext context)
        {
            Context = context; Dbset = Context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return Dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Dbset;
        }

        public void Add(T entity)
        {
             Dbset.Add(entity);
        }

        public T AddRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
    }
}
