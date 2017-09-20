using System.Collections.Generic;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.UiLogic
{
    public class ApiInboxManager : IApiInboxmanager
    {
        private IApiInboxmanager _apiInboxmanager = (IApiInboxmanager)Application.Presentation.ObjectFactory.CreateInstance("IQ.ApiLogic.Infrastructure.BusinessProcess.BPApiInbox, IQApiLogic");

        public int AddApiInbox(ApiInbox apiInbox)
        {
           ApiInbox inbox = new ApiInbox()
           {
               DateReceived = apiInbox.DateReceived,
               Message = apiInbox.Message
           };

            return _apiInboxmanager.AddApiInbox(apiInbox);

        }

        public int EditApiInbox(ApiInbox apiInbox)
        {
           ApiInbox inbox=new ApiInbox()
           {
               Id = apiInbox.Id,
               Processed = apiInbox.Processed,
               LogMessage = apiInbox.LogMessage
           };

            return _apiInboxmanager.EditApiInbox(inbox);
        }

        public List<ApiInbox> GetAllUnprocessesMessage()
        {
            return _apiInboxmanager.GetAllUnprocessesMessage();
        }
    }
}
