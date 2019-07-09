using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class GetMonthlyRefillDetailsCommandHandler : IRequestHandler<GetMonthlyRefillDetailsCommand, Result<RefillResponse>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;



        public GetMonthlyRefillDetailsCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }
        public async Task<Result<RefillResponse>> Handle(GetMonthlyRefillDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<MonthlyRefillDetailsResponse> itemdetails = new List<MonthlyRefillDetailsResponse>();
                var clinicalnotes = await _prepUnitOfWork.Repository<PatientClinicalNotes>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId && x.ServiceAreaId == request.ServiceAreaId && x.DeleteFlag ==false).FirstOrDefaultAsync();
                var adherence = await _prepUnitOfWork.Repository<AdherenceOutcome>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId && x.DeleteFlag ==false)
                    .OrderByDescending(x => x.Id).FirstOrDefaultAsync();    
                if(adherence !=null)
                {
                    MonthlyRefillDetailsResponse mrd = new MonthlyRefillDetailsResponse();
                    mrd.PatientId = request.PatientId;
                    mrd.PatientMasterVisitId = request.PatientMasterVisitId;
                    mrd.ItemId = adherence.Score;
                    mrd.MasterId = adherence.AdherenceType;
                    mrd.Comment = adherence.ScoreDescription;

                    itemdetails.Add(mrd);
                }


                var patientscreeninglist = await _prepUnitOfWork.Repository<PatientScreening>().Get(x => x.PatientId==request.PatientId && x.PatientMasterVisitId ==request.PatientMasterVisitId && x.DeleteFlag == false).ToListAsync();
               
                if(patientscreeninglist.Count >0)
                {
                    patientscreeninglist.ForEach(x =>
                    {
                        MonthlyRefillDetailsResponse mrd = new MonthlyRefillDetailsResponse();
                        mrd.PatientId = request.PatientId;
                        mrd.PatientMasterVisitId = request.PatientMasterVisitId;
                        mrd.ItemId = x.ScreeningValueId;
                        mrd.MasterId = x.ScreeningTypeId;
                        mrd.Comment = x.Comment;

                        itemdetails.Add(mrd);


                    });
                }
                return Result<RefillResponse>.Valid( new RefillResponse
                { refilldetails= itemdetails,
                  clinicalnote = clinicalnotes
                });

            }
            catch(Exception ex)
            {
                return Result<RefillResponse>.Invalid(ex.Message);
            }

        }

    }
}
