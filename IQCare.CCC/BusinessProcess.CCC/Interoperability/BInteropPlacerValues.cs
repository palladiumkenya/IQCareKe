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
        public int AddInteropPlacerValue(InteropPlacerValues interopPlacerValues)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.InteropPlacerValuesRepository.Add(interopPlacerValues);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return interopPlacerValues.Id;
            }
        }

        public InteropPlacerValues GetInteropPlacerValues(int interopPlacerTypeId, int identifierType, string placerValue)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var interopPlacerValues = unitOfWork.InteropPlacerValuesRepository.FindBy(x => x.PlacerValue == placerValue && x.InteropPlacerTypeId == interopPlacerTypeId && x.IdentifierType == identifierType).FirstOrDefault();
                unitOfWork.Dispose();
                return interopPlacerValues;
            }
        }
    }
}
