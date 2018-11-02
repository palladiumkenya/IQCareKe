using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.pregnancy
{
    public class AddPregnancyCommandHandler: IRequestHandler<AddPregnancyCommand, Result<AddPregnancyCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPregnancyCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddPregnancyCommandResult>> Handle(AddPregnancyCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   await _unitOfWork.Repository<PatientPregnancy>().AddAsync(request.Pregnancy);
                    await _unitOfWork.SaveAsync();
                    return Result<AddPregnancyCommandResult>.Valid(new AddPregnancyCommandResult()
                    {
                        PatientId = request.Pregnancy.PatientId,PregnancyId = request.Pregnancy.Id
                            
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<AddPregnancyCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}