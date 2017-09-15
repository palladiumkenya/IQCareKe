using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.CCC.Repository.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientHivTestingRepository : BaseRepository<PatientHivTesting>, IPatientHvTestingRepository
    {
        private GreencardContext _context;

 public PatientHivTestingRepository() : this(new GreencardContext())
        {
        }

        public PatientHivTestingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}