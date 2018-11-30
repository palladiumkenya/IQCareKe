using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers
{
    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result<RegisterClientResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        public RegisterClientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<RegisterClientResponse>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                var facilityList = await _unitOfWork.Repository<Facility>()
                    .Get(x => x.PosID == request.Person.FacilityId.ToString()).ToListAsync();
                Facility facility = new Facility();
                if (facilityList.Count > 0)
                {
                    facility = facilityList[0];
                }
                else
                {
                    facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0)
                        .FirstOrDefaultAsync();
                }

                var result = await registerPersonService.RegisterPerson(request.Person.FirstName, request.Person.MiddleName,
                    request.Person.LastName, request.Person.Sex, request.Person.CreatedBy, facility.FacilityID, request.Person.DateOfBirth);

                _unitOfWork.Dispose();

                return Result<RegisterClientResponse>.Valid(new RegisterClientResponse {PersonId = result.Id});

            }
            catch (Exception e)
            {
                return Result<RegisterClientResponse>.Invalid(e.Message);
            }
        }
    }
}