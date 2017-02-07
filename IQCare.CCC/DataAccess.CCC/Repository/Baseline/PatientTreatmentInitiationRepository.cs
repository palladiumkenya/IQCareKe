using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientTreatmentInitiationRepository:BaseRepository<PatientArtInitiationBaseline>,IPatientTreatmentInitiationRepository
    {
        private readonly GreencardContext _context;

        public PatientTreatmentInitiationRepository() : this(new GreencardContext())
        {
            
        }

        public PatientTreatmentInitiationRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

    }
}
