using System;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.UILogic
{
    public class ApiInboxManager : IApiInboxmanager
    {
        private IApiInboxmanager _apiInboxmanager = (IApiInboxmanager)Application.Presentation.ObjectFactory.CreateInstance("IQ.ApiLogic.Infrastructure.BusinessProcess.BPApiInbox, IQApiLogic");

        public int AddApiInbox(ApiInbox apiInbox)
        {
           ApiInbox inbox=new ApiInbox()
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

    }
}
