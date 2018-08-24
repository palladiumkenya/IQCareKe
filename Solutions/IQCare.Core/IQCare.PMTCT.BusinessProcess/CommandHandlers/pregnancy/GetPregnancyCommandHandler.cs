using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.pregnancy
{
    public class GetPregnancyCommandHandler: IRequestHandler<GetPregnancyCommand,Result<PatientPregnancy>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPregnancyCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientPregnancy>> Handle(GetPregnancyCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientPregnancy result = _unitOfWork.Repository<PatientPregnancy>().Get(x => x.PatientId == request.PatientId  && x.Outcome<1).FirstOrDefault();
                    return (result != null) ? Result<PatientPregnancy>.Valid(result) : Result<PatientPregnancy>.Valid(new PatientPregnancy());                   
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientPregnancy>.Invalid(e.Message);
                }
            }
        }
    }
}
