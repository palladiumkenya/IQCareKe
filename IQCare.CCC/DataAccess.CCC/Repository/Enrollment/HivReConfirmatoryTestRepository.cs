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
    public class HivReConfirmatoryTestRepository:BaseRepository<HivReConfirmatoryTest>,IHivReConfirmatoryTestRepository
    {
        private readonly GreencardContext _context;

        public HivReConfirmatoryTestRepository() : this(new GreencardContext())
        {

        }

        public HivReConfirmatoryTestRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
