using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class INHProphylaxisRepository : BaseRepository<INHProphylaxis>, IINHProphylaxisRepository
    {
        private GreencardContext _context;

        public INHProphylaxisRepository():base(new GreencardContext())
        {

        }

        public INHProphylaxisRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

    }
}
