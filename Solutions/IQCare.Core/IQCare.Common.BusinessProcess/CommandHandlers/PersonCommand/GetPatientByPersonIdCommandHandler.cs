using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPatientByPersonIdCommandHandler : IRequestHandler<GetPatientByPersonIdCommand, Result<Patient>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientByPersonIdCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Patient>> Handle(GetPatientByPersonIdCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var result = await registerPersonService.GetPatientByPersonId(request.PersonId);

                    return Result<Patient>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occured while finding Patient by PersonId: {request.PersonId}. ErrorMessage: {ex.Message}, InnerException: {ex.InnerException}");
                    return Result<Patient>.Invalid($"An error occured while finding Patient by PersonId: {request.PersonId}");
                }
            }
        }
    }
}