using Entity.WebApi;
using Interface.WebApi;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic
{
    public class ApiInboxManager : IApiInboxManager
    {
        private IApiInboxManager _apiInboxmanager = (IApiInboxManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPApiInbox, BPApiInbox");

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

        public List<ApiInbox> GetUnProcessedMessage()
        {
            return _apiInboxmanager.GetUnProcessedMessage();
        }
    }
}
