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
                var result = await registerPersonService.RegisterPerson(request.Person.FirstName, request.Person.MiddleName,
                    request.Person.LastName, request.Person.Sex, request.Person.DateOfBirth, request.Person.CreatedBy);

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