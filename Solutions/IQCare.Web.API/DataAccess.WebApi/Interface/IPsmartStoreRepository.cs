using DataAccess.Context;
using Entities.PSmart;
using Entity.WebApi.PSmart;

namespace DataAccess.WebApi.Interface
{
    public interface IPsmartStoreRepository:IRepository<Psmart_Store>
    {
        
    }
    public interface IPSmartTransactionLogRepository : IRepository<TransactionLog>
    {

    }

    //public interface IPSmartAuthRepository : IRepository<UserAuth>
    //{
    //    void Add(PsmartStore psmartStore);
    //}
}