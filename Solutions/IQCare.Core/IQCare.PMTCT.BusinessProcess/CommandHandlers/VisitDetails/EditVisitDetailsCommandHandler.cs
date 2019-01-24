using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using IQCare.PMTCT.BusinessProcess.Commands.VisitDetails;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.VisitDetails
{
    public class EditVisitDetailsCommandHandler: IRequestHandler<EditVisitDetailsCommand, Result<Core.Models.VisitDetails>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditVisitDetailsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }


        public async Task<Result<Core.Models.VisitDetails>> Handle(EditVisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.VisitDetails visitDetails = await _unitOfWork.Repository<Core.Models.VisitDetails>()
                        .Get(x => x.Id == request.VisitDetails.Id).FirstOrDefaultAsync();
                    if (visitDetails != null)
                    {
                        visitDetails.VisitNumber = request.VisitDetails.VisitNumber;
                        visitDetails.VisitType = request.VisitDetails.VisitType;
                        visitDetails.DaysPostPartum = request.VisitDetails.DaysPostPartum;
                    }

                     _unitOfWork.Repository<Core.Models.VisitDetails>().Update(visitDetails);
                    await _unitOfWork.SaveAsync();
                    return Result<Core.Models.VisitDetails>.Valid(visitDetails);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Core.Models.VisitDetails>.Invalid(e.Message);
                }
            }
        }
    }
}