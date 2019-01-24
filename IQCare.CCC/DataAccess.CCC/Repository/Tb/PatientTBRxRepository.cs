using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientTBRxRepository : BaseRepository<PatientTBRx>, IPatientTBRxRepository
    {
        private GreencardContext _context;

        public PatientTBRxRepository() : this(new GreencardContext())
        {
        }

        public PatientTBRxRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientTBRx> GetByPatientId(int patientId)
        {
            IPatientTBRxRepository patientTBRxRepository = new PatientTBRxRepository();
            List<PatientTBRx> patientTBRx = patientTBRxRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientTBRx;
        }
    }
}
