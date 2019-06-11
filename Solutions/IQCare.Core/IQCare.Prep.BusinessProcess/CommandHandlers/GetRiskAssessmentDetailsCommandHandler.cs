using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class GetRiskAssessmentDetailsCommandHandler : IRequestHandler<GetRiskAssessmentDetailsCommand, Result<GetRiskAssessmentResponse>>
    {

        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly ILogger logger = Log.ForContext<CheckRiskAssessmentEncounterCommandHandler>();
        public string message;
        public int pmvId;
        private List<RiskAssessmentDetails> rads = new List<RiskAssessmentDetails>();
        private List<Notes> pcn = new List<Notes>();
        private DateTime? visitDate;

        public GetRiskAssessmentDetailsCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async Task<Result<GetRiskAssessmentResponse>> Handle(GetRiskAssessmentDetailsCommand request, CancellationToken cancellationToken)
        {

            try
            {
              
                var RiskAssessmentList = await _prepUnitOfWork.Repository<RiskAssessment>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId && x.PatientId == request.PatientId && x.DeleteFlag == false).ToListAsync();
                if(RiskAssessmentList !=null && RiskAssessmentList.Count > 0)
                {
                    RiskAssessmentList.ForEach(x =>
                    {
                        RiskAssessmentDetails rd = new RiskAssessmentDetails();
                        rd.Id = x.Id;
                        rd.RiskAssessmentid = x.RiskAssessmentId;
                        rd.Value = x.RiskAssessmentValue;
                        rd.DeleteFlag = x.DeleteFlag;
                        rd.Comment = x.Comment;
                        rd.Date = x.AssessmentDate;
                        
                        rads.Add(rd);

                    });

                }

                var RiskComments = await _prepUnitOfWork.Repository<PatientClinicalNotes>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId && x.PatientId == request.PatientId && x.DeleteFlag == false).ToListAsync();

                if(RiskComments !=null && RiskComments.Count > 0)
                {
                    RiskComments.ForEach(x =>
                    {
                        Notes nt = new Notes
                        {
                            Comment = x.ClinicalNotes,
                            Id = x.Id,
                            DeleteFlag = x.DeleteFlag
                        };
                        pcn.Add(nt);

                    });

                }


                return Result<GetRiskAssessmentResponse>.Valid(new GetRiskAssessmentResponse
                {
                    VisitDate=visitDate,
                    RiskAssessmentDetails= rads,
                    ClinicalNotes = pcn
                });

            }
            catch (Exception ex)
            {


                {
                    string message = $"An error  has Occured" + ex.Message;


                    return await Task.FromResult(Result<GetRiskAssessmentResponse>.Invalid(message));
                }
            }
        }
    }
}
