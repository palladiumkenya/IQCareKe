using System.Collections.Generic;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Interface
{
    public interface IApiInboxmanager
    {
        int AddApiInbox(ApiInbox apiInbox);
        int EditApiInbox(ApiInbox apiInbox);
        List<ApiInbox> GetAllUnprocessesMessage();

    }
}
