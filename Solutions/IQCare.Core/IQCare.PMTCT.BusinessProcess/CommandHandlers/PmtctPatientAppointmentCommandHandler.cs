using System;
using System.Collections.Generic;
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
    public class PmtctPatientAppointmentCommandHandler: IRequestHandler<GetPatientAppointmentCommand, Result<PatientAppointment>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public PmtctPatientAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PatientAppointment>> Handle(GetPatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientAppointment patientAppointment = await _unitOfWork.Repository<PatientAppointment>()
                        .Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId==request.PatientMasterVisitId).FirstOrDefaultAsync();
                    return Result<PatientAppointment>.Valid(patientAppointment);
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