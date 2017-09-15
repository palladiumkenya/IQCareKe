using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
   public class PatientEncounterRepository:BaseRepository<PatientEncounter>,IPatientEncounterRepository
   {

        private GreencardContext _context;

        public PatientEncounterRepository():this(new GreencardContext())
       {
           
       }

       public PatientEncounterRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }


    }
}
