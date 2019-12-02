using System;
using System.Collections.Generic;
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
    public class GetPatientCareEndedByServiceAreaCommandHandler : IRequestHandler<GetPatientCareEndedByServiceAreaCommand, Result<List<PatientCareEndingServiceArea>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientCareEndedByServiceAreaCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<List<PatientCareEndingServiceArea>>> Handle(GetPatientCareEndedByServiceAreaCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {

                    var result = await _unitOfWork.Repository<PatientCareEndingServiceArea>().Get(x => x.PersonId == request.PersonId).ToListAsync();
                    return Result<List<PatientCareEndingServiceArea>>.Valid(result);
                }
                catch (Exception ex)
                {
                    return Result<List<PatientCareEndingServiceArea>>.Invalid(ex.Message);

                }

            }
        }
    }
}
