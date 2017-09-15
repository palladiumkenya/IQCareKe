using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientIptOutcomeRepository : BaseRepository<PatientIptOutcome>, IPatientIptOutcomeRepository
    {
        private GreencardContext _context;

        public PatientIptOutcomeRepository() : this(new GreencardContext())
        {
        }

        public PatientIptOutcomeRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientIptOutcome> GetByPatientId(int patientId)
        {
            IPatientIptOutcomeRepository patientIptOutcomeRepository = new PatientIptOutcomeRepository();
            List<PatientIptOutcome> patientIptOutcome = patientIptOutcomeRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientIptOutcome;
        }
    }
}