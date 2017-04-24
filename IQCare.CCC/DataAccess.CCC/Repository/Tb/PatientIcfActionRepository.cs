using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Tb;
using DataAccess.Context;
using Entities.CCC.Tb;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Tb
{
    public class PatientIcfActionRepository : BaseRepository<PatientIcfAction>, IPatientIcfActionRepository
    {
        private GreencardContext _context;

        public PatientIcfActionRepository() : this(new GreencardContext())
        {
        }

        public PatientIcfActionRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientIcfAction> GetByPatientId(int patientId)
        {
            IPatientIcfActionRepository patientIcfActionRepository = new PatientIcfActionRepository();
            List<PatientIcfAction> patientIcfAction = patientIcfActionRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientIcfAction;
        }
    }
}