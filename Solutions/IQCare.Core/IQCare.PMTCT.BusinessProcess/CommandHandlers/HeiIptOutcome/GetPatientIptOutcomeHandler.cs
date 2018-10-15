using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptOutcome;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiIptOutcome
{
    public class GetPatientIptOutcomeHandler: IRequestHandler<EditHeiPatientIptOutcomeCommand,Result<PatientIptOutcome>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientIptOutcomeHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIptOutcome>> Handle(EditHeiPatientIptOutcomeCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientIptOutcome patientIptOutcome = _unitOfWork.Repository<PatientIptOutcome>()
                        .Get(x => x.PatientId == request.PatientIptOutcome.PatientId && !x.DeleteFlag).FirstOrDefault();
                    return Result<PatientIptOutcome>.Valid(patientIptOutcome);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientIptOutcome>.Invalid(e.Message);
                }
            }
        }
    }
}