using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIpt;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptOutcome;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiIpt;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiIptOutcome
{
    public class EditPatientIptOutcomeCommandHandler: IRequestHandler<EditHeiPatientIptOutcomeCommand,Result<PatientIptOutcome>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditPatientIptOutcomeCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIptOutcome>> Handle(EditHeiPatientIptOutcomeCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientIptOutcome patientIptOutcome = _unitOfWork.Repository<PatientIptOutcome>().Get(x =>
                            x.PatientId == request.PatientIptOutcome.PatientId && x.Id == request.PatientIptOutcome.Id)
                        .FirstOrDefault();
                    if (patientIptOutcome != null)
                    {
                        patientIptOutcome.IptEvent = request.PatientIptOutcome.IptEvent;
                        patientIptOutcome.ReasonForDiscontinuation = request.PatientIptOutcome.ReasonForDiscontinuation;
                        _unitOfWork.Repository<PatientIptOutcome>().Update(patientIptOutcome);
                        await _unitOfWork.SaveAsync();
                    }

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