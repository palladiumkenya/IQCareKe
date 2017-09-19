using System;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.UILogic
{
    public class ApiInteropManager : IApiInteropSystemsManager
    {
            private IApiInteropSystemsManager _apiInteropSystemsManager  = (IApiInteropSystemsManager)Application.Presentation.ObjectFactory.CreateInstance("IQ.ApiLogic.Infrastructure.BusinessProcess.BPApiInteropSystems, IQApiLogic");

        public int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem)
        {
            return _apiInteropSystemsManager.AddApiInteroperabilitySystems(apiInteropSystem);
        }

        public int EditApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem)
        {
            return _apiInteropSystemsManager.EditApiInteroperabilitySystems(apiInteropSystem);
        }
    }
}
