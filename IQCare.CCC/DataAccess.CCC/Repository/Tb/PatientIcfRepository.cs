using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientIcfRepository : BaseRepository<PatientIcf>, IPatientIcfRepository
    {
        private GreencardContext _context;

        public PatientIcfRepository() : this(new GreencardContext())
        {
        }

        public PatientIcfRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientIcf> GetByPatientId(int patientId)
        {
            IPatientIcfRepository patientIcfRepository = new PatientIcfRepository();
            List<PatientIcf> patientIcf = patientIcfRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientIcf;
        }
    }
}