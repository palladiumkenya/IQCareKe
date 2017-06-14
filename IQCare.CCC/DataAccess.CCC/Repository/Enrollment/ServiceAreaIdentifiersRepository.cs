using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
    public class ServiceAreaIdentifiersRepository : BaseRepository<ServiceAreaIdentifiers>, IServiceAreaIdentifiersRepository
    {
        private readonly GreencardContext _context;

        public ServiceAreaIdentifiersRepository():this(new GreencardContext())
        {
            
        }

        public ServiceAreaIdentifiersRepository(GreencardContext context):base(context)
        {
            _context = context;
        }
    }
}
