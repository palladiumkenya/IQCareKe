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

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
    public class PersonIdentifierCommandHandler:IRequestHandler<PersonIdentifierCommand,Result<AddPersonIdentifierResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonIdentifierCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork?? throw new ArgumentNullException(nameof(unitOfWork));
        }

       
        public string msg;
        public async Task<Result<AddPersonIdentifierResponse>> Handle (PersonIdentifierCommand request,CancellationToken cancellationToken)
        {
            try

            {

                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                if(request.PersonId> 0)
                {
                    PersonIdentifier pidm = new PersonIdentifier();
                     pidm =await rs.GetCurrentPersonIdentifier(request.IdentifierId,request.PersonId);

                    if(pidm !=null)
                    {
                        pidm.DeleteFlag = true;
                       var pdm = await Task.Run(() => rs.UpdatePersonIdentifier(pidm));
                       var  finalupdate = await rs.addPersonIdentifiers(request.PersonId, request.IdentifierId, request.IdentifierValue, request.UserId);
                        if(finalupdate !=null)
                        {
                            if(finalupdate.Id > 0)
                            {
                                msg += "PersonIdentifier updated successfully";
                            }
                        }
                    }

                    else
                    {
                       var finalIdent = await rs.addPersonIdentifiers(request.PersonId, request.IdentifierId, request.IdentifierValue, request.UserId);
                        if (finalIdent != null)
                        {
                            if (finalIdent.Id > 0)
                            {
                                msg += "PersonIdentifierType added successfully";
                            }
                        }
                    }
                }


                return Result<AddPersonIdentifierResponse>.Valid(new AddPersonIdentifierResponse()
                    {
                    Message = msg
                });


            }
            catch(Exception e)
            {
                return Result<AddPersonIdentifierResponse>.Invalid(e.Message);
            }
        }

    }
}
