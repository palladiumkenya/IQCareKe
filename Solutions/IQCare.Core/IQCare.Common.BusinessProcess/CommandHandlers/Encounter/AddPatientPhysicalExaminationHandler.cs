using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    class AddPatientPhysicalExaminationHandler : IRequestHandler<AddPatientPhysicalExaminationCommand, Result<AddPatientPhysicalExamResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddPatientPhysicalExaminationHandler(ICommonUnitOfWork unitOfWork)
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
                        PatientId=request.PatientPhysicalExamination.PatientId,
                        PatientMasterVisitId=request.PatientPhysicalExamination.PatientMasterVisitId,
                        ExamId=request.PatientPhysicalExamination.ExamId,
                        ExaminationTypeId=request.PatientPhysicalExamination.ExaminationTypeId,
                        FindingId=request.PatientPhysicalExamination.FindingId,
                        FindingsNotes=request.PatientPhysicalExamination.FindingsNotes
                    };

                    await _unitOfWork.Repository<PatientPhysicalExamination>().AddAsync(patientPhysicalExamination);
                    int result= await  _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPatientPhysicalExamResponse>.Valid(new AddPatientPhysicalExamResponse()
                    {
                        PatientPhysicalExamId=result
                    
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
