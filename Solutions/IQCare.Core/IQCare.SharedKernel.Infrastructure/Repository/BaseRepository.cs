using System.Collections.Generic;
using IQCare.SharedKernel.Interfaces;
using IQCare.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;

namespace IQCare.SharedKernel.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId>:IRepository<T, TId> where T : Entity<TId>
    {
        protected internal DbContext Context;
        protected internal DbSet<T> DbSet;

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public T Get(TId id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}