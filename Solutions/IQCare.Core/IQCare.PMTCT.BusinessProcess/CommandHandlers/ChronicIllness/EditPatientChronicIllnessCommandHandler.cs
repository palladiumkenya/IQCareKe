using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.ChronicIllness
{
    public class EditPatientChronicIllnessCommandHandler : IRequestHandler<EditPatientChronicIllnessCommand, Result<PatientChronicIllness>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditPatientChronicIllnessCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PatientChronicIllness>> Handle(EditPatientChronicIllnessCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientChronicIllness patientChronicIllness =  _unitOfWork.Repository<PatientChronicIllness>()
                        .Get(x => x.Id == request.PatientChronicIllness.Id &&
                                  x.PatientId == request.PatientChronicIllness.PatientId).FirstOrDefault();
                    if (patientChronicIllness != null)
                    {
                        patientChronicIllness.ChronicIllness = request.PatientChronicIllness.ChronicIllness;
                        patientChronicIllness.Dose = request.PatientChronicIllness.Dose;
                        patientChronicIllness.Duration = request.PatientChronicIllness.Duration;
                        patientChronicIllness.OnsetDate = request.PatientChronicIllness.OnsetDate;
                        patientChronicIllness.Treatment = request.PatientChronicIllness.Treatment;
                    }

                     _unitOfWork.Repository<PatientChronicIllness>().Update(patientChronicIllness);
                    await _unitOfWork.SaveAsync();

                    return Result<PatientChronicIllness>.Valid(request.PatientChronicIllness);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientChronicIllness>.Invalid(e.Message);
                }
            }
        }
    }
}