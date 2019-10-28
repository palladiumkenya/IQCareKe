using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.BusinessProcess.Commands.PatientTreatmentTracker;
using IQCare.Pharm.Infrastructure;
using IQCare.Pharm.BusinessProcess.Services;

namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientTreatmentTracker
{
    public class PatientStartTreatmentCommandHandler : IRequestHandler<PatientStartTreatmentCommand, Result<PatientStartTreatmentResponse>>
    {
        private readonly IPharmUnitOfWork _unitOfWork;
        public PatientStartTreatmentCommandHandler(IPharmUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<Result<PatientStartTreatmentResponse>> Handle(PatientStartTreatmentCommand request, CancellationToken cancellationToken)
        {
            try
            {



                PharmacyService pharm = new PharmacyService(_unitOfWork);


                var result = await pharm.HasPatientTreatmentStarted(request.PatientId);


                return Result<PatientStartTreatmentResponse>.Valid(new PatientStartTreatmentResponse
                {
                    StartTreatment = result
                });
            }
            catch (Exception ex)
            {
                return Result<PatientStartTreatmentResponse>.Invalid(ex.Message);



            }



        }

    }
}
