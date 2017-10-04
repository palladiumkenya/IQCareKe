using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi;
using Interface.WebApi;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.WebApi
{
    public class BApiOutBox: ProcessBase, IApiOutboxManager
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

        public List<ApiOutbox> GetUnsentMessages()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                var outbound = unitOfWork.ApiOutboxRepository.FindBy(x => x.DateSent == null);
                return outbound.ToList();
            }
        }
    }
}
