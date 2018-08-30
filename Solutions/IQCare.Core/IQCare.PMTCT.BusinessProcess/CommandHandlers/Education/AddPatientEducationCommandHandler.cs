using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Education
{
    public class AddPatientEducationCommandHandler:IRequestHandler<AddPatientEducationCommand, Result<AddPatientEducationCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientEducationCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); 
        }
        public async Task<Result<AddPatientEducationCommandResult>> Handle(AddPatientEducationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientEducation patientEducation = new PatientEducation()
                    {
                        PatientId=request.patientEducation.PatientId,
                        PatientMasterVisitId=request.patientEducation.PatientMasterVisitId,
                        CounsellingTopicId=request.patientEducation.CounsellingTopicId,
                        CounsellingDate=request.patientEducation.CounsellingDate,
                        Description=request.patientEducation.Description,
                    };

                    await _unitOfWork.Repository<PatientEducation>().AddAsync(patientEducation);
                    await _unitOfWork.SaveAsync();
                    return Result<AddPatientEducationCommandResult>.Valid(new AddPatientEducationCommandResult()
                    {
                        PatientCounsellingId = 1
                    });
                }
                catch (Exception e)
                {                
                    Log.Error(e.Message);
                    return Result<AddPatientEducationCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}
