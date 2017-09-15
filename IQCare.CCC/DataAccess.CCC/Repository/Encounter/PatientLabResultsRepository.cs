using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientLabResultsRepository : BaseRepository<LabResultsEntity>, IPatientLabResultsRepository
    {
        private GreencardContext _context;
        public PatientLabResultsRepository() : base(new GreencardContext())
        {
        }

        public PatientLabResultsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}

namespace DataAccess.CCC.Repository.visit
{
    public class PatientLabResultsRepository : BaseRepository<LabResultsEntity>, IPatientLabResultsRepository
    {
        private readonly GreencardContext _context;

        public PatientLabResultsRepository() : this(new GreencardContext())
        {

        }

        public PatientLabResultsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


    }
}