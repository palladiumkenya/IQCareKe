using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Repository.Patient;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
{
    public class PersonContactRepository:BaseRepository<PersonContact>,IPersonContactRepository
    {
        private readonly PersonContext _context;

        public PersonContactRepository() : this(new PersonContext())
        {
            
        }

        public PersonContactRepository(PersonContext context ) : base(context)
        {
            _context = context;
        }

        public List<PersonContact> GetLatespersonContact(int personId)
        {
            IPersonContactRepository personContactRepository=new PersonContactRepository();
            var personContactList= personContactRepository.GetAll().Where(x => x.PersonId == personId).OrderBy(x=>x.Id);
            return personContactList.ToList();
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            IPersonContactRepository personContactRepository = new PersonContactRepository();
            var personContactList = personContactRepository.GetAll().Where(x => x.PersonId == personId).OrderBy(x => x.Id);
            return personContactList.ToList();
        }

        public void DeletePersonContact(int id)
        {
            var entityToDelete = _context.Set<PersonContact>().Find(id);
            _context.Set<PersonContact>().Remove(entityToDelete);
            _context.SaveChanges();
        }
    }
}
