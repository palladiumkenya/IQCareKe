using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PreventiveServices
{
    public class GetPreventiveServicesCommandHandler : IRequestHandler<GetPatientPrevenetiveServicesCommand, Result<List<PreventiveService>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPreventiveServicesCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PreventiveService>>> Handle(GetPatientPrevenetiveServicesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PreventiveService> result = _unitOfWork.Repository<PreventiveService>().Get(x => x.PatientId == request.PatientId).ToList();
                    return Result<List<PreventiveService>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PreventiveService>>.Invalid(e.Message);
                }
                
            }          
        }
    }
}
