using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPatientIdentifiersCommandHandler : IRequestHandler<GetPatientIdentifiersCommand, Result<List<PatientIdentifier>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPatientIdentifiersCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientIdentifier>>> Handle(GetPatientIdentifiersCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientIdentifier>()
                        .Get(x => x.PatientId == request.PatientId).ToListAsync();

                    return Result<List<PatientIdentifier>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error($"Error fetching patient identifiers list for patientId: {request.PatientId}. Exception: {e.Message}, InnerException: {e.InnerException}");
                    return Result<List<PatientIdentifier>>.Invalid($"Error fetching patient identifiers list for patientId: {request.PatientId}. Exception: {e.Message}, InnerException: {e.InnerException}");
                }
            }
        }
    }
}