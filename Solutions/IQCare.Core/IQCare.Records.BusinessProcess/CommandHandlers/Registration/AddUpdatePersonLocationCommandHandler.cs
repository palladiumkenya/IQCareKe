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
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class AddUpdatePersonLocationCommandHandler:IRequestHandler<AddUpdatePersonLocationCommand,Result<AddUpdatePersonLocationResponse>>
    {
        public int res;
        public string msg;
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddUpdatePersonLocationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddUpdatePersonLocationResponse>> Handle (AddUpdatePersonLocationCommand request,CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                if(request.PersonId >0)
                {
                    var currentlocation = await rs.GetCurrentPersonLocation(request.PersonId);
                    if(currentlocation!=null)
                    {
                        currentlocation.PersonId = request.PersonId;
                        currentlocation.County = request.CountyId;
                        currentlocation.SubCounty = request.SubCountyId;
                        currentlocation.Ward = request.WardId;
                        currentlocation.LandMark = request.LandMark;
                        currentlocation.NearestHealthCentre = request.NearestHealthCentre;
                        currentlocation.CreatedBy = request.UserId;

                        res = await rs.UpdatePersonLocation(currentlocation);
                        if(res > 0)
                        {
                            msg += "Person Location Successfully Updated";
                        }

                    }
                    else
                    {
                        var res = await rs.AddPersonLocation(request.PersonId, request.CountyId, request.SubCountyId, request.WardId, "", "", "", request.LandMark, request.NearestHealthCentre, request.UserId);
                        if (res > 0)
                        {
                            msg += "Person Location Successfully Added";
                        }

                    }
                }
                return Result<AddUpdatePersonLocationResponse>.Valid(new AddUpdatePersonLocationResponse()
                {
                            Message =msg
                }
                    );
            }
            catch(Exception e)
            {

                return Result<AddUpdatePersonLocationResponse>.Invalid(e.Message);
            }
        }
     }
}
