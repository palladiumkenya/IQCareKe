using IQCare.Common.BusinessProcess.Commands.Refferal;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Refferal
{
    public class AddRefferalCommandHandler : IRequestHandler<AddRefferalCommand, Result<AddRefferalCommandResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddRefferalCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddRefferalCommandResponse>> Handle(AddRefferalCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientRefferal patientRefferal = new PatientRefferal()
                    {
                        PatientId = request.patientRefferal.PatientId,
                        PatientMasterVisitId = request.patientRefferal.PatientMasterVisitId,
                        ReferralDate = request.patientRefferal.ReferralDate,
                        ReferralReason = request.patientRefferal.ReferralReason,
                        ReferredBy = request.patientRefferal.ReferredBy,
                        ReferredFrom = request.patientRefferal.ReferredFrom,
                        ReferredTo = request.patientRefferal.ReferredTo,
                        DeleteFlag = 0,
                    };
                  await  _unitOfWork.Repository<PatientRefferal>().AddAsync(patientRefferal);
                    await _unitOfWork.SaveAsync();
                    return Result<AddRefferalCommandResponse>.Valid(new AddRefferalCommandResponse()
                    {
                        Id = 1
                    });
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<AddRefferalCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
