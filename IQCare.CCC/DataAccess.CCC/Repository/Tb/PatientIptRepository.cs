using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientIptRepository : BaseRepository<PatientIpt>, IPatientIptRepository
    {
        private GreencardContext _context;

        public PatientIptRepository() : this(new GreencardContext())
        {
        }

        public PatientIptRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientIpt> GetByPatientId(int patientId)
        {
            IPatientIptRepository patientIptRepository = new PatientIptRepository();
            List<PatientIpt> patientIpt = patientIptRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientIpt;
        }
    }
}