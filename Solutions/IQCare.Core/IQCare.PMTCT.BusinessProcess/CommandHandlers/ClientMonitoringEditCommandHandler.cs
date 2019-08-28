using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PatientScreening = IQCare.PMTCT.Core.Models.PatientScreening;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class ClientMonitoringEditCommandHandler : IRequestHandler<ClientMonitoringEditCommand, Result<ClientMonitoringCommandEditResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int results = 0;

        public ClientMonitoringEditCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ClientMonitoringCommandEditResponse>> Handle(ClientMonitoringEditCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                ClientMonitoringServices clientMonitoringService = new ClientMonitoringServices(_unitOfWork);
                PatientWhoStage patientWhoStage =
                    clientMonitoringService.GetPatientWhoStage(request.PatientId, request.PatientMasterVisitId);

                patientWhoStage.WHOStage = request.WhoStage;
                int whoId = await clientMonitoringService.EditPatientWhoStage(patientWhoStage);
                int pmtctScreeningId = 0;
                int vlSampleTakenId = 0;
                int tbScreeingId = 0;
                int cacxScreeingId = 0;
                int vlSampleTypeId = await _commonUnitOfWork.Repository<LookupMaster>()
                    .Get(x => x.Name == "ViralLoadSampleTaken").Select(x => x.Id).FirstOrDefaultAsync();
                string yesNoId = await _commonUnitOfWork.Repository<LookupItem>()
                    .Get(x => x.Id == request.ViralLoadSampleTaken).Select(x => x.Name).FirstOrDefaultAsync();
                int patientWhoStageResult = await clientMonitoringService.EditPatientWhoStage(patientWhoStage);
                int tbscreeningTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "TBScreeningPMTCT").Select(x => x.MasterId).FirstOrDefaultAsync();
                int tbScreeningcaegoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "TBScreening").Select(x => x.Id).FirstOrDefaultAsync();


                PatientScreening viralLoadSamplePatientScreening =
                    clientMonitoringService.GetPatientScreening(request.PatientId, request.PatientMasterVisitId, vlSampleTypeId);

                if (viralLoadSamplePatientScreening != null)
                {
                    viralLoadSamplePatientScreening.ScreeningTypeId = vlSampleTypeId;
                    viralLoadSamplePatientScreening.ScreeningValueId = request.ViralLoadSampleTaken;
                    viralLoadSamplePatientScreening.ScreeningDone = (yesNoId == "Yes") ? true : false;
                    viralLoadSamplePatientScreening.ScreeningCategoryId = vlSampleTypeId;
                    viralLoadSamplePatientScreening.Comment = request.Comments;
                    vlSampleTypeId =
                        await clientMonitoringService.EditPatientScreening(viralLoadSamplePatientScreening);

                }
                else
                {
                    vlSampleTypeId = 0; }

                PatientScreening tbScreeningPmtctPatientScreening =
                    clientMonitoringService.GetPatientScreening(request.PatientId, request.PatientMasterVisitId, tbscreeningTypeId);
                if (tbScreeningPmtctPatientScreening != null)
                {
                    tbScreeningPmtctPatientScreening.ScreeningTypeId = tbscreeningTypeId;
                    tbScreeningPmtctPatientScreening.ScreeningValueId = request.screenedTB;
                    tbScreeningPmtctPatientScreening.ScreeningDone = (yesNoId == "Yes") ? true : false;
                    tbScreeningPmtctPatientScreening.ScreeningCategoryId = tbScreeningcaegoryId;
                    tbScreeningPmtctPatientScreening.Comment = request.Comments;
                    pmtctScreeningId =
                        await clientMonitoringService.EditPatientScreening(tbScreeningPmtctPatientScreening);
                } else {
                    pmtctScreeningId = 0;
                }

                    PatientScreening tbScreeningPatientScreening =
                        clientMonitoringService.GetPatientScreening(request.PatientId, request.PatientMasterVisitId, tbScreeningcaegoryId);
                    if (tbScreeningPatientScreening != null)
                    {
                        tbScreeningPatientScreening.ScreeningTypeId = tbScreeningcaegoryId;
                        tbScreeningPatientScreening.ScreeningValueId = request.screenedTB;
                        tbScreeningPatientScreening.ScreeningDone = (yesNoId == "Yes") ? true : false;
                        tbScreeningPatientScreening.ScreeningCategoryId = tbScreeningcaegoryId;
                        tbScreeningPatientScreening.Comment = request.Comments;
                        tbScreeingId =
                            await clientMonitoringService.EditPatientScreening(tbScreeningPatientScreening);
                    }
                
                if(tbScreeningPatientScreening == null  && tbScreeningPmtctPatientScreening== null)
                { 


                    //TB Screening
                    PatientScreening patientScreeningTb = new PatientScreening()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ScreeningTypeId = tbscreeningTypeId,
                        ScreeningValueId = request.screenedTB,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Now,
                        ScreeningCategoryId = tbScreeningcaegoryId,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    };

                    tbScreeingId=await clientMonitoringService.AddPatientScreening(patientScreeningTb);
                }





                
                    int cacxTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "CaCxScreening").Select(x => x.MasterId).FirstOrDefaultAsync();
                    int cacxCategoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "CaCxMethod").Select(x => x.Id).FirstOrDefaultAsync();

                    PatientScreening cacxScreeningPatientScreening =
                    clientMonitoringService.GetPatientScreening(request.PatientId, request.PatientMasterVisitId, cacxTypeId);
                if (cacxScreeningPatientScreening != null)
                {
                     cacxScreeningPatientScreening.ScreeningTypeId = cacxTypeId;
                     cacxScreeningPatientScreening.ScreeningValueId = request.cacxResult;
                        cacxScreeningPatientScreening.ScreeningDone = (yesNoId == "Yes") ? true : false;
                        cacxScreeningPatientScreening.ScreeningCategoryId = request.cacxMethod;
                        cacxScreeningPatientScreening.Comment = request.ClinicalNotes;
                    cacxScreeingId =
                        await clientMonitoringService.EditPatientScreening(cacxScreeningPatientScreening);
                }
                else
                {
                    //TB Screening
                    PatientScreening patientScreeningcacx = new PatientScreening()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ScreeningTypeId = cacxTypeId,
                        ScreeningValueId = request.cacxResult,
                        ScreeningDone = request.ScreeningDone,
                        ScreeningDate = DateTime.Now,
                        ScreeningCategoryId = request.cacxMethod,
                        Comment = request.ClinicalNotes,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    };

                    cacxScreeingId = await clientMonitoringService.AddPatientScreening(patientScreeningcacx);
                }







                if (tbScreeingId>0 & pmtctScreeningId>0 & vlSampleTypeId>0 & cacxScreeingId >0)
                    return Result<ClientMonitoringCommandEditResponse>.Valid(new ClientMonitoringCommandEditResponse {resultId = 1});
                return Result<ClientMonitoringCommandEditResponse>.Valid(new ClientMonitoringCommandEditResponse {resultId = 0});
            }
        }
    }
}