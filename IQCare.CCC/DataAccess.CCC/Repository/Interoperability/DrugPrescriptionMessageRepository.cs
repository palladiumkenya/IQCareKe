using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Repository.Interoperability
{
    class DrugPrescriptionMessageRepository: BaseRepository<DrugPrescriptionEntity>, IDrugPrescriptionMessageRepository
    {

        private readonly LookupContext _context;

        public DrugPrescriptionMessageRepository() : this(new LookupContext())
        {

        }

        public DrugPrescriptionMessageRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
