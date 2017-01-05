using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.Patient
{
   public class PatientPopulationRepository :BaseRepository<PatientPopulation>,IPatientPopulationRepository
   {
       private readonly GreencardContext _context;

       public PatientPopulationRepository() : this(new GreencardContext())
       {
           
       }

       public PatientPopulationRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }
   }
}
