using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Repository;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.BusinessProcess
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

        public List<ApiOutbox> GetAllUnsentMessages()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
              var outbound=   unitOfWork.ApiOutboxRepository.FindBy(x => x.DateSent == null);
                return outbound.ToList();
            }
        }
    }
}
