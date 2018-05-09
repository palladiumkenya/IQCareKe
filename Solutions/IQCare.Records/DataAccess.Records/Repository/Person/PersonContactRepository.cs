using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Records.Interface;
using Entities.Common;
namespace DataAccess.Records.Repository
{
   public class PersonContactRepository :BaseRepository<PersonContact>,IPersonContactRepository
    {
        private readonly PersonContext _context;

        public PersonContactRepository() : this(new PersonContext())
        {

        }

        public override void Add(PersonContact entity)
        {
            //do nothing
        }

        public override void Update(PersonContact entity)
        {
            //do nothing
        }

        public PersonContactRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public List<PersonContact> GetCurrentPersonContact(int personId)
        {
            IPersonContactRepository personContactRepository = new PersonContactRepository();
            var personContactList = personContactRepository.GetAll().Where(x => x.PersonId == personId && x.DeleteFlag == false).OrderBy(x => x.Id);
            return personContactList.ToList();
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            IPersonContactRepository personContactRepository = new PersonContactRepository();
            var personContactList = personContactRepository.FindBy(x => x.PersonId == personId && x.DeleteFlag == false);
            return personContactList.ToList();
        }
    }

}

