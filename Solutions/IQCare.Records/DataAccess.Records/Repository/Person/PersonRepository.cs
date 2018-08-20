using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Records.Interface;
using DataAccess.Context;
using Entities.Common;
namespace DataAccess.Records.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly PersonContext _context;

        public override void Add(Person entity)
        { }

        public override IEnumerable<Person> GetAll()
        {
            return base.GetAll();
        }

        public PersonRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(Person entity)
        {
            //do nothing
        }


    }
}
