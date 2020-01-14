using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.CCC.Repository.visit
{
   public class PatientLabTrackerRepository : BaseRepository<PatientLabTracker>, IPatientLabTrackerRepository
    {
        private readonly GreencardContext _context;

        public PatientLabTrackerRepository():this(new GreencardContext())
       {

        }

        public PatientLabTrackerRepository(GreencardContext context) : base(context)
       {
            _context = context;
        }
        public List<PatientLabTracker> GetVlPendingCount(int facilityId)
        {
            var pending = "Pending";
            IPatientLabTrackerRepository patientLabTrackerRepository = new PatientLabTrackerRepository();
            List<PatientLabTracker> pendingVLCount = patientLabTrackerRepository.FindBy(p => p.FacilityId == facilityId &&
                                                                                 p.Results == pending &&
                                                                                 p.LabTestId == 3).ToList();
            return pendingVLCount;
        }
       
        public List<PatientLabTracker> GetVlCompleteCount(int facilityId)
        {
            var complete = "Complete";
            IPatientLabTrackerRepository patientLabTrackerRepository = new PatientLabTrackerRepository();
            List<PatientLabTracker> completeVLCount = patientLabTrackerRepository.FindBy(p => p.FacilityId == facilityId &
                                                                                 p.Results == complete &
                                                                                 p.LabTestId == 3).ToList();
            return completeVLCount;
        }
    }
}
