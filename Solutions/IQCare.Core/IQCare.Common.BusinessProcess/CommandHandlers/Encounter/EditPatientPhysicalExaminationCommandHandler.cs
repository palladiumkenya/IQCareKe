using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    class EditPatientPhysicalExaminationCommandHandler : IRequestHandler<EditPatientPhysicalExaminationCommand, Result<EditPatientPhysicalExamResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public EditPatientPhysicalExaminationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditPatientPhysicalExamResponse>> Handle(EditPatientPhysicalExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                 try
                {
                    PatientPhysicalExamination patientPhysicalExamination = await  _unitOfWork.Repository<PatientPhysicalExamination>().FindAsync(x=>x.Id==request.PatientPhysicalExamination.Id);

                    patientPhysicalExamination.PatientId = request.PatientPhysicalExamination.PatientId;
                    patientPhysicalExamination.PatientMasterVisitId = request.PatientPhysicalExamination.PatientMasterVisitId;
                    patientPhysicalExamination.ExamId = request.PatientPhysicalExamination.ExamId;
                    patientPhysicalExamination.ExaminationTypeId = request.PatientPhysicalExamination.ExaminationTypeId;
                    patientPhysicalExamination.FindingId = request.PatientPhysicalExamination.FindingId;
                    patientPhysicalExamination.FindingsNotes = request.PatientPhysicalExamination.FindingsNotes;

                    _unitOfWork.Repository<PatientPhysicalExamination>().Update(patientPhysicalExamination);
                    int result = await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Dispose();

                    return Result<EditPatientPhysicalExamResponse>.Valid(new EditPatientPhysicalExamResponse()
                    {
                        PatientPhysicalExamId = result

                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<EditPatientPhysicalExamResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
