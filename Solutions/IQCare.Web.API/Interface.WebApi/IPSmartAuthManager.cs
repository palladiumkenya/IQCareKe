using Entity.WebApi.PSmart;

namespace Interface.WebApi
{
    public interface IPSmartAuthManager
    {
        PsmartAuthUser LoginValidate(string username);
    }
}
