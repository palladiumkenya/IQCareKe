using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPatientOVCStatusCommandHandler : IRequestHandler<GetPatientOVCStatusCommand, Result<PatientOVCStatus>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPatientOVCStatusCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientOVCStatus>> Handle(GetPatientOVCStatusCommand request, CancellationToken cancellationToken)
        {
         
                using (_unitOfWork)
                {

                try
                {
                    RegisterPersonService rs = new RegisterPersonService(_unitOfWork);

                    var PatientOVCStatus =await  rs.GetPatientOVCStatusByPersonId(request.PersonId);

                    return  Result<PatientOVCStatus>.Valid(PatientOVCStatus);

                }
                catch (Exception ex)
                {

                    return Result<PatientOVCStatus>.Invalid(ex.Message);

                }

              }

            
        }

    }
}
