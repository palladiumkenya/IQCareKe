

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
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.PMTCT.Services;
using PatientClinicalNotes = IQCare.PMTCT.Core.Models.PatientClinicalNotes;
using PatientScreening = IQCare.PMTCT.Core.Models.PatientScreening;

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
                int PatientScreeningResult = 0;
                try
                {
                    ClientMonitoringServices clientMonitoringService= new ClientMonitoringServices(_unitOfWork);

                    PatientWhoStage patientWhoStage = new PatientWhoStage()
                    {
                        PatientId=request.PatientId,
                        PatientMasterVisitId=request.PatientMasterVisitId,
                        WHOStage=request.WhoStage
                        
                    };

                    int vlSampleTypeId = await _commonUnitOfWork.Repository<LookupMaster>()
                        .Get(x => x.Name == "ViralLoadSampleTaken").Select(x => x.Id).FirstOrDefaultAsync();
                    string yesNoId = await _commonUnitOfWork.Repository<LookupItem>()
                        .Get(x => x.Id == request.ViralLoadSampleTaken).Select(x => x.Name).FirstOrDefaultAsync();
                    int patientWhoStageResult= await clientMonitoringService.AddPatientWhoStage(patientWhoStage);
                    int tbscreeningTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "TBScreeningPMTCT").Select(x => x.MasterId).FirstOrDefaultAsync();
                    int tbScreeningcaegoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "TBScreening").Select(x => x.Id).FirstOrDefaultAsync();

                    if (vlSampleTypeId>0)
                    {
                        PatientScreening patientViralLoadScreening = new PatientScreening()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            ScreeningTypeId = vlSampleTypeId,
                            ScreeningValueId = request.ViralLoadSampleTaken,
                            ScreeningDone = (yesNoId=="Yes")? true: false,
                            ScreeningDate = DateTime.Now,
                            ScreeningCategoryId = vlSampleTypeId,
                            Comment = request.Comments,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.CreatedBy
                        };
                        PatientScreeningResult = await clientMonitoringService.AddPatientScreening(patientViralLoadScreening);
                    }
                    if (request.ScreeningDone)
                    {
                        int cacxTypeId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItemView>().Get(x => x.MasterName == "CaCxScreening").Select(x => x.MasterId).FirstOrDefaultAsync();
                        int cacxCategoryId = await _commonUnitOfWork.Repository<Common.Core.Models.LookupItem>().Get(x => x.Name == "CaCxMethod").Select(x => x.Id).FirstOrDefaultAsync();

                        // CACXMethod       
                        PatientScreening patientScreeningCaCxMethod = new PatientScreening()
                            {
                                PatientId=request.PatientId,
                                PatientMasterVisitId=request.PatientMasterVisitId,
                                ScreeningTypeId= cacxTypeId,
                                ScreeningValueId=request.cacxResult,
                                ScreeningDone=request.ScreeningDone,
                                ScreeningDate=DateTime.Now,
                                ScreeningCategoryId= request.cacxMethod,
                                Comment = request.Comments,
                                CreateDate = DateTime.Now,
                                CreatedBy = request.CreatedBy
                            };
                        PatientScreeningResult = await clientMonitoringService.AddPatientScreening(patientScreeningCaCxMethod);
                    }
                    

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
                  
                    await clientMonitoringService.AddPatientScreening(patientScreeningTb);

                    if (request.ClinicalNotes != "n/a")
                    {
                        PatientClinicalNotes patientClinicalNotes = new PatientClinicalNotes()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            ServiceAreaId = request.ServiceAreaId,
                            ClinicalNotes = request.ClinicalNotes,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.CreatedBy,
                            Active = false
                        };

                        int clinicalNotesId = await clientMonitoringService.AddPatientClinicalNotes(patientClinicalNotes);
                    }

                    return Result<ClientMonitoringCommandResponse>.Valid(new ClientMonitoringCommandResponse()
                    {
                        resultId = PatientScreeningResult
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
