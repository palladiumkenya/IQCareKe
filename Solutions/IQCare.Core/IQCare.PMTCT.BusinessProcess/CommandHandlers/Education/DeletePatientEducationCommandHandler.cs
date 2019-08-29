using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Education
{
    public class DeletePatientEducationCommandHandler : IRequestHandler<DeletePatientEducationCommand, Result<DeletePatientEducationCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int educationresultId;

        public DeletePatientEducationCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<DeletePatientEducationCommandResult>> Handle(DeletePatientEducationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    if (request.Id > 0)
                    {
                        var patientEducation = await _unitOfWork.Repository<PatientEducation>().Get(x => x.Id == request.Id)
                            .FirstOrDefaultAsync();
                        if (patientEducation != null)
                        {
                            patientEducation.DeleteFlag = true;
                            _unitOfWork.Repository<PatientEducation>().Update(patientEducation);
                            educationresultId = 1;
                            await _unitOfWork.SaveAsync();

                        }

                        else
                        {
                            educationresultId = 0;
                        }
                    } else
                    {
                        educationresultId = 0;
                    }
                    return Result<DeletePatientEducationCommandResult>.Valid(new DeletePatientEducationCommandResult()
                    {
                        PatientCounsellingId = educationresultId
                    });



                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                    return Result<DeletePatientEducationCommandResult>.Invalid(ex.Message);

                }



            }
        }
    }
}
