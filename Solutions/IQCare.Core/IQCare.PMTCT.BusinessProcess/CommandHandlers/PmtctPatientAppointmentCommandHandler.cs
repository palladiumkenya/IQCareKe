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
    public class PmtctPatientAppointmentCommandHandler: IRequestHandler<GetPatientAppointmentCommand, Result<List<PatientAppointment>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public PmtctPatientAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientAppointment>>> Handle(GetPatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientAppointment> patientAppointment = await _unitOfWork.Repository<PatientAppointment>()
                        .Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<PatientAppointment>>.Valid(patientAppointment);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<List<PatientAppointment>>.Invalid(e.Message);
                }
            }
        }
    }
}