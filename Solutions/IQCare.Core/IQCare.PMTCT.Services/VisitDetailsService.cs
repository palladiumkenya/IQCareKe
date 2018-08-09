using IQCare.PMTCT.Infrastructure;
using System;

namespace IQCare.PMTCT.Services
{
    public class VisitDetailsService
    {
        public IPmtctUnitOfWork PmtctUnitOfWork  { get; set; }

        public VisitDetailsService(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            PmtctUnitOfWork = pmtctUnitOfWork;
        }



    }
}