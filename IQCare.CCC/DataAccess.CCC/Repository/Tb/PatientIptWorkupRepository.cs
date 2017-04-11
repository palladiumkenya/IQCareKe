using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientIptWorkupRepository : BaseRepository<PatientIptWorkup>, IPatientIptWorkupRepository
    {
        private GreencardContext _context;

        public PatientIptWorkupRepository() : this(new GreencardContext())
        {
        }

        public PatientIptWorkupRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientIptWorkup> GetByPatientId(int patientId)
        {
            IPatientIptWorkupRepository patientIptWorkupRepository = new PatientIptWorkupRepository();
            List<PatientIptWorkup> patientIptWorkup = patientIptWorkupRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientIptWorkup;
        }
    }