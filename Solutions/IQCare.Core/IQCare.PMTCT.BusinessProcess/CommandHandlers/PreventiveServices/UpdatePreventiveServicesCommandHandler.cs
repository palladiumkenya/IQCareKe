using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PreventiveServices
{
   
   public class UpdatePreventiveServicesCommandHandler:IRequestHandler<EditPatientPrevemtiveServiceCommand,Result<EditPreventiveServiceCommandResult>>
    {
        private readonly ICommonUnitOfWork _pmtctUnitOfWork;

        public UpdatePreventiveServicesCommandHandler(ICommonUnitOfWork pmtctUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork;
        }
        public async Task<Result<EditPreventiveServiceCommandResult>> Handle(EditPatientPrevemtiveServiceCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {
                    PreventiveService _preventiveService =await _pmtctUnitOfWork.Repository<PreventiveService>().FindAsync(x => x.PatientId == request.preventiveService.PatientId && x.Id == request.preventiveService.Id);
                    if (_preventiveService != null)
                    {
                        _preventiveService.PreventiveServiceId = request.preventiveService.PreventiveServiceId;
                        _preventiveService.PreventiveServiceDate = request.preventiveService.PreventiveServiceDate;
                    }
                    _pmtctUnitOfWork.Repository<PreventiveService>().Update(_preventiveService);
                   await  _pmtctUnitOfWork.SaveAsync();
                    _pmtctUnitOfWork.Dispose();

                    return Result<EditPreventiveServiceCommandResult>.Valid(new EditPreventiveServiceCommandResult()
                    {
                        PreventiveServiceId = 1
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<EditPreventiveServiceCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}
