using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientLookupRepository:BaseRepository<PatientLookup>,IPatientLookupRepository
    {
        private readonly LookupContext _context;

        public PatientLookupRepository():this(new LookupContext())
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
    public class PatientRelationshipViewRepository:BaseRepository<PatientRelationshipDTO>, IPatientRelationshipViewRepository
    {
        private readonly ViewContext _context;
        public PatientRelationshipViewRepository(ViewContext context) : base(context)
        {
            _context = context;
        }
    }

    }
