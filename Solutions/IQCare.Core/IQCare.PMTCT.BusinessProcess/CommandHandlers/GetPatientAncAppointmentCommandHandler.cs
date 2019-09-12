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
    public class GetPatientAncAppointmentCommandHandler: IRequestHandler<GetAncAppointmentCommand, Result<List<PatientAppointment>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientAncAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientAppointment>>> Handle(GetAncAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var patientAppointment = await _unitOfWork.Repository<PatientAppointment>()
                        .Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId
                                  && x.PatientMasterVisitId > 0).ToListAsync();
                    return Result<List<PatientAppointment>>.Valid(patientAppointment);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<PatientAppointment>>.Invalid(e.Message);
                }
            }
        }
    }
}