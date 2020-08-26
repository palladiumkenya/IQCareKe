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
    public class GetHtsFacilityStatisticsCommandHandler : IRequestHandler<GetHtsFacilityStatisticsCommand, Result<List<HtsFacilityDashboard>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetHtsFacilityStatisticsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<HtsFacilityDashboard>>> Handle(GetHtsFacilityStatisticsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<HtsFacilityDashboard>().GetAllAsync();
                    return Result<List<HtsFacilityDashboard>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching hts facility statistics");
                    return Result<List<HtsFacilityDashboard>>.Invalid(ex.Message);
                }
            }
        }
    }
}
