using System;
using System.Text;
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
    public class UpdatePersonContactCommandHandler : IRequestHandler<UpdatePersonContactCommand, Result<PersonContact>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePersonContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonContact>> Handle(UpdatePersonContactCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PersonContact personContact = new PersonContact();

                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var personContactList = await registerPersonService.GetPersonContact(request.PersonId);
                    if (personContactList.Count > 0)
                    {
                        personContact = await registerPersonService.UpdatePersonContact(request.PersonId,
                            request.PhysicalAddress, request.MobileNumber);
                    }
                    else
                    {
                        personContact = await registerPersonService.addPersonContact(request.PersonId,
                            request.PhysicalAddress, request.MobileNumber, request.AlternativeNumber,
                            request.EmailAddress, request.UserId);
                    }

                    return Result<PersonContact>.Valid(personContact);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PersonContact>.Invalid(e.Message);
                }
            }
        }
    }
}