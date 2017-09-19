using DataAccess.Base;
using IQCare.Web.ApiLogic.Infrastructure.Context;
using IQCare.Web.ApiLogic.Infrastructure.Core.Repository;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Model;

namespace IQCare.Web.ApiLogic.Infrastructure.BusinessProcess
{
   public class BPApiInteropSystems :ProcessBase,IApiInteropSystemsManager
   {
       public int Result = 0;

        public int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInteropSystemsRepository.Add(apiInteropSystem);
                Result=unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int EditApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInteropSystemsRepository.Update(apiInteropSystem);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }
    }
}
