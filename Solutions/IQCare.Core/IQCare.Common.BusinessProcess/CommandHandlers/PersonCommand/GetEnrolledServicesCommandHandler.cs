using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetEnrolledServicesCommandHandler : IRequestHandler<GetEnrolledServicesCommand, Library.Result<EnrolledServicesResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetEnrolledServicesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Library.Result<EnrolledServicesResponse>> Handle(GetEnrolledServicesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    List<PatientEnrollment> result = new List<PatientEnrollment>();
                    var patient = await registerPersonService.GetPatientByPersonId(request.PersonId);
                    if (patient != null)
                    {
                        result = await _unitOfWork.Repository<PatientEnrollment>().Get(x => x.PatientId == patient.Id).ToListAsync();
                    }

                    return Library.Result<EnrolledServicesResponse>.Valid(new EnrolledServicesResponse()
                    {
                        PersonEnrollmentList = result
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<EnrolledServicesResponse>.Invalid(e.Message);
                }
            }
        }
    }
}