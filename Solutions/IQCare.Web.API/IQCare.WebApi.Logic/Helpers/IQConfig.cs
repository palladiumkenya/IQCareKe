using Application.Common;
using Application.Presentation;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.Helpers
{
    public class IQConfig
    {
        private readonly IIQConfig iQConfig = (IIQConfig)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BIQConfig, BusinessProcess.WebApi");

        public string GetIQCareConnectionString()
        {
            Utility objUtil = new Utility();
            return  objUtil.Decrypt(iQConfig.EMRConnectionString);
        }
    }
}
