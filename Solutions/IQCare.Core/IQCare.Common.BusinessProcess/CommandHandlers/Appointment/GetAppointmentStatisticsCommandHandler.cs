using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class GetAppointmentStatisticsCommandHandler : IRequestHandler<GetAppointmentStatisticsCommand, Result<List<AppointmentSummary>>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public GetAppointmentStatisticsCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<List<AppointmentSummary>>> Handle(GetAppointmentStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commontUnitOfWork.Repository<AppointmentSummary>().Get(x => x.AppointmentDate.Date == request.summaryDate.Date).ToListAsync();
                return Result<List<AppointmentSummary>>.Valid(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching Facility Appointment statistics");
                return Result<List<AppointmentSummary>>.Invalid(ex.Message);
            }
        }
    }
}
