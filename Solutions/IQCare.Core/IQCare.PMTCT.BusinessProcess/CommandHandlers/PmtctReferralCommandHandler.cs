using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models.Views;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class PmtctReferralCommandHandler : IRequestHandler<PmtctReferralCommand, Result<PmtctReferralView>>
    {

        private readonly IPmtctUnitOfWork _unitOfWork;

        public PmtctReferralCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PmtctReferralView>> Handle(PmtctReferralCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PmtctReferralView pmtctReferralView = await _unitOfWork.Repository<PmtctReferralView>()
                        .Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId==request.PatientMasterVisitId).FirstOrDefaultAsync();
                    return Result<PmtctReferralView>.Valid(pmtctReferralView);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<PmtctReferralView>.Invalid(e.Message);
                }
            }
        }
    }
}