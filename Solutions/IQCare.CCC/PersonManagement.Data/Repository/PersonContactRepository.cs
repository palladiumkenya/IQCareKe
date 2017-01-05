using Common.Data.Repository;
using PersonManagement.Core.Interfaces;
using PersonManagement.Core.Model;

namespace PersonManagement.Data.Repository
{
    public class PersonContactRepository : BaseRepository<PersonContact>, IPersonContactRepository
    {
        private readonly PersonContext _context;

        public PersonContactRepository() : this(new PersonContext())
        {
        }

        public PersonContactRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
    }
}
