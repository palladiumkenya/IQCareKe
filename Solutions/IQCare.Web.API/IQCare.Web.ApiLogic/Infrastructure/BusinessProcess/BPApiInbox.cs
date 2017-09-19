using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Repository;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.BusinessProcess
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

        public List<ApiInbox> GetAllUnprocessesMessage()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                var inbox = unitOfWork.ApiInboxRepository.FindBy(x => x.DateProcessed == null);
                unitOfWork.Dispose();
                return inbox.ToList();
            }
        }
    }
}
