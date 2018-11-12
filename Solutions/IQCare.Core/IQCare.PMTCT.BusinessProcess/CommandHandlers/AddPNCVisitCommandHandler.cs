using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class AddPNCVisitCommandHandler : IRequestHandler<AddPNCVisitCommand, Result<AddPNCVisitResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public AddPNCVisitCommandHandler(IPmtctUnitOfWork unitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _commonUnitOfWork = commonUnitOfWork;
        }
        public async Task<Result<AddPNCVisitResponse>> Handle(AddPNCVisitCommand request, CancellationToken cancellationToken)
        {

            try
            {
                //PatientMasterVisitService patientMasterVisitService = new PatientMasterVisitService(_commonUnitOfWork);
                //PatientEncounterService patientEncounterService = new PatientEncounterService(_commonUnitOfWork);
                //LookupLogic lookupLogic = new LookupLogic(_commonUnitOfWork);
                VisitDetailsService visitDetailsService = new VisitDetailsService(_unitOfWork);

                //var patientMasterVisit = await patientMasterVisitService.Add(request.PatientId, 1, DateTime.Today, 0, request.VisitDate, request.VisitDate, 0, 0, request.VisitType, 0);

                //var encounterTypeId = await lookupLogic.GetLookupIdbyName(request.EncounterType);

                //var encounter = await patientEncounterService.Add(request.PatientId, encounterTypeId, patientMasterVisit.Id, DateTime.Now, DateTime.Now, request.ServiceAreaId, request.UserId);

                PatientProfile patientProfile = new PatientProfile()
                {
                    PatientId = request.PatientId,
                    PatientMasterVisitId = request.PatientMasterVisitId,
                    VisitType = request.VisitType,
                    CreatedBy = (request.UserId < 1) ? 1 : request.UserId,
                    CreateDate = DateTime.Now,
                    DaysPostPartum = request.DaysPostPartum,
                    VisitNumber = request.VisitNumber,
                    AgeMenarche = request.AgeMenarche
                    
                };

                var profile = await visitDetailsService.AddPatientProfile(patientProfile);

                return Result<AddPNCVisitResponse>.Valid(new AddPNCVisitResponse { ProfileId = profile.Id });
            }
            catch (Exception ex)
            {
                Log.Error("An error occured while adding PNC patient visit details",ex);
                return Result<AddPNCVisitResponse>.Invalid(ex.Message);
            }

        }
    }
}
