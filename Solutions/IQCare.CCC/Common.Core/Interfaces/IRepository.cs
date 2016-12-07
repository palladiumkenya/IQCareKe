using System.Collections.Generic;
using Common.Core.Model;

namespace Common.Core.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}