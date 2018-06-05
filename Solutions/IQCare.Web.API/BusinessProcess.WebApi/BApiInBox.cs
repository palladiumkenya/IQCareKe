using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi;
using Interface.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.WebApi
{
    public class BApiInBox : ProcessBase, IApiInboxManager
    {
        private int Result = 0;

        public int AddApiInbox(ApiInbox apiInbox)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInboxRepository.Add(apiInbox);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return apiInbox.Id;
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

        public List<ApiInbox> GetUnProcessedMessage()
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
