using System.Collections.Generic;
using IQCare.SharedKernel.Model;

namespace IQCare.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void SaveChanges();

    }
}