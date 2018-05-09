using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
    public class BServiceAreaIdentifiers : ProcessBase, IServiceAreaIdentifiers
    {
        public List<ServiceAreaIdentifiers> GetIdentifiersByServiceArea(int serviceArea)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var serviceAreaIdentifiers = unitOfWork.ServiceAreaIdentifiersRepository
                    .FindBy(x => x.ServiceAreaId == serviceArea).ToList();
                unitOfWork.Dispose();
                return serviceAreaIdentifiers;
            }
        }
    }
}
