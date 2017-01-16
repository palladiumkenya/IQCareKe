using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
{
   public class PersonRepository:BaseRepository<Person>,IPersonRepository
    {
        private readonly personContext _context;

        public PersonRepository() : this(new PatientContext())
        {

        }

        public PersonRepository(personContext context) : base(context)
        {
            _context = context;
        }
    }
}
