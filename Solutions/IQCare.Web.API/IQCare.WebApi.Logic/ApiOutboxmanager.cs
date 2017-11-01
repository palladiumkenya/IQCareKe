using Entity.WebApi;
using Interface.WebApi;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic
{
    public class ApiOutboxmanager : IApiOutboxManager
    {
        private IApiOutboxManager _apiOutboxmanager = (IApiOutboxManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BApiOutBox, BusinessProcess.WebApi");


        public int AddApiOutbox(ApiOutbox apiOutbox)
        {
            ApiOutbox outbox=new ApiOutbox()
            {
                DateSent = apiOutbox.DateSent,
                RecepientId = apiOutbox.RecepientId,
                AttemptCount = 0,
                Message = apiOutbox.Message
            };

            return  _apiOutboxmanager.AddApiOutbox(outbox);
        }

        public int AddApiProcessed(ApiOutbox apiOutbox)
        {
            ApiOutbox outbox = new ApiOutbox()
            {
                Id = apiOutbox.Id,
                DateSent = apiOutbox.DateSent
                
            };

            return _apiOutboxmanager.AddApiProcessed(outbox);
        }

        public List<ApiOutbox> GetUnsentMessages()
        {
            return _apiOutboxmanager.GetUnsentMessages();
        }
    }
}

