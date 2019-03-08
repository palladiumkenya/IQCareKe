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
    public class AddPatientPreventiveServicesCommandHandler : IRequestHandler<PatientPreventiveServiceCommand, Result<AddPatientPreventiveServiceCommandResponse>>
    {
       private readonly IPmtctUnitOfWork _pmtctUnitOfWork;

        public AddPatientPreventiveServicesCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork ?? throw new ArgumentNullException(nameof(pmtctUnitOfWork));
        }
        public async Task<Result<AddPatientPreventiveServiceCommandResponse>> Handle(PatientPreventiveServiceCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {
                    List<PreventiveService> preventiveServices = new List<PreventiveService>();

                    List<PreventiveService> preventiveServicesExist = _pmtctUnitOfWork
                        .Repository<PreventiveService>().Get(x => x.PatientId == request.preventiveService[0].PatientId)
                        .ToList();

                    if (request.preventiveService.Count > 0)
                    {
                        
                        
                        foreach (var item in request.preventiveService)
                        {
                            bool itemExists = preventiveServicesExist.Exists(x =>
                                x.PreventiveServiceId == item.PreventiveServiceId && x.PatientId == item.PatientId
                                                                                  && x.PreventiveServiceDate ==
                                                                                  item.PreventiveServiceDate);
                            if (!itemExists)
                            {
                                PreventiveService _preventiveServices = new PreventiveService()
                                {
                                    Id = item.Id,
                                    PatientId = item.PatientId,
                                    PatientMasterVisitId = item.PatientMasterVisitId,
                                    PreventiveServiceId = item.PreventiveServiceId,
                                    PreventiveServiceDate = item.PreventiveServiceDate,
                                    Description = item.Description
                                };
                                preventiveServices.Add(_preventiveServices);
                            }
                         
                        }
                        await _pmtctUnitOfWork.Repository<PreventiveService>().AddRangeAsync(preventiveServices);
                        await _pmtctUnitOfWork.SaveAsync();
                        return Result<AddPatientPreventiveServiceCommandResponse>.Valid(new AddPatientPreventiveServiceCommandResponse()
                        {
                            PreventiveServiceId = 1

                        });
                    }
                    else
                    {
                        return Result<AddPatientPreventiveServiceCommandResponse>.Valid(new AddPatientPreventiveServiceCommandResponse {PreventiveServiceId = 1});
                    }                
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<AddPatientPreventiveServiceCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
