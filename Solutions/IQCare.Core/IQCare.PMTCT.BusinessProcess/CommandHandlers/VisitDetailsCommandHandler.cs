using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
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
        public int VisitNumber = 0;
        public PatientPregnancy Pregnancy;
        public int PregnancyId { get; set; }

        public VisitDetailsCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Library.Result<VisitDetailsCommandResult>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_commonUnitOfWork)
            {
                int profileId = 0;

                try
                {
                    VisitNumber = request.VisitNumber;

                    
                    PatientMasterVisitService patientMasterVisitService = new PatientMasterVisitService(_commonUnitOfWork);
                    LookupLogic lookupLogic = new LookupLogic(_commonUnitOfWork);
                    VisitDetailsService visitDetailsService = new VisitDetailsService(_unitOfWork);
                    PatientEncounterService patientEncounterService = new PatientEncounterService(_commonUnitOfWork);
                    PregnancyServices patientPregnancyServices =new PregnancyServices(_unitOfWork);


                    var patientMasterVisit = await patientMasterVisitService.Add(request.PatientId, 1,DateTime.Today, 0,request.VisitDate, request.VisitDate, 0,0,request.VisitType,0);


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

                    
                    int encounterTypeId = await lookupLogic.GetLookupIdbyName("anc-encounter");                   
                    var encounter = await patientEncounterService.Add(request.PatientId, encounterTypeId, patientMasterVisit.Id, DateTime.Now, DateTime.Now, request.ServiceAreaId,request.UserId);

                    if (VisitNumber <= 1)
                    {
                         this.Pregnancy = await visitDetailsService.AddPatientPregnancy(patientPregnancy);
                         this.PregnancyId = 0;
                    }
                    else
                    {
                        PatientPregnancy pregnancyData =  patientPregnancyServices.GetActivePregnancy(request.PatientId);
                        this.PregnancyId = pregnancyData.Id;
                    }
                   
                    var visits = await visitDetailsService.GetPatientProfile(request.PatientId);


                    if (this.VisitNumber <=1)
                    {

                        this.VisitNumber += 1;
                    }


                        PatientProfile patientProfile = new PatientProfile()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = patientMasterVisit.Id,
                            AgeMenarche = request.AgeAtMenarche,
                            PregnancyId = this.PregnancyId,
                            VisitNumber = this.VisitNumber,
                            VisitType = request.VisitType,
                            CreatedBy = (request.UserId<1)?1:request.UserId,
                            CreateDate = DateTime.Now
                        };

                          var profile = visitDetailsService.AddPatientProfile(patientProfile);
                        profileId = profile.Id;
                    



                    return Library.Result<VisitDetailsCommandResult>.Valid(new VisitDetailsCommandResult()
                    {
                        PatientMasterVisitId =  patientMasterVisit.Id,
                        PregancyId = (this.VisitNumber>=1)?this.PregnancyId:this.Pregnancy.Id,
                        ProfileId=profileId,
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