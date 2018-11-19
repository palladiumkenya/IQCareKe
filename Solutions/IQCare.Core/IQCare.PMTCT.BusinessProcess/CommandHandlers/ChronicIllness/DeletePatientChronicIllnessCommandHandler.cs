using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.ChronicIllness
{
    public class DeletePatientChronicIllnessCommandHandler: IRequestHandler<DeletePatientChronicIllnessCommand, Result<PatientChronicIllness>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public DeletePatientChronicIllnessCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PatientChronicIllness>> Handle(DeletePatientChronicIllnessCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientChronicIllness patientChronicIllness = await _unitOfWork.Repository<PatientChronicIllness>()
                        .Get(x => x.Id == request.Id && !x.DeleteFlag).FirstOrDefaultAsync();
                    if (patientChronicIllness != null)
                    {
                        patientChronicIllness.DeleteFlag = true;
                        _unitOfWork.Repository<PatientChronicIllness>().Update(patientChronicIllness);
                        await _unitOfWork.SaveAsync();                      
                    }
                    return Result<PatientChronicIllness>.Valid(patientChronicIllness);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + e.InnerException);
                    return Result<PatientChronicIllness>.Invalid(e.Message);
                }
            }
        }
    }
}