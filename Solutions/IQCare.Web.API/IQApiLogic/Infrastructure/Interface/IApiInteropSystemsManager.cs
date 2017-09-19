using System;
using System.Collections.Generic;
using System.Text;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.Interface
{
    public interface IApiInteropSystemsManager
    {
        int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
        int EditApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem);
    }
}
