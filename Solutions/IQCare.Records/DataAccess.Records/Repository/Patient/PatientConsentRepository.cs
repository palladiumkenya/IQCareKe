using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository.Patient
{
    public class PatientConsentRepository:BaseRepository<PatientConsent>,IPatientConsentRepository
    {
        private RecordContext _context;

        public PatientConsentRepository():this(new RecordContext())
        { }
        public PatientConsentRepository(RecordContext context):base(context)
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
