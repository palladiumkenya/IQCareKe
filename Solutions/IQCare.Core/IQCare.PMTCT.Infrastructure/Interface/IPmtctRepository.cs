using IQCare.SharedKernel.Interfaces;

namespace IQCare.PMTCT.Infrastructure.Interface
{
    public interface IPmtctRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
    }
}