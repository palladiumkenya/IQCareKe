using System;
using System.Collections.Generic;
using System.Text;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.Interface
{
    public interface IApiInboxmanager
    {
        int AddApiInbox(ApiInbox apiInbox);
        int EditApiInbox(ApiInbox apiInbox);
        List<ApiInbox> GetAllUnprocessesMessage();

    }
}
