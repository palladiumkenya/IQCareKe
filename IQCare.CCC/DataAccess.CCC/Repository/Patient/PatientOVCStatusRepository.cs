using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;
namespace DataAccess.CCC.Repository.Patient
{
    public class PatientOVCStatusRepository :BaseRepository<PatientOVCStatus>,IPatientOvcStatusRepository
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
