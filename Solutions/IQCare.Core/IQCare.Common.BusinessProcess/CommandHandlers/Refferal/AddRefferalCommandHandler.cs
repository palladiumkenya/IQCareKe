using IQCare.Common.BusinessProcess.Commands.Refferal;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Refferal
{
    public class AddRefferalCommandHandler : IRequestHandler<AddPatientReferralCommand, Result<AddPatientReferralResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddRefferalCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddPatientReferralResponse>> Handle(AddPatientReferralCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientRefferal patientRefferal = new PatientRefferal(request.PatientId, request.PatientMasterVisitId, request.ReferredFrom, request.ReferredTo, request.ReferralReason, request.ReferralDate, request.ReferredBy, request.CreatedBy, request.DeleteFlag);
                   
                    await _unitOfWork.Repository<PatientRefferal>().AddAsync(patientRefferal);
                    await _unitOfWork.SaveAsync();

                    return Result<AddPatientReferralResponse>.Valid(new AddPatientReferralResponse()
                    {
                        PatientReferralId = patientRefferal.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<AddPatientReferralResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
