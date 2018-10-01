using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records.Enrollment;

namespace DataAccess.Records.Repository
{
    class PersonOccupationRepository:BaseRepository<PersonOccupation> ,IPersonOccupationRepository

    {
        private readonly RecordContext _context;

        public PersonOccupationRepository():this(new RecordContext())
        {

        }
        public  PersonOccupationRepository (RecordContext context):base(context)
        {
            _context = context;
        }
    }
}
