using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Result<Person>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePersonCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Person>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var person = await registerPersonService.UpdatePerson(request.PersonId, request.FirstName, request.MiddleName,
                        request.LastName, request.Sex, request.DateOfBirth);

                    return Result<Person>.Valid(person);

                    //var maritalStatusList = await registerPersonService.GetPersonMaritalStatus(request.PersonId);
                    //if (maritalStatusList.Count > 0)
                    //{
                    //    var matStatus = await registerPersonService.UpdateMaritalStatus(request.PersonId, request.MaritalStatus);
                    //}
                    //else
                    //{
                    //    var matStatus = await registerPersonService.AddMaritalStatus(request.PersonId, request.MaritalStatus,
                    //        request.CreatedBy);
                    //}

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Person>.Invalid(e.Message);
                }
            }
        }
    }
}