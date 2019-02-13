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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EditPatientEducationCommadResult>> Handle(EditPatientEducationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var patientEducation = await _unitOfWork.Repository<PatientEducation>().FindByIdAsync(request.Id);
                    if(patientEducation==null)
                        return Result<EditPatientEducationCommadResult>.Invalid($"Patient Education for Id: {request.Id} could not be found");

                    patientEducation.Description = request.Description;
                    patientEducation.CounsellingTopicId = request.CounsellingTopicId;

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
