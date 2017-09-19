using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.Interface
{
    public interface IApiInteropSystemsManager
    {
        int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
        int EditApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
    }
}
