using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Commands;
using Entities.Records;
using IQCare.Records.UILogic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities.Records.Enrollment;
using IQCare.Records.UILogic.Enrollment;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonIdentifierCommandHandler:IRequestHandler<PersonIdentifierCommand,Result<AddPersonIdentifierResponse>>
    {
        public int res;
        public string msg;
        public async Task<Result<AddPersonIdentifierResponse>>Handle(PersonIdentifierCommand request,CancellationToken cancellationToken)
        {
            PersonIdentifier pm = new PersonIdentifier();



            try
            {
                if (request.PersonId > 0)
                {
                    PersonIdentifierManager idm = new PersonIdentifierManager();
                    pm = idm.GetCurrentPersonIdentifier(request.PersonId, request.IdentifierId);

                    if (pm != null)
                    {
                        res = await Task.Run(() => idm.UpdatePersondentifier(request.PersonId, request.IdentifierId, request.IdentifierValue, request.UserId));
                        if (res > 0)
                        {
                            msg = "PersonIdentifier updated successfully";
                        }
                    }
                    else
                    {
                        res = await Task.Run(() => idm.AddPersonIdentifier(request.UserId, request.IdentifierId, request.IdentifierValue, request.UserId));
                        if (res > 0)
                        {
                            msg = "PersonIdentifier Added successfully for PersonId" + request.PersonId;
                        }

                    }
                }


                return Result<AddPersonIdentifierResponse>.Valid(new AddPersonIdentifierResponse()
                {
                    Message = msg
                }
                    );
            }
            catch(Exception e)
            {
                return Result<AddPersonIdentifierResponse>.Invalid(e.Message);

            }


        }
    }
}
