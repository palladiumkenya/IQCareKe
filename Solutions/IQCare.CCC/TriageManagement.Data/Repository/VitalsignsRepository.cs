using System;
using Common.Core.Interfaces;
using Common.Data.Repository;
using TriageManagement.Core.Interfaces;
using TriageManagement.Core.Model;
using VisitManagement.Data;

namespace TriageManagement.Data.Repository
{
    public class VitalsignsRepository :BaseRepository<PatientVitals>,IVitalSignsRepository
    {
       private readonly VisitContext _context;

        public VitalsignsRepository() :this(new VisitContext())
        {
        }
        public VitalsignsRepository(VisitContext context) : base(context)
        {
            _context = context;
        }

        public PatientVitals GetCurrentPatientVitals(int patientId)
        {
            throw new NotImplementedException();
        }

        public PatientVitals GetPatientVitalsHistoryByDateRange(DateTime vitalsFromDate, DateTime vitalsToDate)
        {
            throw new NotImplementedException();
        }
    }
}
