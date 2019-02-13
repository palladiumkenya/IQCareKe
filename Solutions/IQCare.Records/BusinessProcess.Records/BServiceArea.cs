using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
   public  class BServiceArea:ProcessBase,IServiceArea
    {

        public ServiceArea   GetServiceArea(string name)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var serviceArea= unitOfWork.ServiceAreaIndicatorRepository
                    .FindBy(x => x.Name == name).FirstOrDefault();
                unitOfWork.Dispose();
                return serviceArea;
            }
        }
    }
}
