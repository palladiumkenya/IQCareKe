using IQCare.Common.BusinessProcess.Commands.Appointment;
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
    public class GetAllCCCVisitCountCommandHandler : IRequestHandler<GetAllCCCVisitCountCommand, Result<CCCVisitCountResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public GetAllCCCVisitCountCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<CCCVisitCountResponse>> Handle(GetAllCCCVisitCountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_commontUnitOfWork)
                {
                    var result = await _commontUnitOfWork.Repository<IQCare.Common.Core.Models.PatientMasterVisit>().Get(x => x.VisitDate.Value.Date == request.SummaryDate.Date && x.ServiceId == 1).CountAsync();
                    return Result<CCCVisitCountResponse>.Valid(new CCCVisitCountResponse() { TotalVisits = result });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching total CCC visits");
                return Result<CCCVisitCountResponse>.Invalid(ex.Message);
            }
        }
    }
}
