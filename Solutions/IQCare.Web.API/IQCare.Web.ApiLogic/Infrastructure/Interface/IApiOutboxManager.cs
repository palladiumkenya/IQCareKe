using System.Collections.Generic;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Interface
{
    public interface IApiOutboxManager
    {
        int AddApiOutbox(ApiOutbox apiOutbox);
        int AddApiProcessed(ApiOutbox apiOutbox);
        List<ApiOutbox> GetAllUnsentMessages();
    }
}
