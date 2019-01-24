using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public  class GetPatientPhysicalExaminationHandler : IRequestHandler<GetPatientPhysicalExaminationCommand, Result<GetPatientPhysicamExamResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientPhysicalExaminationHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPatientPhysicamExamResponse>> Handle(GetPatientPhysicalExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientPhysicalExamination>().Get(x => x.PatientId == request.patientId).FirstOrDefaultAsync();
                    return Result<GetPatientPhysicamExamResponse>.Valid(new GetPatientPhysicamExamResponse()
                    {
                        PatientPhysicamExamination = result
                    });
                }
                catch (Exception e)
                {
                Log.Error(e.Message);
                return Result<GetPatientPhysicamExamResponse>.Invalid(e.Message);
            }
            }
        }

    }
}
