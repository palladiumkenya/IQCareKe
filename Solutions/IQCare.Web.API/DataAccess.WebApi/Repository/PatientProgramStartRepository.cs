using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class PatientProgramStartRepository : BaseRepository<PatientProgramStart>, IPatientProgramStartRepository
    {
        private readonly PsmartContext _context;
        public PatientProgramStartRepository()
        {
        }

        public PatientProgramStartRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}