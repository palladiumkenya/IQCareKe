using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientDisclosureRepository : BaseRepository<PatientDisclosure>, IPatientDisclosureRepository
    {
        private readonly GreencardContext _context;

        public PatientDisclosureRepository():this(new GreencardContext())
       {

        }

        public PatientDisclosureRepository(GreencardContext context) : base(context)
       {
            _context = context;
        }
    }
}
