using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Appointment;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class GetPatientAncAppointmentCommandHandler: IRequestHandler<GetAncAppointmentCommand, Library.Result<PatientAppointment>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientAncAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientAppointment>> Handle(GetAncAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientAppointment patientAppointment = await _unitOfWork.Repository<PatientAppointment>()
                        .Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId==request.PatientMasterVisitId
                                  && x.Description=="ANC Follow-up").FirstOrDefaultAsync();
                    return Result<PatientAppointment>.Valid(new PatientAppointment());
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                                        return Library.Result<PatientAppointment>.Invalid(e.Message);
                }
            }
        }
    }
}