using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository
{
    public class PersonEmergencyContactRepository:BaseRepository<PersonEmergencyContact>, IPersonEmergencyContactRepository
    {
        private readonly RecordContext _context;


        public PersonEmergencyContactRepository() : this(new RecordContext())
        {

        }

        public  PersonEmergencyContactRepository(RecordContext context) : base(context)
        {
            _context = context;
        }
    }
}
