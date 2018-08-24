using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Command;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Services;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonEmergencyContactCommandHandler : IRequestHandler<PersonEmergencyContactCommand, Result<AddPersonEmergencyContactResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonEmergencyContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddPersonEmergencyContactResponse>> Handle(PersonEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                PersonContactsService personContactsService = new PersonContactsService(_unitOfWork);
                //await registerPersonService.RegisterPerson(request.Emergencycontact.Firstname,
                //    request.Emergencycontact.Middlename, request.Emergencycontact.Lastname,
                //    request.Emergencycontact.Gender, "", request.Emergencycontact.CreatedBy, DateTime.Now);

                //await personContactsService.Add(request.Emergencycontact.PersonId, request.Emergencycontact.EmergencyContactPersonId, request.Emergencycontact.CreatedBy, 1);

                return Result<AddPersonEmergencyContactResponse>.Valid(new AddPersonEmergencyContactResponse(){});
            }
            catch (Exception e)
            {
                return Result<AddPersonEmergencyContactResponse>.Invalid(e.Message);
            }
        }
    }
}
