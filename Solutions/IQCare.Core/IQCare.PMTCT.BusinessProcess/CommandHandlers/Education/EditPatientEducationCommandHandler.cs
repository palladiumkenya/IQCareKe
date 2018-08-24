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
    public class EditPatientEducationCommandHandler: IRequestHandler<EditPatientEducationCommand ,Result<EditPatientEducationCommadResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditPatientEducationCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditPatientEducationCommadResult>> Handle(EditPatientEducationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                    try
                    {
                        PatientEducation patientEducation = new PatientEducation()
                        {
                            PatientId = request.patientEducation.PatientId,
                            PatientMasterVisitId = request.patientEducation.PatientMasterVisitId,
                            CounsellingTopicId = request.patientEducation.CounsellingTopicId,
                            CounsellingDate = request.patientEducation.CounsellingDate,
                            Description = request.patientEducation.Description,
                        };

                         _unitOfWork.Repository<PatientEducation>().Update(patientEducation);
                        await _unitOfWork.SaveAsync();

                        return Result<EditPatientEducationCommadResult>.Valid(new EditPatientEducationCommadResult()
                        {
                            PatientEducationId = 1
                        });
                    }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<EditPatientEducationCommadResult>.Invalid(e.Message);
                }
            }
        }
    }
}
