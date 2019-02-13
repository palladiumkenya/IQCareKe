using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.VisitDetails;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.VisitDetails
{
    public class DeleteVisitDetailsCommandHandler: IRequestHandler<Commands.VisitDetails.DeleteVisitDetailsCommand, Result<DeleteVisitDetailsCommandResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public DeleteVisitDetailsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<DeleteVisitDetailsCommandResponse>> Handle(DeleteVisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.VisitDetails visitDetails = await _unitOfWork.Repository<Core.Models.VisitDetails>()
                        .Get(x => x.Id ==Int32.Parse(request.Id.ToString())).FirstOrDefaultAsync();

                    if (visitDetails != null)
                    {
                        visitDetails.DeleteFlag = true;
                    }
                    
                    _unitOfWork.Repository<Core.Models.VisitDetails>().Update(visitDetails);
                    await _unitOfWork.SaveAsync();
                    return Result<DeleteVisitDetailsCommandResponse>.Valid(new DeleteVisitDetailsCommandResponse()
                    {
                        Id = request.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<DeleteVisitDetailsCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}