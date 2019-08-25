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
    public class GetPatientEnrollmentByServiceAreaIdCommandHandler : IRequestHandler<GetPatientEnrollmentByServiceAreaIdCommand, Result<PatientEnrollment>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientEnrollmentByServiceAreaIdCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientEnrollment>> Handle(GetPatientEnrollmentByServiceAreaIdCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientEnrollment>().Get(x =>
                        x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId &&
                        x.DeleteFlag == false).FirstOrDefaultAsync();

                    return Result<PatientEnrollment>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error fetching patient enrollment for PatientId: {request.PatientId} and ServiceAreaId: {request.ServiceAreaId}. ErrorMessage: {ex.Message}, innerexception: {ex.InnerException}");
                    return Result<PatientEnrollment>.Invalid($"Error fetching patient enrollment for PatientId: {request.PatientId} and ServiceAreaId: {request.ServiceAreaId}");
                }
            }
        }
    }
}