using Entity.WebApi;

namespace Interface.WebApi
{
    public interface IApiInteropSystemsManager
    {
        int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
        int EditApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
    }
}
