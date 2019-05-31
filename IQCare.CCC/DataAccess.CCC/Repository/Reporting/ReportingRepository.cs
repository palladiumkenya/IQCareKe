using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Reporting;
using DataAccess.Context;
using Entities.CCC.Reports;

namespace DataAccess.CCC.Repository.Reporting
{
    public class ReportingRepository : BaseRepository<sp_gettxcurr>, IReportingRepository
    {
        private readonly GreencardContext _context;
        public ReportingRepository() : base(new GreencardContext())
        {

        }
        public ReportingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
