using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Services;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Services;
using Serilog;
using MediatR;
using IQCare.Common.Infrastructure;
using IQCare.PMTCT.Infrastructure;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class VisitDetailsCommandHandler : IRequestHandler<VisitDetailsCommand, Library.Result<VisitDetailsCommandResult>>
    {

        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        public int visitCount=0;

        public VisitDetailsCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Library.Result<VisitDetailsCommandResult>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_commonUnitOfWork)
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
                        Parity = request.ParityOne,
                        Parity2 = request.ParityTwo,
                        Gestation = request.Gestation,
                        Gravidae = request.Gravidae,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now                                          
                    };

                    // Get anc-encounter Id:
                    LookupLogic lookupLogic = new LookupLogic(_commonUnitOfWork);
                    int encounterTypeId = await lookupLogic.GetLookupIdbyName("anc-encounter");

                    PatientEncounterService patientEncounterService = new PatientEncounterService(_commonUnitOfWork);

                    var encounter = await patientEncounterService.Add(request.PatientId, encounterTypeId, patientMasterVisit.Id, DateTime.Now, DateTime.Now, request.ServiceAreaId,request.UserId);

                    VisitDetailsService visitDetailsService = new VisitDetailsService(_unitOfWork);
                    ////PatientProfileService patientProfileService= new PatientProfileService(_unitOfWork);
                    var pregnancy = await visitDetailsService.AddPatientPregnancy(patientPregnancy);

                    ////patientProfile
                    var visits = await visitDetailsService.GetPatientProfile(request.PatientId);
                    if (visits.Count() > 0)
                    {
                        
                        visitCount += visits.Count();
                    }
                    else
                    {
                       this.visitCount= 1+ visits.Count();

                        PatientProfile patientProfile = new PatientProfile()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = patientMasterVisit.Id,
                            AgeMenarche = request.AgeAtMenarche,
                            PregnancyId = pregnancy.Id,
                            VisitNumber = this.visitCount,
                            VisitType = request.VisitType,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now
                        };

                          var profile = visitDetailsService.AddPatientProfile(patientProfile);
                    }

                    return Library.Result<VisitDetailsCommandResult>.Valid(new VisitDetailsCommandResult()
                    {
                        PatientMasterVisitId =  patientMasterVisit.Id,
                        PregancyId =pregnancy.Id,
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