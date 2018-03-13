using IQCare.Common.Infrastructure;

namespace IQCare.Registration.Core
{
    public interface IRegistrationRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
