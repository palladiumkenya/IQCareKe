using DataAccess.Base;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Repository;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.BusinessProcess
{
    public class BPApiOutbox : ProcessBase, IApiOutboxManager
    {
        private int Result = 0;

        public int AddApiOutbox(ApiOutbox apiOutbox)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiOutboxRepository.Add(apiOutbox);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }

        }

        public int AddApiProcessed(ApiOutbox apiOutbox)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiOutboxRepository.Update(apiOutbox);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }
    }
}
