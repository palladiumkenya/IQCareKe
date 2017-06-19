using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace BusinessProcess.CCC.Enrollment
{
    public class BServiceAreaIdentifiers : ProcessBase, IServiceAreaIdentifiers
    {
        public List<ServiceAreaIdentifiers> GetIdentifiersByServiceArea(int serviceArea)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var serviceAreaIdentifiers = unitOfWork.ServiceAreaIdentifiersRepository
                    .FindBy(x => x.ServiceAreaId == serviceArea).ToList();
                unitOfWork.Dispose();
                return serviceAreaIdentifiers;
            }
        }
    }
}
