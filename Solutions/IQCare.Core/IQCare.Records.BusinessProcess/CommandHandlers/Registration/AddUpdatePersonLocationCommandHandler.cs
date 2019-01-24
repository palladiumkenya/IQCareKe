using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class AddUpdatePersonLocationCommandHandler:IRequestHandler<AddUpdatePersonLocationCommand,Result<AddUpdatePersonLocationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddUpdatePersonLocationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddUpdatePersonLocationResponse>> Handle (AddUpdatePersonLocationCommand request,CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PersonLocation location = new PersonLocation();
                    PersonLocationService personLocationService = new PersonLocationService(_unitOfWork);
                    List<PersonLocation> personLocationList = await personLocationService.GetPersonLocation(request.PersonId);
                    if (personLocationList.Count > 0)
                    {
                        personLocationList[0].County = request.CountyId;
                        personLocationList[0].SubCounty = request.SubCountyId;
                        personLocationList[0].Ward = request.WardId;
                        personLocationList[0].NearestHealthCentre = request.NearestHealthCentre;
                        personLocationList[0].LandMark = request.LandMark;

                        location = await personLocationService.UpdatePersonLocation(personLocationList[0]);
                    }
                    else
                    {
                        PersonLocation personLocation = new PersonLocation()
                        {
                            PersonId = request.PersonId,
                            County = request.CountyId,
                            SubCounty = request.SubCountyId,
                            Ward = request.WardId,
                            NearestHealthCentre = request.NearestHealthCentre,
                            LandMark = request.LandMark,
                            Village = "",
                            Location = "",
                            SubLocation = "",
                            Active = false,
                            DeleteFlag = false,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.UserId
                        };
                        location = await personLocationService.AddPersonLocation(personLocation);
                    }

                    return Result<AddUpdatePersonLocationResponse>.Valid(new AddUpdatePersonLocationResponse()
                    {
                        Message = "Successful",
                        PersonLocationId = location.Id
                    });
                }
                catch (Exception e)
                {

                    return Result<AddUpdatePersonLocationResponse>.Invalid(e.Message);
                }
            }
        }
     }
}
