using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientLinkageRepository : BaseRepository<PatientLinkage>, IPatientLinkageRepository
    {
        private GreencardContext _context;

        public PatientLinkageRepository() : this(new GreencardContext())
        {
        }

        public PatientLinkageRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
