using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class GetServiceEntryPointCommandHandler : IRequestHandler<GetServiceEntryPointCommand, Result<ServiceEntryPoint>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetServiceEntryPointCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ServiceEntryPoint>> Handle(GetServiceEntryPointCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<ServiceEntryPoint>().Get(x =>
                        x.ServiceAreaId == request.ServiceAreaId && x.PatientId == request.PatientId &&
                        x.DeleteFlag == false).FirstOrDefaultAsync();

                    return Result<ServiceEntryPoint>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error fetching serviceentrypoints for serviceareaId: {request.ServiceAreaId} and patientId: {request.PatientId}");
                    return Result<ServiceEntryPoint>.Invalid(
                        $"Error fetching serviceentrypoints for serviceareaId: {request.ServiceAreaId} and patientId: {request.PatientId}");
                }
            }
        }
    }
}