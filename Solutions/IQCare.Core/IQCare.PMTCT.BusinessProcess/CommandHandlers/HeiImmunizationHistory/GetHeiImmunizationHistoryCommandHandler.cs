using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiImmunizationHistory
{
    public class GetHeiImmunizationHistoryCommandHandler: IRequestHandler<GetImmunizationHistoryCommand,Result<Vaccination>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetHeiImmunizationHistoryCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Vaccination>> Handle(GetImmunizationHistoryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                  var vaccine=  await _unitOfWork.Repository<Vaccination>().Get(x => x.PatientId == request.PatientId && x.DeleteFlag==0)
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    return Result<Vaccination>.Valid(vaccine);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<Vaccination>.Invalid(e.Message);
                }
            }
        }
    }
}
