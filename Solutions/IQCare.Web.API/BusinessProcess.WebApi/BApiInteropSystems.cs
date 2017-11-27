using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi;
using Interface.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.WebApi
{
   public class BApiInteropSystems:ProcessBase, IApiInteropSystemsManager
    {
        public int Result = 0;

        public int AddApiInteroperabilitySystems(ApiInteropSystem apiInteropSystem)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApiContext()))
            {
                unitOfWork.ApiInteropSystemsRepository.Add(apiInteropSystem);
                Result = unitOfWork.Complete();
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
