using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Result<Person>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePersonCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Person>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    Facility clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == request.PosId.ToString()).FirstOrDefaultAsync();
                    if (clientFacility == null)
                    {
                        clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                    }
                    var person = await registerPersonService.UpdatePerson(request.PersonId, request.FirstName, request.MiddleName,
                        request.LastName, request.Sex, request.DateOfBirth, clientFacility.FacilityID);

                    return Result<Person>.Valid(person);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Person>.Invalid(e.Message);
                }
            }
        }
    }
}