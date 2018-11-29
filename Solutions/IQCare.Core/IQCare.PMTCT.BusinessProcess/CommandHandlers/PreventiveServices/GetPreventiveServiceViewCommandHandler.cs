using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices;
using IQCare.PMTCT.Core.Models.Views;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PreventiveServices
{
    public class GetPreventiveServiceViewCommandHandler :  IRequestHandler<GetPreventiveServiceViewCommand, Result<List<PatientPreventiveServiceView>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPreventiveServiceViewCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientPreventiveServiceView>>> Handle(GetPreventiveServiceViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientPreventiveServiceView> patientPreventiveServiceViews = await _unitOfWork
                        .Repository<PatientPreventiveServiceView>().Get(x => x.PatientId == request.PatientId)
                        .ToListAsync();
                    return Result<List<PatientPreventiveServiceView>>.Valid(patientPreventiveServiceViews);
                }
                catch (Exception e)
                {
                    Log.Error("An error occured while get patient preventive service info", e);
                    return Result<List<PatientPreventiveServiceView>>.Invalid(e.Message);
                }
            }
        }
    }
}