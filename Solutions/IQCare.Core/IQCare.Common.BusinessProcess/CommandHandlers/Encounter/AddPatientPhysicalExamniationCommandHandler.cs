using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddPatientPhysicalExamniationCommandHandler : IRequestHandler<AddPatientPhysicalExaminationCommand, Result<AddPatientPhysicalExamResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddPatientPhysicalExamniationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddPatientPhysicalExamResponse>> Handle(AddPatientPhysicalExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientPhysicalExamination patientPhysicalExamination = new PatientPhysicalExamination()
                    {
                        PatientId = request.PatientPhysicalExamination.PatientId,
                        PatientMasterVisitId =request.PatientPhysicalExamination.PatientMasterVisitId,
                        ExamId =request.PatientPhysicalExamination.ExamId,
                        ExaminationTypeId =request.PatientPhysicalExamination .ExaminationTypeId,
                        FindingId = (request.PatientPhysicalExamination.FindingId == null) ? request.PatientPhysicalExamination.FindingId : 0,
                        FindingsNotes = (request.PatientPhysicalExamination.FindingsNotes == null) ? request.PatientPhysicalExamination.FindingsNotes : ""
                    };

                    await _unitOfWork.Repository<PatientPhysicalExamination>().AddAsync(patientPhysicalExamination);
                    await _unitOfWork.SaveAsync();

                   return  Result<AddPatientPhysicalExamResponse>.Valid(new AddPatientPhysicalExamResponse() {
                        PatientPhysicalExamId = 1
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<AddPatientPhysicalExamResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
