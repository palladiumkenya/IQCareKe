using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Linq;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Repository.Encounter
{
    public class NeonatalRepository: BaseRepository<PatientNeonatalHistory>, INeonatalRepository
    {
        private readonly GreencardContext _context;
        public NeonatalRepository():base(new GreencardContext())
        {

        }
        public NeonatalRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientNeonatalHistory> getNeonatalNotes(int personId, int patientMasterVisitId)
        {
            INeonatalRepository neonatalRepository = new NeonatalRepository();
            var neonatalNotesList = neonatalRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return neonatalNotesList.ToList();
        }
    }
}
