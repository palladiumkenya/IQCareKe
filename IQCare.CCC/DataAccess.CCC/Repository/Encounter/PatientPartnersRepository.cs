using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Repository.Encounter
{
   public class PatientPartnersRepository : BaseRepository<PatientPartner>, IPatientPartnersRepository
    {
        private readonly GreencardContext _context;

        public PatientPartnersRepository() : this(new GreencardContext())
        {
        }

        public PatientPartnersRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
