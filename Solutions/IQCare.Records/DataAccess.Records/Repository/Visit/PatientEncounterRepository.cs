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
    public class PatientEncounterRepository : BaseRepository<PatientEncounter>, IPatientEncounterRepository
    {

        private RecordContext _context;

        public PatientEncounterRepository() : this(new RecordContext())
        {

        }

        public PatientEncounterRepository(RecordContext context) : base(context)
        {
            _context = context;
        }


    }
}
