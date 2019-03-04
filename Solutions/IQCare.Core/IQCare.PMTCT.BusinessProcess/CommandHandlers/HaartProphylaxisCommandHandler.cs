using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.PMTCT.Services;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class HaartProphylaxisCommandHandler:IRequestHandler<HaartProphylaxisCommand,Result<HaartProphylaxisResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int result=0;

        public HaartProphylaxisCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HaartProphylaxisResponse>> Handle(HaartProphylaxisCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                HaartProphylaxisService _service=new HaartProphylaxisService(_unitOfWork);

                try
                {
                    List<PatientDrugAdministration> patientDrugAdministion_data = new List<PatientDrugAdministration>();
                   // List<PatientChronicIllness> patientChronicIllnesses_data = new List<PatientChronicIllness>();
                    List<PreventiveService> preventiveServices_data = new List<PreventiveService>();

                //  int result1=  await _service.AddPatientChronicIllness(request.PatientChronicIllnesses);
                  int result2=  await _service.AddPatientDrugAdministration(request.PatientDrugAdministration);

                 // int result3=  await _service.AddPreventiveService(request.preventiveServices);
                   if(result2 >0)
                    {
                        result = 1;
                    }

                  return   Result<HaartProphylaxisResponse>.Valid(new HaartProphylaxisResponse()
                    {

                        PreventivePartnerId=result
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    throw e;
                }
            }
        }
    }
}
