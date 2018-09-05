using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

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
                    Facility clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == request.Person.PosId.ToString()).FirstOrDefaultAsync();
                    if (clientFacility == null)
                    {
                        clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                    }

                    if (!request.Person.Id.HasValue)
                    {
                        person = await registerPersonService.RegisterPerson(request.Person.FirstName, request.Person.MiddleName,
                            request.Person.LastName, request.Person.Sex, request.Person.CreatedBy, clientFacility.FacilityID, request.Person.DateOfBirth,
                            request.Person.RegistrationDate);
                    }
                    else
                    {
                        person = await registerPersonService.UpdatePerson(request.Person.Id.Value,
                            request.Person.FirstName, request.Person.MiddleName, request.Person.LastName,
                            request.Person.Sex, request.Person.DateOfBirth, clientFacility.FacilityID, request.Person.RegistrationDate, request.Person.DobPrecision);
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
