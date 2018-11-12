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
    public class PatientSexualHistoryRepository : BaseRepository<PatientSexualHistory>, IPatientSexualHistoryRepository
    {
        private readonly GreencardContext _context;

        public PatientSexualHistoryRepository () : this(new GreencardContext())
        {
        }

        public PatientSexualHistoryRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
