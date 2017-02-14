using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.person
{
    public class PatientPopulationRepository :BaseRepository<PatientPopulation>,IPatientPopulationRepository
   {
       private readonly PersonContext _context;

       public PatientPopulationRepository() : this(new PersonContext())
       {
           
       }

       public PatientPopulationRepository(PersonContext context) : base(context)
       {
           _context = context;
       }

       public List<PatientPopulation> GetPatientPopulations(int patientId)
       {
           IPatientPopulationRepository patientPopulationRepository =new PatientPopulationRepository();
            List<PatientPopulation> myList =
                new List<PatientPopulation>
                {
                    patientPopulationRepository.FindBy(x => x.PersonId == patientId & x.DeleteFlag == true)
                        .OrderBy(x => x.Id)
                        .FirstOrDefault()
                };
            return myList;
        }
   }
}
