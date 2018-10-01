using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiImmunizationHistory
{
    public class EditImmunizationHistoryCommandHandler: IRequestHandler<EditImmunizationHistoryCommand,Result<Vaccination>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditImmunizationHistoryCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Vaccination>> Handle(EditImmunizationHistoryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var vacc = _unitOfWork.Repository<Vaccination>().Get(x =>
                        x.PatientId == request.Vaccination.PatientId &&
                        x.PatientMasterVisitId == request.Vaccination.PatientMasterVisitId).FirstOrDefault();
                    if (vacc != null)
                    {
                        vacc.AppointmentId = request.Vaccination.PatientId;
                        vacc.Period = request.Vaccination.Period;
                        vacc.VaccineDate = request.Vaccination.VaccineDate;
                        vacc.VaccineStage = request.Vaccination.VaccineStage;
                    }

                     _unitOfWork.Repository<Vaccination>().Update(vacc);
                    await _unitOfWork.SaveAsync();
                    return Result<Vaccination>.Valid(vacc);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<Vaccination>.Invalid(e.Message);
                }
            }
        }
    }
}
