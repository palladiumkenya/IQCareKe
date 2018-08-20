using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository.Visit
{
    public class PatientVisitRepository : BaseRepository<PatientVisit>, IPatientVisitRepository
    {
        private RecordContext _context;
        public PatientVisitRepository() : this(new RecordContext())
        {

        }

        public PatientVisitRepository(RecordContext context) : base(context)
        {
            _context = context;
        }


    }
}
