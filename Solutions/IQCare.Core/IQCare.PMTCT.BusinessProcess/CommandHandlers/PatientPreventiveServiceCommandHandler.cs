using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    
    public class PatientPreventiveServiceCommandHandler : IRequestHandler<PatientPreventiveServiceCommand, Result<PatientPreventiveServiceResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private readonly IPatientPreventiveService _service;
        public int Result = 0;

        public PatientPreventiveServiceCommandHandler(IPmtctUnitOfWork unitOfWork, IPatientPreventiveService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task<Result<PatientPreventiveServiceResponse>> Handle(PatientPreventiveServiceCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                PatientPartnerTesting partnerTesting= new PatientPartnerTesting()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PartnerTested = request.PartnerTestingVisit,
                    PartnerHivResult = request.FinalHIVResult,
                    DeleteFlag = 0
                    
                };
                Result = await _service.AddPatientParterTesting(partnerTesting);

                List<PreventiveService> preventiveServices=new List<PreventiveService>();

                PreventiveService insecticideNet= new PreventiveService()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PreventiveServiceId = request.InsecticideTreatedNet,
                    PreventiveServiceDate = request.InsecticideGivenDate,
                    Description = ""
                };

                PreventiveService exercise =new PreventiveService()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PreventiveServiceId = request.AntenatalExercise,
                    PreventiveServiceDate = DateTime.Today,
                    Description = ""
                };

                preventiveServices.Add(insecticideNet);
                preventiveServices.Add(exercise);

                int resultTwo = await _service.AddPatientPreventiveService(request.PreventiveService);
                int resultThree = await _service.AddPatientPreventiveService(preventiveServices);

                foreach (var data in request.PreventiveService)
                {
                    if (data.NextSchedule.Day>0)
                    {
                        PatientAppointment appointment=new PatientAppointment()
                        {
                            PatientId = data.PatientId,
                            PatientMasterVisitId = data.PatientMasterVisitId,
                            ServiceAreaId = 3,
                            AppointmentDate = data.NextSchedule,
                            ReasonId = 0,
                            Description = "ANC Preventive Services Schedule",
                            StatusId = 0,
                            DifferentiatedCareId = 0
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
