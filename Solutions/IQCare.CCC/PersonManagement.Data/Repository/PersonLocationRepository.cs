using Common.Data.Repository;
using PersonManagement.Core.Model;
using PersonManagement.Core.Interfaces;

namespace PersonManagement.Data.Repository
{
    public class PersonLocationRepository :BaseRepository<PersonLocation>, IPersonLocationRepository
    {
        private readonly PersonContext _context;

        public PersonLocationRepository() : this(new PersonContext())
        {

        }

        public PersonLocationRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
    }
}
