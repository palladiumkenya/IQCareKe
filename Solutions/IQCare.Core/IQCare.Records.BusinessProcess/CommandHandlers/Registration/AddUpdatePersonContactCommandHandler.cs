using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class AddUpdatePersonContactCommandHandler : IRequestHandler<AddUpdatePersonContactCommand, Result<AddUpdatePersonContactResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddUpdatePersonContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddUpdatePersonContactResponse>> Handle(AddUpdatePersonContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                PersonContact personContact = await registerPersonService.GetPersonContactByPersonId(request.PersonId);
                if (personContact != null)
                {
                    await registerPersonService.UpdatePersonContact(request.PersonId, request.PhysicalAddress,
                        request.MobileNumber, request.EmailAddress, request.AlternativeNumber);
                }
                else
                {
                    personContact = await registerPersonService.addPersonContact(request.PersonId,
                        request.PhysicalAddress, request.MobileNumber, request.AlternativeNumber, request.EmailAddress,
                        request.UserId);
                }

                return Result<AddUpdatePersonContactResponse>.Valid(new AddUpdatePersonContactResponse()
                {
                    PersonContactId=personContact.Id,
                    Message = "Person Contact Successful"
                });

            }
            catch(Exception e)
            {
                return Result<AddUpdatePersonContactResponse>.Invalid(e.Message);
            }


        }
    }
}
