using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Data.Repository;
using VisitManagement.Core.Interfaces;
using VisitManagement.Core.Model;

namespace VisitManagement.Data.Repository
{
    public class PatientMasterVisitRepository :BaseRepository<PatientMasterVisit>, IPatientMasterVisitRepository
    {
        private readonly VisitContext _context;

        public PatientMasterVisitRepository():this(new VisitContext())
        { }
        public PatientMasterVisitRepository(BaseContext context) : base(context)
        {
        }

        public PatientMasterVisitRepository(VisitContext context) : base(context)
        {
            _context = context;
        }

        public PatientMasterVisit GetCurrentMasterVisitDetails(int patentId)
        {
            return _context
                .PatientMasterVisits
                .FirstOrDefault(x => x.PatientId== patentId);
        }

    }
}
