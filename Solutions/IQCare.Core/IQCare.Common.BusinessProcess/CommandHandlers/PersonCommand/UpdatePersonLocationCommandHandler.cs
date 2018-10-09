using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class UpdatePersonLocationCommandHandler : IRequestHandler<UpdatePersonLocationCommand, Result<PersonLocation>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePersonLocationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonLocation>> Handle(UpdatePersonLocationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PersonLocation personLocation = new PersonLocation();
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var personLocationList = await registerPersonService.GetPersonLocation(request.PersonId);
                    if (personLocationList.Count > 0)
                    {
                        personLocation = await registerPersonService.UpdatePersonLocation(request.PersonId, request.LandMark);
                    }
                    else
                    {
                        personLocation = await registerPersonService.addPersonLocation(request.PersonId, request.CountyId, request.SubCountyId, request.WardId, request.Village, request.LandMark, request.UserId);
                    }

                    return Result<PersonLocation>.Valid(personLocation);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PersonLocation>.Invalid(e.Message);
                }
            }
        }
    }
}