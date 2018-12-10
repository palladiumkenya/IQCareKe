using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class UpdatePersonMaritalStatusCommandHandler : IRequestHandler<UpdatePersonMaritalStatusCommand, Result<PersonMaritalStatus>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePersonMaritalStatusCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonMaritalStatus>> Handle(UpdatePersonMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PersonMaritalStatus personMaritalStatus = new PersonMaritalStatus();
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var maritalStatusList = await registerPersonService.GetPersonMaritalStatus(request.PersonId);
                    if (maritalStatusList.Count > 0)
                    {
                        personMaritalStatus = await registerPersonService.UpdateMaritalStatus(request.PersonId, request.MaritalStatusId);
                    }
                    else
                    {
                        personMaritalStatus = await registerPersonService.AddMaritalStatus(request.PersonId, request.MaritalStatusId, request.UserId);
                    }

                    return Result<PersonMaritalStatus>.Valid(personMaritalStatus);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PersonMaritalStatus>.Invalid(e.Message);
                }
            }
        }
    }
}