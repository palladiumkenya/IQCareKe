using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class GetPatientCareEndingCommandHandler :IRequestHandler<GetPatientCareEndingCommand,Result<PatientCareEnding>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientCareEndingCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientCareEnding>> Handle(GetPatientCareEndingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Repository<PatientCareEnding>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId).FirstOrDefaultAsync();

                return Result<PatientCareEnding>.Valid(result);
            }

            catch(Exception ex)
            {
                return Result<PatientCareEnding>.Invalid(ex.Message);
            }
        }
    }
}
