using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.HIVEducation;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC;

namespace DataAccess.CCC.Repository.HIVEducation
{
    public class PatientHIVEducationFollowupRepository : BaseRepository<HIVEducationFollowup>, IHIVEducationRepository
    {
        private readonly GreencardContext _context;
        public PatientHIVEducationFollowupRepository() : this(new GreencardContext())
        {

        }
        public PatientHIVEducationFollowupRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
