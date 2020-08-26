using IQCare.Common.BusinessProcess.Commands.Matrix;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Matrix
{
    class GetPatientStabilitySummaryCommandHandler : IRequestHandler<GetPatientStabilitySummaryCommand, Result<List<PatientStabilitySummary>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPatientStabilitySummaryCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientStabilitySummary>>> Handle(GetPatientStabilitySummaryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientStabilitySummary>().GetAllAsync();
                    return Result<List<PatientStabilitySummary>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching facility stability summary");
                    return Result<List<PatientStabilitySummary>>.Invalid(ex.Message);
                }
            }
        }
    }
}
