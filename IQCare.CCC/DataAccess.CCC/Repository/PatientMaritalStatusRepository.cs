

using DataAccess.CCC.Interfaces;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository
{
    public class PatientMaritalStatusRepository :BaseRepository<PatientMaritalStatus>,IPatientMaritalStatusRepository
    {
        private readonly GreencardContext _context;

        public PatientMaritalStatusRepository() :this(new GreencardContext())
        {
            
        }

        public PatientMaritalStatusRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
