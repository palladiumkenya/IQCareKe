using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using IQCare.PMTCT.Services.Interface;
using MediatR;
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
                    Description = "Insecticide treated nets given",
                    CreatedBy = request.CreatedBy
                };

                PreventiveService exercise =new PreventiveService()
                {
                    PatientId = request.PreventiveService[0].PatientId,
                    PatientMasterVisitId = request.PreventiveService[0].PatientMasterVisitId,
                    PreventiveServiceId = request.AntenatalExercise,
                    PreventiveServiceDate = DateTime.Now,
                    Description = "Antenatal exercise",
                    CreatedBy = request.CreatedBy
                };

                preventiveServices.Add(insecticideNet);
                preventiveServices.Add(exercise);

                int resultTwo = await _service.AddPatientPreventiveService(request.PreventiveService);
                int resultThree = await _service.AddPatientPreventiveService(preventiveServices);

                var appointmentStatusId = _commonUnitOfWork.Repository<LookupItem>()
                    .Get(x => x.Name == "Pending").SingleOrDefault()?.Id;

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
                            ReasonId = data.PreventiveServiceId,
                            Description = "ANC Preventive Services Schedule",
                            StatusId = Int32.Parse(appointmentStatusId.ToString()) ,
                            DifferentiatedCareId = 0,
                            CreatedBy = request.CreatedBy
                            
                        };

                        await _service.AddPatientAppointment(appointment);
                    }

                }

                if (resultThree >0 && resultTwo > 0)
                {
                    Result = 1;
                }

                return Library.Result<PatientPreventiveServiceResponse>.Valid(new PatientPreventiveServiceResponse()
                {
                    Id = Result
                });
            } 
        }
    }
}
