using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientStabilitySummaryManager : ProcessBase, IPatientStabilitySummaryManager
    {
        public List<PatientStabilitySummary> GetAllStabilitySummaries()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var summaries = unitOfWork.PatientStabilitySummaryRepository.GetAll();
                unitOfWork.Dispose();
                return summaries.ToList();
            }
        }
    }
}
