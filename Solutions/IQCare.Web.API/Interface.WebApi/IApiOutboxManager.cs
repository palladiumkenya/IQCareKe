using Entity.WebApi;
using System.Collections.Generic;

namespace Interface.WebApi
{
    public interface IApiOutboxManager
    {
        int AddApiOutbox(ApiOutbox apiOutbox);
        int AddApiProcessed(ApiOutbox apiOutbox);
        List<ApiOutbox> GetUnsentMessages();
    }
}
