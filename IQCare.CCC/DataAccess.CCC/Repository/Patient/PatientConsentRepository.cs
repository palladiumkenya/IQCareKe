using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Consent;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientConsentRepository : BaseRepository<PatientConsent>, IPatientConsentRepository
    {
        private GreencardContext _context;

        public PatientConsentRepository() : this(new GreencardContext())
        {
        }

        public PatientConsentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            IPatientConsentRepository patientConsentRepository = new PatientConsentRepository();
            List<PatientConsent> patientConsent = patientConsentRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientConsent;
        }

    }
}