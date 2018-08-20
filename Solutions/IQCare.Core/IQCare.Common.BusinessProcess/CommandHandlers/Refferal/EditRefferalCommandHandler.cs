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
    public class EditRefferalCommandHandler : IRequestHandler<EditRefferalCommand, Result<EditRefferalCommandResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public EditRefferalCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditRefferalCommandResponse>> Handle(EditRefferalCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientRefferal patientRefferal = await _unitOfWork.Repository<PatientRefferal>().FindAsync(x => x.PatientId == request.PatientRefferal.PatientId && x.PatientMasterVisitId == request.PatientRefferal.PatientMasterVisitId);

                    patientRefferal.ReferralDate = request.PatientRefferal.ReferralDate;
                    patientRefferal.ReferralReason = request.PatientRefferal.ReferralReason;
                    patientRefferal.ReferredBy = request.PatientRefferal.ReferredBy;
                    patientRefferal.ReferredFrom = request.PatientRefferal.ReferredFrom;
                    patientRefferal.ReferredTo = request.PatientRefferal.ReferredTo;
                    patientRefferal.DeleteFlag = 0;

                    _unitOfWork.Repository<PatientRefferal>().Update(patientRefferal);
                    var result = await _unitOfWork.SaveChangesAsync();
                    return Result<EditRefferalCommandResponse>.Valid(new EditRefferalCommandResponse()
                    {
                        Id=result
                    });

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<EditRefferalCommandResponse>.Invalid(e.Message);
                }
            }
                
        }
    }
}
