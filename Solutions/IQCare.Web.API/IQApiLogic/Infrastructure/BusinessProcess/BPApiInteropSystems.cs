using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Base;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Repository;
using IQ.ApiLogic.Infrastructure.Interface;
using IQ.ApiLogic.Model;

namespace IQ.ApiLogic.Infrastructure.BusinessProcess
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
