using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetBaselineAncCommandHandler : IRequestHandler<GetBaselineAntenatalCareCommand, Result<Core.Models.BaselineAntenatalCare>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetBaselineAncCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<Core.Models.BaselineAntenatalCare>> Handle(GetBaselineAntenatalCareCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.BaselineAntenatalCare baselineAntenatalCare = await _unitOfWork
                        .Repository<Core.Models.BaselineAntenatalCare>().Get(x => x.PatientId == request.PatientId)
                        .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    return Result<Core.Models.BaselineAntenatalCare>.Valid(baselineAntenatalCare);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Core.Models.BaselineAntenatalCare>.Invalid(e.Message);
                }
            }
        }
    }
}