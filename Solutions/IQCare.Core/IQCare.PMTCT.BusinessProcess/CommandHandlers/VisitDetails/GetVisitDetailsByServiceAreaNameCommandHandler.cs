using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.VisitDetails;
using IQCare.PMTCT.Core.Models.Views;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.VisitDetails
{
    public class GetVisitDetailsByServiceAreaNameCommandHandler :IRequestHandler<GetVisitDetailsByServiceAreaNameCommand, Result<List<VisitDetailsView>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetVisitDetailsByServiceAreaNameCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<VisitDetailsView>>> Handle(GetVisitDetailsByServiceAreaNameCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<VisitDetailsView> visitDetails = await _unitOfWork.Repository<VisitDetailsView>().Get(x =>
                        x.PatientId == request.PatientId && x.ServiceAreaName == request.ServiceAreaName).ToListAsync();
                    return Result<List<VisitDetailsView>>.Valid(visitDetails);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<VisitDetailsView>>.Invalid(e.Message);
                }
            }
        }
    }
}