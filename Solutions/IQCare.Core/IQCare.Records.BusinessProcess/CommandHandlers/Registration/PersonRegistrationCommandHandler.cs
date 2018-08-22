using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using Serilog;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class PersonRegistrationCommandHandler:IRequestHandler<PersonRegistrationCommand, Result<PersonRegistrationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
    
        public PersonRegistrationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonRegistrationResponse>> Handle(PersonRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    Person person = new Person();
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    if (request.Person.PersonId == 0)
                    {
                        person = await registerPersonService.RegisterPerson(request.Person.FirstName, request.Person.MiddleName,
                            request.Person.LastName, request.Person.Sex, request.Person.DateOfBirth,
                            request.Person.CreatedBy, request.Person.RegistrationDate);
                    }
                    else
                    {

                    }

                    _unitOfWork.Dispose();
                    return Result<PersonRegistrationResponse>.Valid(new PersonRegistrationResponse { PersonId = person.Id, Message = "Success" });
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                return Result<PersonRegistrationResponse>.Invalid(e.Message);
            }
        }
    }
}
