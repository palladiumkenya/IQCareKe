using Common.Data;
using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientLocationRepository :BaseRepository<PatientLocation>,IPatientLocationRepository
    {
        private PatientContext _context;
        public PatientLocationRepository(BaseContext context) : base(context)
        {

        }
    }
}
