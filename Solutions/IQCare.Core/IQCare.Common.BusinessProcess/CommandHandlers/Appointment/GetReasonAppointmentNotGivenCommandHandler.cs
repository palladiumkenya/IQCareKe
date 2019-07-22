using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class GetReasonAppointmentNotGivenCommandHandler : IRequestHandler<GetReasonAppointmentNotGivenCommand, Result<List<PatientAppointmentReasons>>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public GetReasonAppointmentNotGivenCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<List<PatientAppointmentReasons>>> Handle(GetReasonAppointmentNotGivenCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var result = await _commontUnitOfWork.Repository<PatientAppointmentReasons>().Get(x =>
                            x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId)
                        .ToListAsync();

                    return Result<List<PatientAppointmentReasons>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"An error occured while trying to fetch patient appointment reason for patientid: {request.PatientId}");
                    return Result<List<PatientAppointmentReasons>>.Invalid($"An error occured while trying to fetch patient appointment reason for patientid: {request.PatientId}");
                }
            }
        }
    }
}