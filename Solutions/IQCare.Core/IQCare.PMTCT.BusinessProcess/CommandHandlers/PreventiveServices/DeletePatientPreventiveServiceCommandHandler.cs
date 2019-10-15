using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PreventiveServices
{
    public class DeletePatientPreventiveServiceCommandHandler : IRequestHandler<DeletePatientPreventiveServiceCommand, Result<DeletePreventiveServiceCommandResult>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;

        private int preventiveServiceId { get; set; }
        public DeletePatientPreventiveServiceCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork ?? throw new ArgumentNullException(nameof(pmtctUnitOfWork));

        }


        public async Task<Result<DeletePreventiveServiceCommandResult>> Handle(DeletePatientPreventiveServiceCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {

                    if(request.Id > 0)
                    {
                        var preventiveservices =  _pmtctUnitOfWork.Repository<PreventiveService>().Get(x => x.Id == request.Id).FirstOrDefault();

                        if(preventiveservices !=null)
                        {
                            preventiveservices.DeleteFlag = true;

                            _pmtctUnitOfWork.Repository<PreventiveService>().Update(preventiveservices);
                            await _pmtctUnitOfWork.SaveAsync();
                            preventiveServiceId = 1;
                        }
                        else
                        {
                           preventiveServiceId = 0;
                        }

                       
                    } 
                    else
                    {
                        preventiveServiceId = 0;
                    }


                    return Result<DeletePreventiveServiceCommandResult>.Valid(new DeletePreventiveServiceCommandResult()
                    {
                        PreventiveServiceId = preventiveServiceId

                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<DeletePreventiveServiceCommandResult>.Invalid(ex.Message);

                }
            }
        }

    }
}
