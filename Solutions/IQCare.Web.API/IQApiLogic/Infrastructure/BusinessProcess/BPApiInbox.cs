using DataAccess.Base;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Repository;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.BusinessProcess
{
    class BPApiInbox : ProcessBase, IApiInboxmanager
    {
        private int Result = 0;

        public int AddApiInbox(ApiInbox apiInbox)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInboxRepository.Add(apiInbox);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int EditApiInbox(ApiInbox apiInbox)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInboxRepository.Update(apiInbox);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }
        
    }
}
