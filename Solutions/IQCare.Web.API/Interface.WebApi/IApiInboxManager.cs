using Entity.WebApi;
using System.Collections.Generic;

namespace Interface.WebApi
{
    public interface IApiInboxManager
    {
        int AddApiInbox(ApiInbox apiInbox);
        int EditApiInbox(ApiInbox apiInbox);
        List<ApiInbox> GetUnProcessedMessage();
    }
}
