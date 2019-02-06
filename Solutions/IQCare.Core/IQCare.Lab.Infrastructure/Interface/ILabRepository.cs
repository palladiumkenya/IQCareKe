using IQCare.SharedKernel.Interfaces;

namespace IQCare.Lab.Infrastructure.Interface
{
    public interface ILabRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
    }
}