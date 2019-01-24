using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.ChronicIllness
{
    public class AddPatientChronicIllnessCommandHandler : IRequestHandler<AddPatientChronicIllnessCommand, Result<PatientChronicIllness>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientChronicIllnessCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientChronicIllness>> Handle(AddPatientChronicIllnessCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientChronicIllness> patientChronicIllness = new List<PatientChronicIllness>();

                    foreach (var item in request.PatientChronicIllnesses)
                    {
                        PatientChronicIllness patientChronic = new PatientChronicIllness()
                        {
                            Active = false,
                            ChronicIllness = item.ChronicIllness,
                            CreateBy = item.CreateBy,
                            DeleteFlag = item.DeleteFlag,
                            Dose = item.Dose,
                            Duration = item.Duration,
                            OnsetDate = item.OnsetDate,
                            Treatment = item.Treatment,
                            PatientId = item.PatientId,
                            PatientMasterVisitId = item.PatientMasterVisitId,
                        };

                        patientChronicIllness.Add(patientChronic);
                    }

                    await _unitOfWork.Repository<PatientChronicIllness>().AddRangeAsync(patientChronicIllness);
                    await _unitOfWork.SaveAsync();

                    return Result<PatientChronicIllness>.Valid(request.PatientChronicIllnesses[0]);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientChronicIllness>.Invalid(e.Message + e.InnerException);
                }
               
            }
        }
    }
}