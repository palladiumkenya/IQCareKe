using System;
using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
{
   public class PersonRepository:BaseRepository<Person>,IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository() : this(new PersonContext())
        {

        }

        public PersonRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Person p)
        {
            _context.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
