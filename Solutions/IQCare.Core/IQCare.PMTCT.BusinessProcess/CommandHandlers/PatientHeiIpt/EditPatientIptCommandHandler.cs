using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiIpt;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PatientHeiIpt
{
    public class EditPatientIptCommandHandler:IRequestHandler<EditHeiPatientIptCommand,Result<PatientIpt>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditPatientIptCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIpt>> Handle(EditHeiPatientIptCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientIpt patientIpt = _unitOfWork.Repository<PatientIpt>()
                        .Get(x => x.PatientId == request.PatientIpt.PatientId && !x.DeleteFlag).FirstOrDefault();
                    if (patientIpt != null)
                    {
                        patientIpt.AdheranceMeasurement = request.PatientIpt.AdheranceMeasurement;
                        patientIpt.AdheranceMeasurementAction = request.PatientIpt.AdheranceMeasurementAction;
                        patientIpt.Hepatotoxicity = request.PatientIpt.Hepatotoxicity;
                        patientIpt.IptDateCollected = request.PatientIpt.IptDateCollected;
                        patientIpt.IptDueDate = request.PatientIpt.IptDueDate;
                    }

                    _unitOfWork.Repository<PatientIpt>().Update(patientIpt);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientIpt>.Valid(request.PatientIpt);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientIpt>.Invalid(e.Message);
                }
            }
        }
    }
}