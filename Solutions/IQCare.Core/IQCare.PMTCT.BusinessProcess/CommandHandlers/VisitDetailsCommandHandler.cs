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

        private readonly IPmtctUnitOfWork _unitOfWork;
        public int visitCount=0;
        public int VisitNumber = 0;
        public PatientPregnancy Pregnancy;
        public int PregnancyId { get; set; }

        public VisitDetailsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Library.Result<VisitDetailsCommandResult>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                int profileId = 0;

                try
                {
                    VisitDetailsService visitDetailsService = new VisitDetailsService(_unitOfWork);
                    PregnancyServices patientPregnancyServices =new PregnancyServices(_unitOfWork);

                    PatientPregnancy pregnancyData = patientPregnancyServices.GetActivePregnancy(request.PatientId);
                    if (pregnancyData != null)
                    {
                        this.PregnancyId = pregnancyData.Id;
                        VisitNumber = visitDetailsService.GetNumberOfVisit(request.PatientId, pregnancyData.Id);
                        // check if the details have changed
                        if (pregnancyData.Lmp != request.Lmp || pregnancyData.Parity != request.ParityOne ||
                            pregnancyData.Parity2 != request.ParityTwo)
                        {
                            // TODO: insert into a tracking table
                        }

                    }
                    else
                    {
                        PatientPregnancy patientPregnancy = new PatientPregnancy()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            Lmp = request.Lmp,
                            Edd = request.Edd,
                            Parity = request.ParityOne,
                            Parity2 = request.ParityTwo,
                            Gestation = request.Gestation,
                            Gravidae = request.Gravidae,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now
                        };
                        this.Pregnancy = await visitDetailsService.AddPatientPregnancy(patientPregnancy);
                        this.PregnancyId = this.Pregnancy.Id;
                    }
                       

                    PatientProfile patientProfile = new PatientProfile()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        AgeMenarche = request.AgeAtMenarche,
                        PregnancyId = this.PregnancyId,
                        VisitNumber = (this.VisitNumber+1),
                        VisitType = request.VisitType,
                        CreatedBy = (request.UserId < 1) ? 1 : request.UserId,
                        CreateDate = DateTime.Now,
                    };
                    var profile = visitDetailsService.AddPatientProfile(patientProfile);
                    profileId = profile.Id;


                    return Library.Result<VisitDetailsCommandResult>.Valid(new VisitDetailsCommandResult()
                    {
                        PregancyId = (this.VisitNumber>=1)?this.PregnancyId:this.Pregnancy.Id,
                        ProfileId=profileId
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