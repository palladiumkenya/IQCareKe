using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Repository.Encounter
{
   public  class PatientOIRepository : BaseRepository<PatientOI>, IPatientOIRepository
    {
        private readonly GreencardContext _context;

        public PatientOIRepository() : this(new GreencardContext())
        {
        }

        public PatientOIRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
