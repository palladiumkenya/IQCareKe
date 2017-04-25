
using DataAccess.CCC.Context;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.Patient
{
   public class PatientContactRepository :BaseRepository<PersonContact>,IPatientContactRepository
    {
        private readonly GreencardContext _context;

        public PatientContactRepository() :this(new GreencardContext())
        {

        }

        public PatientContactRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }

    public class PatientContactRepository1 : BaseRepository<PersonContact>, IRepository<PersonContact>
    {
        private readonly GreencardContext _context;

        public PatientContactRepository1() : this(new GreencardContext())
        {

        }

        public PatientContactRepository1(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
