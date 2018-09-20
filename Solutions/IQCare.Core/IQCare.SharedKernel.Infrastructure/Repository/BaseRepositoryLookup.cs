using System;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IQCare.SharedKernel.Infrastructure.Repository
{
    public class BaseRepositoryLookup<T> : IRepositoryLookup<T> where T : class 
    {
        protected internal DbContext Context;
        protected internal DbSet<T> DbSet;

        protected BaseRepositoryLookup(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public T Get(int id)
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

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> results = DbSet.AsNoTracking()
                .Where(predicate).ToList();
            return results;
        }
    }
}
