using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientFamilyTestingRepository : BaseRepository<PatientFamilyTesting>, IPatientFamilyTestingRepository
    {
        public PatientFamilyTestingRepository(GreencardContext context) : base(context)
        {
        }

        public PatientFamilyTestingRepository() : this(new GreencardContext())
        {
        }

        public List<PatientFamilyTesting> GetByPatientId(int patientId)
        {
            IPatientFamilyTestingRepository patientFamilyTestingRepository = new PatientFamilyTestingRepository();
            List<PatientFamilyTesting> patientFamilyTestings = patientFamilyTestingRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientFamilyTestings;
        }
    }
}