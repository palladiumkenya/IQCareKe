using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records;

namespace DataAccess.Records.Repository.Lookup
{
    public class PatientLookupRepository : BaseRepository<PatientLookup>, IPatientLookupRepository
    {
        private readonly LookupContext _context;

        public PatientLookupRepository() : this(new LookupContext())
        {

        }

        public PatientLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

        public PatientLookup GetGenderId(int patientId)
        {
            IPatientLookupRepository patientRepository = new PatientLookupRepository();
            var genderId = patientRepository.FindBy(x => x.Id == patientId).FirstOrDefault();
            return genderId;

        }
        public PatientLookup GetPatientById(int patientId)
        {
            IPatientLookupRepository patientRepository = new PatientLookupRepository();
            var patient = patientRepository.FindBy(x => x.Id == patientId).FirstOrDefault();
            return patient;

        }
    }
}
