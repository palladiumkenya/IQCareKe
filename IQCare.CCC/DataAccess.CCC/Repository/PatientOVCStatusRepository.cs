using DataAccess.CCC.Interfaces;
using DataAccess.Context;
using Entities.PatientCore;
namespace DataAccess.CCC.Repository
{
    public class PatientOVCStatusRepository :BaseRepository<PatientOVCStatus>,IPatientOVCStatusRepository
    {
        private readonly GreencardContext _context;

        public PatientOVCStatusRepository():this(new GreencardContext())
        { }

        public PatientOVCStatusRepository(GreencardContext context) :base(context)
        {
            _context = context;
        }
    }
}
