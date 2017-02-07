using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
   public class PatientEncounterRepository:BaseRepository<PatientEncounter>,IPatientEncounterRepository
   {

        private readonly GreencardContext _context;

        public PatientEncounterRepository():this(new GreencardContext())
       {
           
       }

       public PatientEncounterRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }

    }
}
