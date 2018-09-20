using IQCare.SharedKernel.Interfaces;

namespace IQCare.Common.Core.Interfaces.Repositories
{
    public interface ICommonRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
    }
}