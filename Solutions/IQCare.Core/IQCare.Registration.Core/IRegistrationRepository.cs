using IQCare.Common.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Registration.Core
{
    public interface IRegistrationRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
