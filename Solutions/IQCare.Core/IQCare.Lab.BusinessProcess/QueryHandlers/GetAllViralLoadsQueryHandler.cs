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
    public class GetAllViralLoadsQueryHandler : IRequestHandler<GetAllViralLoadsQuery, Result<List<PatientLabTracker>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        public GetAllViralLoadsQueryHandler(ILabUnitOfWork labUnitOfWork)
        {
            _labUnitOfWork = labUnitOfWork;
        }

        public async Task<Result<List<PatientLabTracker>>> Handle(GetAllViralLoadsQuery request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {
                    var result = await _labUnitOfWork.Repository<PatientLabTracker>().Get(x => x.LabTestId == 3).ToListAsync();
                    return Result<List<PatientLabTracker>>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured while fetching patient viral loads");
                    return Result<List<PatientLabTracker>>.Invalid(ex.Message);
                }
            }
        }
    }
}
