using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.BaselineANC;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.BaselineAntenatalCare
{
    public class DeleteBaselineAncCommandHandler : IRequestHandler<DeleteBaselineAntenatalCareCommand, Result<DeleteBaselineAntenatalCareResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public DeleteBaselineAncCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<DeleteBaselineAntenatalCareResponse>> Handle(DeleteBaselineAntenatalCareCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.BaselineAntenatalCare baselineAntenatalCare = await _unitOfWork
                        .Repository<Core.Models.BaselineAntenatalCare>().Get(x => x.Id == request.Id)
                        .FirstOrDefaultAsync();
                    if (baselineAntenatalCare != null)
                    {
                        baselineAntenatalCare.DeleteFlag = true;
                    }
                    return Result<DeleteBaselineAntenatalCareResponse>.Valid(new DeleteBaselineAntenatalCareResponse()
                    {
                        Id=request.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<DeleteBaselineAntenatalCareResponse>.Invalid(e.Message);
                }
            }
        }
    }
}