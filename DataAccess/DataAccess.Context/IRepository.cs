using Entities.Common;
using System.Collections.Generic;

namespace DataAccess.Context
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T AddRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
