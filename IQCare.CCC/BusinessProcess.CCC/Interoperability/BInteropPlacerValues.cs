using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BInteropPlacerValues : ProcessBase, IInteropPlacerValuesManager
    {
        public InteropPlacerValues GetInteropPlacerValues(int interopPlacerTypeId, int identifierType, int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var interopPlacerValues = unitOfWork.InteropPlacerValuesRepository.FindBy(x =>
                    x.EntityId == entityId && x.InteropPlacerTypeId == interopPlacerTypeId &&
                    x.IdentifierType == identifierType).FirstOrDefault();
                unitOfWork.Dispose();
                return interopPlacerValues;
            }
        }
    }
}
