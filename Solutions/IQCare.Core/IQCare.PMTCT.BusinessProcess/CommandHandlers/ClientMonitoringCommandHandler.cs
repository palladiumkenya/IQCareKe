

using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.PMTCT.Services;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class ClientMonitoringCommandHandler: IRequestHandler<ClientMonitoringCommand, Result<ClientMonitoringCommandResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int results = 0;

        public ClientMonitoringCommandHandler(ICommonUnitOfWork commonUnitOfWork,   IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ClientMonitoringCommandResponse>> Handle(ClientMonitoringCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    ClientMonitoringServices clientMonitoringService= new ClientMonitoringServices(_unitOfWork);

                    PatientWHOStage patientWhoStage = new PatientWHOStage()
                    {
                        PatientId=request.PatientId,
                        PatientMasterVisitId=request.PatientMasterVisitId,
                        WHOStage=request.WhoStage
                        
                    };

                   int patientWhoStageResult= await clientMonitoringService.AddPatientWhoStage(patientWhoStage);

                    int cacxTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "CaCxScreening").Select(x => x.MasterId).FirstOrDefaultAsync();
                    int tbscreeningTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "TBScreeningPMTCT").Select(x => x.MasterId).FirstOrDefaultAsync();
                    int cacxCategoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "CaCxMethod").Select(x => x.Id).FirstOrDefaultAsync();
                    int tbScreeningcaegoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "TBScreening").Select(x => x.Id).FirstOrDefaultAsync();

                    // CACXMethod       
                    PatientScreening patientScreeningCaCxMethod = new PatientScreening()
                    {
                        PatientId=request.PatientId,
                        PatientMasterVisitId=request.PatientMasterVisitId,
                        ScreeningTypeId= cacxTypeId,
                        ScreeningValueId=request.cacxResult,
                        ScreeningDone=true,
                        ScreeningDate=DateTime.Now,
                        ScreeningCategoryId= cacxCategoryId,
                        Comment = request.Comments,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    };

                    //TB Screening
                    PatientScreening patientScreeningTb = new PatientScreening()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ScreeningTypeId = tbscreeningTypeId,
                        ScreeningValueId=request.screenedTB,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Now,
                        ScreeningCategoryId = tbScreeningcaegoryId,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    };


                    int PatientScreeningResult = await clientMonitoringService.AddPatientScreening(patientScreeningCaCxMethod);
                    await clientMonitoringService.AddPatientScreening(patientScreeningTb);

                    PatientClinicalNotes patientClinicalNotes = new PatientClinicalNotes()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ServiceAreaId = request.ServiceAreaId,
                        ClinicalNotes = request.ClinicalNotes,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    
                    };

                    int clinicalNotesId =await clientMonitoringService.AddPatientClinicalNotes(patientClinicalNotes);

                    if(clinicalNotesId>0 & PatientScreeningResult>0 & patientWhoStageResult > 0)
                    {
                        results = 1;
                    }

                    return Result<ClientMonitoringCommandResponse>.Valid(new ClientMonitoringCommandResponse()
                    {
                        resultId = results
                    });

                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<ClientMonitoringCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
