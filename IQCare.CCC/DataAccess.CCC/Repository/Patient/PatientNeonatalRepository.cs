using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientNeonatalRepository : BaseRepository<PatientMilestone>, IPatientNeonatalRepository
    {
        private readonly GreencardContext _context;

        public PatientNeonatalRepository() : this(new GreencardContext())
        {

        }
        public PatientNeonatalRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }  
    }
}
