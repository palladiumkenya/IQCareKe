using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
   public class PatientPopulationRepository :BaseRepository<PatientPopulation>,IPatientPopulationRepository
   {
       private readonly PatientContext _context;

       public PatientPopulationRepository() : this(new PatientContext())
       {
           
       }

       public PatientPopulationRepository(PatientContext context) : base(context)
       {
           _context = context;
       }
   }
}
