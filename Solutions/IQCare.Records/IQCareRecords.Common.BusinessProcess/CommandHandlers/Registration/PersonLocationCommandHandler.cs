using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using Entities.PatientCore;
using IQCareRecords.Common.BusinessProcess.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using IQCare.Records.UILogic;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class PersonLocationCommandHandler:IRequestHandler<AddUpdatePersonLocationCommand,Result<AddUpdatePersonLocationResponse>>
    {

        public int res;
        public string msg;
        public async Task<Result<AddUpdatePersonLocationResponse>> Handle(AddUpdatePersonLocationCommand request,CancellationToken cancellationToken)
        {
            try
            {
                var PersonLocation = new PersonLocationManager();
                if (request.PersonId > 0)
                {
                    var currentLocation = PersonLocation.GetCurrentPersonLocation(request.PersonId);
                    if (currentLocation.Count > 0)
                    {
                        currentLocation[0].DeleteFlag = true;
                        PersonLocation.UpdatePersonLocation(currentLocation[0]);
                        //public int AddPersonLocation(int personId, int county, int subcounty, int ward, string village, string location, string sublocation, string landmark, string nearesthealthcentre, int userId)
                        res = await Task.Run(()=>PersonLocation.AddPersonLocation(request.PersonId, request.CountyId, request.SubCountyId, request.WardId, "", "", "", request.LandMark, request.NearestHealthCentre, request.UserId));
                        if (res > 0)
                        {
                            msg += "Person Location Successfully Updated";
                        }
                    }


                    else
                    {
                        res = await Task.Run(()=>PersonLocation.AddPersonLocation(request.PersonId, request.CountyId, request.SubCountyId, request.WardId, "", "", "", request.LandMark, request.NearestHealthCentre, request.UserId));
                        if (res > 0)
                        {
                            msg += "Person Location Successfully Added";
                        }

                    }

                }
                else
                {
                    msg += "The current person was not updated";
                }

                return Result<AddUpdatePersonLocationResponse>.Valid(new AddUpdatePersonLocationResponse()
                {
                    Message = msg
                });

            }
            catch( Exception e)

            {
                return Result<AddUpdatePersonLocationResponse>.Invalid(e.Message);

            }
        }
    }
}
