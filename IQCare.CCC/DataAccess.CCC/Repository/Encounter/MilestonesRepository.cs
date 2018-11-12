using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Linq;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Repository.Encounter
{
    public class MilestonesRepository: BaseRepository<PatientMilestone>, IMilestonesRepository
    {
        private readonly GreencardContext _context;
        public MilestonesRepository():base(new GreencardContext())
        {

        }
        public MilestonesRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<PatientMilestone> getPatientMilestones(int patientId)
        {
            IMilestonesRepository neonatalRepository = new MilestonesRepository();
            var milestonesList = neonatalRepository.GetAll().Where(x => x.PatientId == patientId);
            return milestonesList.ToList();
        }
        public List<PatientMilestone> getMilestoneAssessed(int milestoneAssessed)
        {
            IMilestonesRepository neonatalRepository = new MilestonesRepository();
            var milestoneList = neonatalRepository.GetAll().Where(x => x.MilestoneAssessedId == milestoneAssessed);
            return milestoneList.ToList();
        }
    }
}
