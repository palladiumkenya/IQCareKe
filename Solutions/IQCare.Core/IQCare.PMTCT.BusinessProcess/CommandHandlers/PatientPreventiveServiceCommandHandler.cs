using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using IQCare.PMTCT.Services.Interface;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    
    public class PatientPreventiveServiceCommandHandler : IRequestHandler<PatientPreventiveServiceCommand, Result<PatientPreventiveServiceResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        public int Result = 0;

        public PatientPreventiveServiceCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientPreventiveServiceResponse>> Handle(PatientPreventiveServiceCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                PatientPreventiveService _service=new PatientPreventiveService(_unitOfWork);
                PatientPartnerTesting partnerTesting= new PatientPartnerTesting()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PartnerTested = request.PartnerTestingVisit,
                    PartnerHivResult = request.FinalHIVResult,
                    DeleteFlag = 0,
                    CreatedBy = request.CreatedBy
                    
                };
                Result = await _service.AddPatientParterTesting(partnerTesting);

                List<PreventiveService> preventiveServices=new List<PreventiveService>();

                PreventiveService insecticideNet= new PreventiveService()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PreventiveServiceId = request.InsecticideTreatedNet,
                    PreventiveServiceDate = request.InsecticideGivenDate,
                    Description = "",
                    CreatedBy = request.CreatedBy
                };

                PreventiveService exercise =new PreventiveService()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PreventiveServiceId = request.AntenatalExercise,
                    PreventiveServiceDate = DateTime.Now,
                    Description = "",
                    CreatedBy = request.CreatedBy
                };

                preventiveServices.Add(insecticideNet);
                preventiveServices.Add(exercise);

                int resultTwo = await _service.AddPatientPreventiveService(request.PreventiveService);
                int resultThree = await _service.AddPatientPreventiveService(preventiveServices);

                foreach (var data in request.PreventiveService)
                {
                    if (data.NextSchedule.HasValue)
                    {
                        PatientAppointment appointment=new PatientAppointment()
                        {
                            PatientId = data.PatientId,
                            PatientMasterVisitId = data.PatientMasterVisitId,
                            ServiceAreaId = 3,
                            AppointmentDate = data.NextSchedule.Value,
                            ReasonId = 0,
                            Description = "ANC Preventive Services Schedule",
                            StatusId = 0,
                            DifferentiatedCareId = 0,
                            CreatedBy = request.CreatedBy
                            
                        };
                    }
                }

                if (resultThree >0 && resultTwo > 0)
                {
                    Result = 1;
                }

                return Result<PatientPreventiveServiceResponse>.Valid(new PatientPreventiveServiceResponse()
                {
                    Id = Result
                });
            } 
        }
    }
}
