using IQCare.Common.BusinessProcess.Commands.Matrix;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
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

namespace IQCare.Common.BusinessProcess.CommandHandlers.Matrix
{
    public class GetFacilityCareEndingCommandHandler : IRequestHandler<GetFacilityCareEndingCommand, Result<FacilityStatisticsView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetFacilityCareEndingCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<FacilityStatisticsView>> Handle(GetFacilityCareEndingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<FacilityStatisticsView>().Get(x => x.Id > 0).ToListAsync();
                    return Result<FacilityStatisticsView>.Valid(result.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching facility care ending summary");
                    return Result<FacilityStatisticsView>.Invalid(ex.Message);
                }
            }
        }
    }
}
