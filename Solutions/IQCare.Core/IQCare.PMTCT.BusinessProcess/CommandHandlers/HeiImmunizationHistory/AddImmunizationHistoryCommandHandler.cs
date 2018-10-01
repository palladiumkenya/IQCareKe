using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiImmunizationHistory
{
    public class AddImmunizationHistoryCommandHandler: IRequestHandler<AddImmunizationHistoryCommand, Result<Vaccination>>
   {
       private readonly IPmtctUnitOfWork _unitOfWork;

       public AddImmunizationHistoryCommandHandler(IPmtctUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public async Task<Result<Vaccination>> Handle(AddImmunizationHistoryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    await _unitOfWork.Repository<Vaccination>().AddAsync(request.Vaccination);
                    await _unitOfWork.SaveAsync();
                    return Result<Vaccination>.Valid(request.Vaccination);
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
