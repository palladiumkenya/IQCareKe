using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Triage;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientLabDetailsRepository : BaseRepository<LabDetailsEntity>, IPatientLabDetailsRepository
    {
        private readonly GreencardContext _context;

        public PatientLabDetailsRepository() : this(new GreencardContext())
        {

        }

        public PatientLabDetailsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


    }
}
