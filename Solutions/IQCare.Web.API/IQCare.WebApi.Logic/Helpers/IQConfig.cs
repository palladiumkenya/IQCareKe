using Application.Presentation;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.Helpers
{
    public class IQConfig
    {
        private readonly IIQConfig iQConfig = (IIQConfig)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BIQConfig, BusinessProcess.WebApi");

        public string GetIQCareConnectionString()
        {
            return iQConfig.EMRConnectionString;
        }
    }
}
