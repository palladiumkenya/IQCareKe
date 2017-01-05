
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.Patient
{
   public class PatientContactRepository :BaseRepository<PatientContact>,IPatientContactRepository
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

    public class PatientContactRepository1 : BaseRepository<PatientContact>, IRepository<PatientContact>
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
