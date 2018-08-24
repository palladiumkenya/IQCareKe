using IQCare.Common.BusinessProcess.Commands.examination;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{

    public class UpdatePatientPhysicalExaminationCommandHandler : IRequestHandler<UpdatePatientPhysicalExaminationCommand, Result<UpdatePhysicalExaminationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public UpdatePatientPhysicalExaminationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdatePhysicalExaminationResponse>> Handle(UpdatePatientPhysicalExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientPhysicalExamination patientPhysicalExamination = new PatientPhysicalExamination()
                    {
                        PatientId = request.PatientPhysicalExamination.PatientId,
                        PatientMasterVisitId = request.PatientPhysicalExamination.PatientMasterVisitId,
                        ExamId = request.PatientPhysicalExamination.ExamId,
                        ExaminationTypeId = request.PatientPhysicalExamination.ExaminationTypeId,
                        FindingId = (request.PatientPhysicalExamination.FindingId == null) ? request.PatientPhysicalExamination.FindingId : 0,
                        FindingsNotes = (request.PatientPhysicalExamination.FindingsNotes == null) ? request.PatientPhysicalExamination.FindingsNotes : ""
                    };

                    await _unitOfWork.Repository<PatientPhysicalExamination>().AddAsync(patientPhysicalExamination);
                    int result = await _unitOfWork.SaveChangesAsync();

                    return Result<UpdatePhysicalExaminationResponse>.Valid(new UpdatePhysicalExaminationResponse()
                    {
                        Id = result
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<UpdatePhysicalExaminationResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
