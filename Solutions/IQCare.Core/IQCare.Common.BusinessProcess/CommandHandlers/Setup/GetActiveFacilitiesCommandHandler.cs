using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Setup
{
    public class GetActiveFacilitiesCommandHandler : IRequestHandler<GetActiveFacilitiesCommand, Result<List<Facility>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetActiveFacilitiesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<Facility>>> Handle(GetActiveFacilitiesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Services.Setup setup = new Services.Setup(_unitOfWork);
                    var result = await setup.GetActiveFacilities();

                    return Result<List<Facility>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<Facility>>.Invalid(e.Message);
                }
            }
        }
    }
}