using DataAccess.CCC.Context;
using DataAccess.Context;
using Entities.CCC.OneTimeEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Repository.OneTimeEvent
{
    public class OneTimeEventTrackerRepository: BaseRepository<OneTimeEventTracker>
    {
        private readonly GreencardContext _context;

        public OneTimeEventTrackerRepository(): base (new GreencardContext())
        {

        }

        public OneTimeEventTrackerRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
