using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository
{
    public class ServiceAreaIdentifiersRepository : BaseRepository<ServiceAreaIdentifiers>, IServiceAreaIdentifiersRepository
    {
        private readonly RecordContext _context;

        public ServiceAreaIdentifiersRepository() : this(new RecordContext())
        {

        }

        public ServiceAreaIdentifiersRepository(RecordContext context) : base(context)
        {
            _context = context;
        }
    }
}
