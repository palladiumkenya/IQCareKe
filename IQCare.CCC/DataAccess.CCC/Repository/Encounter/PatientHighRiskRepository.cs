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
    public class PatientHighRiskRepository : BaseRepository<PatientHighRisk>, IPatientHighRiskRepository
    {
        private readonly GreencardContext _context;

        public PatientHighRiskRepository() : this(new GreencardContext())
        {
        }

        public PatientHighRiskRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
    
    
}
