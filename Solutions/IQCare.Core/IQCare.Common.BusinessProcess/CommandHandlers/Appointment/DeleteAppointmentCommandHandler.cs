using System;
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
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Result<DeleteAppointmentReason>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public DeleteAppointmentCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<DeleteAppointmentReason>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var result = await _commontUnitOfWork.Repository<PatientAppointment>()
                        .Get(x => x.Id == request.AppointmentId && x.DeleteFlag == false).ToListAsync();
                    if (result.Count > 0)
                    {
                        result[0].DeleteFlag = true;

                        _commontUnitOfWork.Repository<PatientAppointment>().Update(result[0]);
                        await _commontUnitOfWork.SaveAsync();
                    }

                    return Result<DeleteAppointmentReason>.Valid(new DeleteAppointmentReason()
                    {
                        Message = "Successfully deleted appointment"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Could not delete appointmentId: {request.AppointmentId}");
                    return Result<DeleteAppointmentReason>.Invalid($"Could not delete appointmentId: {request.AppointmentId}");
                }
            }
        }
    }
}