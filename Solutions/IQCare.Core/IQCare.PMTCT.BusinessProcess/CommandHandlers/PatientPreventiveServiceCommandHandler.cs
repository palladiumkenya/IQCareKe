using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using IQCare.PMTCT.Services.Interface;
using MediatR;
using Serilog;
using PatientAppointment = IQCare.PMTCT.Core.Models.PatientAppointment;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    
    public class PatientPreventiveServiceCommandHandler : IRequestHandler<PatientPreventiveServiceCommand, Library.Result<PatientPreventiveServiceResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public int Result = 0;

        public PatientPreventiveServiceCommandHandler(IPmtctUnitOfWork unitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _commonUnitOfWork = commonUnitOfWork;
        }

        public async Task<Library.Result<PatientPreventiveServiceResponse>> Handle(PatientPreventiveServiceCommand request, CancellationToken cancellationToken)
        {
            int resultTwo = 0;
            int resultOne = 0;
            int resultThree = 0;
            using (_unitOfWork)
            {
                try
                {
                    List<PreventiveService> preventiveService = new List<PreventiveService>();
                    PatientPreventiveService _service = new PatientPreventiveService(_unitOfWork);

                    PatientPartnerTesting partnerTesting = new PatientPartnerTesting()
                    {
                        PatientId = request.PreventiveService[0].PatientId,
                        PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                        PartnerTested = request.PartnerTestingVisit,
                        PartnerHivResult = request.FinalHIVResult,
                        DeleteFlag = 0,
                        CreatedBy = request.CreatedBy

                    };
                    Result = await _service.AddPatientParterTesting(partnerTesting);

                    List<PreventiveService> preventiveServices = new List<PreventiveService>();
                    
                    List<PreventiveService> preventiveServiceData = _unitOfWork.Repository<PreventiveService>()
                        .Get(x => x.PatientId == request.PreventiveService[0].PatientId && !x.DeleteFlag )
                        .ToList();

                    bool itemAntenatal =
                        preventiveServiceData.Exists(x => x.PreventiveServiceId == request.AntenatalExercise);
                    if (!itemAntenatal)
                    {
                        PreventiveService exercise = new PreventiveService()
                        {
                            PatientId = request.PreventiveService[0].PatientId,
                            PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                            PreventiveServiceId = request.AntenatalExercise,
                            PreventiveServiceDate = DateTime.Now,
                            Description = "Antenatal exercise",
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = false
                        };
                        preventiveServices.Add(exercise);
                    }

                    bool itemInsecticide =
                        preventiveServiceData.Exists(x => x.PreventiveServiceId == request.InsecticideTreatedNet);
                    if (!itemInsecticide)
                    {
                        PreventiveService insecticideNet = new PreventiveService()
                        {
                            PatientId = request.PreventiveService[0].PatientId,
                            PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                            PreventiveServiceId = request.InsecticideTreatedNet,
                            PreventiveServiceDate = request.InsecticideGivenDate,
                            Description = request.InsecticideGivenDate.HasValue? "Insecticide treated nets given": "Insecticide Treated Net Not given",
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = false
                        
                        };          

                        preventiveServices.Add(insecticideNet);
                    }


                    if (preventiveServices.Count > 0)
                        resultThree = await _service.AddPatientPreventiveService(preventiveServices);
                    resultThree = 1;

                  //  int resultTwo = await _service.AddPatientPreventiveService(request.PreventiveService);
                    
                   // int 

                    var appointmentStatusId = _commonUnitOfWork.Repository<LookupItem>()
                        .Get(x => x.Name == "Pending").SingleOrDefault()?.Id;

                    foreach (var data in request.PreventiveService)
                    {
                        
                       
                        bool itemExists =
                            preventiveServiceData.Exists(x => x.PreventiveServiceId == data.PreventiveServiceId && Equals(x.PreventiveServiceDate, data.PreventiveServiceDate));
                        if (!itemExists)
                        {
                            PreventiveService rawData= new PreventiveService()
                            {
                               
                                PatientId = data.PatientId,
                                 PatientMasterVisitId = data.PatientMasterVisitId,
                                 PreventiveServiceId =data.PreventiveServiceId,
                                 PreventiveServiceDate=data.PreventiveServiceDate,
                                Description =data.Description,
                                 NextSchedule=data.NextSchedule,            
                                CreatedBy =data.CreatedBy,
                                DeleteFlag =data.DeleteFlag
                            };
                            
                            preventiveService.Add(rawData);
                            
                            if (data.NextSchedule.HasValue)
                            {  
                                PatientAppointment appointment = new PatientAppointment()
                                {
                                    PatientId = data.PatientId,
                                    PatientMasterVisitId = data.PatientMasterVisitId,
                                    ServiceAreaId = 3,
                                    AppointmentDate = data.NextSchedule.Value,
                                    ReasonId = data.PreventiveServiceId,
                                    Description = "ANC Preventive Services Schedule",
                                    StatusId = Int32.Parse(appointmentStatusId.ToString()),
                                    DifferentiatedCareId = 0,
                                    CreatedBy = request.CreatedBy                            
                                };

                                await _service.AddPatientAppointment(appointment);
                                                   
                            }
                        }   
                    }

                    if (preventiveService.Count > 0)
                        resultTwo = await _service.AddPatientPreventiveService(preventiveService);
                    resultTwo = 1;

                    if (resultThree > 0 && resultTwo > 0)
                    {
                        Result = 1;
                    }

                    return Library.Result<PatientPreventiveServiceResponse>.Valid(new PatientPreventiveServiceResponse()
                    {
                        Id = Result
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientPreventiveServiceResponse>.Invalid(e.Message);
                }
            } 
        }
    }
}
