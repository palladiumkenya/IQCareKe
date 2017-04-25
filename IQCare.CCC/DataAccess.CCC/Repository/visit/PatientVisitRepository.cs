using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Repository.visit
{
    public class PatientVisitRepository : BaseRepository<PatientVisit>, IPatientVisitRepository
    {
        private GreencardContext _context;
        public PatientVisitRepository() : this(new GreencardContext())
        {

        }

        public PatientVisitRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


    }
}
