using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BPersonExtendedLookupManager :ProcessBase ,IPersonExtendedLookupManager
    {
        public PersonExtLookup GetPatientDetailsByPersonId(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                PersonExtLookup patient = unitOfWork.PersonExtendedLookupRepository.FindBy(x => x.PersonId == personId)
                    .FirstOrDefault();
                return patient;
            }
        }
    }
}