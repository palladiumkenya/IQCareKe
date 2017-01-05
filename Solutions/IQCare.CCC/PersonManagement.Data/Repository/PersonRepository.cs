using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Data.Repository;
using PersonManagement.Core.Interfaces;
using PersonManagement.Core.Model;

namespace PersonManagement.Data.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly PersonContext _context;
        public PersonRepository() : this(new PersonContext())
        {
        }

        public PersonRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
    }
}
