using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientWhoStageRepository:BaseRepository<PatientWhoStage>, IPatientWHOStageRepository
    {
        private readonly GreencardContext _context;

        public PatientWhoStageRepository() : this(new GreencardContext())
        {
        }

        public PatientWhoStageRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
