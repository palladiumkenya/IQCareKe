using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Records;
using Entities.Records.Enrollment;
using IQCareRecords.Common.BusinessProcess.Commands;
using IQCare.Records.UILogic;
using MediatR;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
  public  class PersonOccupationLevelCommandHandler:IRequestHandler<PersonOccupationLevelCommand,Result<AddPersonOccupationLevelResponse>>
    {
        public int res;
        public string msg;

        public async Task<Result<AddPersonOccupationLevelResponse>>Handle(PersonOccupationLevelCommand request, CancellationToken  cancellationToken)
        {
            PersonOccupation pmo = new PersonOccupation();
            try
            {
                 if(request.PersonId > 0)
                {
                    PersonOccupationManager pm = new PersonOccupationManager();
                    pmo = pm.GetCurrentOccupationion(request.PersonId);

                    if(pmo!=null)
                    {
                        pmo.DeleteFlag = true;
                        int result;
                        result=pm.UpdateOccupation(pmo);
                      
                        res=await Task.Run(()=>pm.AddPersonOccupation(request.PersonId, request.UserId, request.Occupation));

                        if(res> 0)
                        {
                            msg = "PersonOccupation  Updated successfully";
                        }
                    }

                    else
                    {
                        res = await Task.Run(() => pm.AddPersonOccupation(request.UserId,request.Occupation,request.PersonId));

                        if (res > 0)
                        {
                            msg = "PersonOccupation added successfully for personId" + request.PersonId;
                        }
                    }
                }

                return Result<AddPersonOccupationLevelResponse>.Valid(new AddPersonOccupationLevelResponse()
                {
                    Message=msg
                }
                    );
            }
            catch(Exception e)
            {
                return Result<AddPersonOccupationLevelResponse>.Invalid(e.Message);
            }

        }

    }
}
