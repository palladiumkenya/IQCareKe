using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using Serilog;
using IQCare.Library;
using MediatR;
using IQCare.Common.BusinessProcess.Services;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class VisitDetailsCommandHandler : IRequestHandler<VisitDetailsCommand, Library.Result<VisitDetailsCommandResult>>
    {
        private readonly Infrastructure.ICommonUnitOfWork _pmtctUnitOfWork;
        private readonly Common.Infrastructure.ICommonUnitOfWork _commonUnitOfWork;
        private int visitCount=0;

        public VisitDetailsCommandHandler(Infrastructure.ICommonUnitOfWork pmtctUnitOfWork, Common.Infrastructure.ICommonUnitOfWork commonUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork ?? throw new ArgumentNullException(nameof(pmtctUnitOfWork));
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Library.Result<VisitDetailsCommandResult>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {

                    // PatientMasterVisit
                    PatientMasterVisitService patientMasterVisitService = new PatientMasterVisitService(_commonUnitOfWork);
                    var patientMasterVisit = await patientMasterVisitService.Add(request.PatientId, 1,DateTime.Today, 0,request.VisitDate, request.VisitDate, 0,0,request.VisitType,0);

                    //PatientPregnancy
                    PatientPregnancy patientPregnancy = new PatientPregnancy()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = patientMasterVisit.Id,
                        Lmp = request.Lmp,
                        Edd = request.Edd,
                        Gravidae = request.Gravidae                 
                    };

                    // Get anc-encounter Id:
                    LookupLogic lookupLogic = new LookupLogic(_commonUnitOfWork);
                    int encounterTypeId = await lookupLogic.GetLookupIdbyName("anc-encounter");

                    PatientEncounterService patientEncounterService = new PatientEncounterService(_commonUnitOfWork);
                    PatientEncounter patientEncounter = new PatientEncounter()
                    {
                        PatientId=request.PatientId,
                        EncounterTypeId=encounterTypeId,
                        PatientMasterVisitId=patientMasterVisit.Id,
                        EncounterStartTime=DateTime.Today,
                        ServiceAreaId=request.ServiceAreaId,
                        CreateDate=DateTime.Today
                    };

                    var encounter = await patientEncounterService.Add(request.PatientId, encounterTypeId, patientMasterVisit.Id, DateTime.Today, DateTime.Today, request.ServiceAreaId);

                    VisitDetailsService visitDetailsService = new VisitDetailsService(_pmtctUnitOfWork);
                    var pregnancy = await visitDetailsService.AddPatientPregnancy(patientPregnancy);

                    //patientProfile
                    var visits =await visitDetailsService.GetPatientProfile(request.PatientId);
                    if (visits.Count() > 0)
                    {
                        visitCount += visits.Count();
                    }

                    PatientProfile patientProfile = new PatientProfile()
                    {
                        PatientId=request.PatientId,
                        PatientMasterVisitId=patientMasterVisit.Id,
                        AgeMenarche=request.AgeAtMenarche,
                        PregnancyId=pregnancy.Id,
                        VisitNumber=visitCount,
                        VisitType= request.VisitType
                    };

                    var profile = visitDetailsService.AddPatientProfile(patientProfile);


                    return Library.Result<VisitDetailsCommandResult>.Valid(new VisitDetailsCommandResult()
                    {
                        PatientMasterVisitId =  patientMasterVisit.Id,
                        PregancyId = pregnancy.Id,
                        ProfileId=profile.Id,
                        PatientEncounterId=encounter.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<VisitDetailsCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}