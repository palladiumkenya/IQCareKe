﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Context
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        int AddRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Filter(Expression<Func<T, bool>> filter);
    }
}
