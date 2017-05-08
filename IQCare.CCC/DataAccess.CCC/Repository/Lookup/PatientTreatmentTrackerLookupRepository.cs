using System;
using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientTreatmentTrackerLookupRepository : BaseRepository<PatientTreamentTrackerLookup>, IPatientTreatmentTrackerLookupRepository
    {
        private readonly LookupContext _context;

        public PatientTreatmentTrackerLookupRepository() : this(new LookupContext())
        {
        }

        public PatientTreatmentTrackerLookupRepository(LookupContext context) : base(context)
       {
            _context = context;

        }

        public PatientTreamentTrackerLookup GetCurrentPatientRegimen(int patientId)
        {
            // var patientRegimen=_context.PatientTreamentTrackerLookups.Find(x=>x.)
            throw new NotImplementedException();
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentInterrupList(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSubstitutionList(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSwitchesList(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
