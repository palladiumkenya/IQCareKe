using System;
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
    public class AddPatientIptOutcomeCommandHandler:IRequestHandler<AddHeiPaientIptOutcomeCommand,Result<PatientIptOutcome>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientIptOutcomeCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIptOutcome>> Handle(AddHeiPaientIptOutcomeCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    await _unitOfWork.Repository<PatientIptOutcome>().AddAsync(request.PatientIptOutcome);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientIptOutcome>.Valid(request.PatientIptOutcome);
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