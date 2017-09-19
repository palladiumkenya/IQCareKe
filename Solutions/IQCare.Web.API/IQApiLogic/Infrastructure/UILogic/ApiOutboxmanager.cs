using System;
using System.Collections.Generic;
using System.Text;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.UILogic
{
    public class ApiOutboxmanager : IApiOutboxManager
    {
        private IApiOutboxManager _apiOutboxmanager = (IApiOutboxManager)Application.Presentation.ObjectFactory.CreateInstance("IQ.ApiLogic.Infrastructure.BusinessProcess.BPApiOutbox, IQApiLogic");


        public int AddApiOutbox(ApiOutbox apiOutbox)
        {
            ApiOutbox outbox=new ApiOutbox()
            {
                DateRead = apiOutbox.DateRead,
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

        public List<ApiOutbox> GetAllUnsentMessages()
        {
            return _apiOutboxmanager.GetAllUnsentMessages();
        }
    }
}

