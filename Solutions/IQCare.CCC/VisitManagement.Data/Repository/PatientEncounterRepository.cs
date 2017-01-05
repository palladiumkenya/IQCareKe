using System.Linq;
using System.Data.Entity;
using System;
using Common.Data;
using Common.Data.Repository;
using VisitManagement.Core.Interfaces;
using VisitManagement.Core.Model;

namespace VisitManagement.Data.Repository
{
   
    public class PatientEncounterRepository : BaseRepository<PatientEncounter>, IPatientEncounterRepository
    {
        private readonly VisitContext _context;

        public PatientEncounterRepository() :this(new VisitContext())
        {
        }

        public PatientEncounterRepository(VisitContext context) : base(context)
        {
            _context = context;
        }

        public PatientEncounter GetPatientEncounterByService(int serviceId)
        {
            return _context
                .PatientEncounters
                .FirstOrDefault(x => x.ServiceId == serviceId);
        }

        public PatientEncounter GetPatientEncounterByMasterVisit(int patientmastervisitId)
        {
            return _context
                .PatientEncounters
                .FirstOrDefault(x => x.PatientMasterVisitId == patientmastervisitId);
        }

        public PatientEncounter GetPatientEncounterByDateRangEncounter(DateTime dateFrom, DateTime dateTo)
        {
           return _context
                .PatientEncounters
                .FirstOrDefault(x=>x.EncounterStarTime==dateFrom && x.EncounterEndTime==dateTo);
        }
    }
}
