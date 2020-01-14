using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Lab.BusinessProcess.QueryHandlers
{
    public class GetVlStatusCountQueryHandler : IRequestHandler<GetVlStatusCountQuery, Result<IEnumerable<VLStatuses<string, int>>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        public GetVlStatusCountQueryHandler(ILabUnitOfWork labUnitOfWork)
        {
            _labUnitOfWork = labUnitOfWork;
        }

        public async Task<Result<IEnumerable<VLStatuses<string, int>>>> Handle(GetVlStatusCountQuery request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {
                    var result =  await _labUnitOfWork.Repository<PatientLabTracker>().Get(x => x.LabTestId == 3).GroupBy(x => x.Results, y => y.Id).ToListAsync();
                    var countResults = result.Select(group => new VLStatuses<string, int> {
                        Metric = group.Key,
                        Count = group.Count()
                    });
                    return Result<IEnumerable<VLStatuses<string, int>>>.Valid(countResults);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured while fetching viral load statuses");
                    return Result<IEnumerable<VLStatuses<string, int>>>.Invalid(ex.Message);
                }
            }
        }
    }
}
