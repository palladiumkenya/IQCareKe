using System.Collections.Generic;
using Common.Core.Model;

namespace Common.Core.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        void Add(T entity);
        T AddRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}