using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Command;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using System.Threading.Tasks;
using System.Threading;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Services;
using IQCare.Library;
using Serilog;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
    public class PersonIdentifierCommandHandler:IRequestHandler<PersonIdentifierCommand,Result<AddPersonIdentifierResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonIdentifierCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonIdentifierResponse>> Handle (PersonIdentifierCommand request,CancellationToken cancellationToken)
        {
            try
            {

                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                PersonIdentifiersService personIdentifiersService = new PersonIdentifiersService(_unitOfWork);
                var personIdentifierTypeList = await personIdentifiersService.GetPersonIdentifierByType(request.IdentifierId, request.PersonId);
                if (personIdentifierTypeList.Count > 0)
                {
                    personIdentifierTypeList[0].IdentifierValue = request.IdentifierValue;
                    await personIdentifiersService.UpdatePersonIdentifierType(personIdentifierTypeList[0]);
                }
                else
                {
                    await personIdentifiersService.AddPersonIdentifierType(request.PersonId, request.IdentifierId, request.IdentifierValue, request.UserId);
                }
                
                return Result<AddPersonIdentifierResponse>.Valid(new AddPersonIdentifierResponse()
                {
                    Message = "Successfully add person identifier"
                });
            }
            catch(Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                return Result<AddPersonIdentifierResponse>.Invalid(e.Message);
            }
        }

    }
}
