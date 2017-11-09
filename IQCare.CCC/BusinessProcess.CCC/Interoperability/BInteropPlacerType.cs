using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BInteropPlacerType : ProcessBase, IInteropPlacerTypeManager
    {
        public InteropPlacerType GetInteropPlacerTypeByName(string name)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var interopType = unitOfWork.InteropPlacerTypesRepository.FindBy(x => x.Name.ToLower() == name.ToLower())
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return interopType;
            }
        }
    }
}
