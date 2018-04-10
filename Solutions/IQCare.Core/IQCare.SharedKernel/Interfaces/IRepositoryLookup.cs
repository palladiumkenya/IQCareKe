using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IQCare.SharedKernel.Interfaces
{
    public interface IRepositoryLookup<T> where T : class 
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void SaveChanges();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
