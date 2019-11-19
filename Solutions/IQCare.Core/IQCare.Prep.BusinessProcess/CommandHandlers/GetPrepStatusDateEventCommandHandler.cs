using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
  public  class GetPrepStatusDateEventCommandHandler:IRequestHandler<GetPrepStatusDateEventCommand,Result<PatientPrEPEvents>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        DateTime? InitiationDate;
        public GetPrepStatusDateEventCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

       public async  Task<Result<PatientPrEPEvents>> Handle(GetPrepStatusDateEventCommand request,CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    var prepevents = await _prepUnitOfWork.Repository<PatientPrEPStatus>().Get(x =>
                     x.PatientId == request.PatientId  &&
                     x.DeleteFlag == false && x.PrepStatusToday == request.startitemId).OrderByDescending(x=>x.PatientEncounterId ).ToListAsync();
                    var prepInitiation = await _prepUnitOfWork.Repository<PatientARVHistory>().Get(x => x.PatientId == request.PatientId && x.Purpose == "PrEP" && x.DeleteFlag == false)
                        .OrderByDescending(x => x.PatientMasterVisitId).OrderByDescending(x => x.CreateDate).ToListAsync();
                   

                     if(prepInitiation.Count > 0)
                    {
                        InitiationDate = prepInitiation[0].InitiationDate;
                    }
                    PatientPrEPEvents pts = new PatientPrEPEvents();
                    if (prepevents.Count > 0)
                    {
                        pts.PatientEncounterId = prepevents[0].PatientEncounterId;
                        pts.PatientId = prepevents[0].PatientId;
                        pts.Id = prepevents[0].Id;
                        if (prepevents[0].DateField == null)
                        {
                            pts.DateStarted = InitiationDate;
                        }
                        else
                        {
                            pts.DateStarted = prepevents[0].DateField;
                        }
                    }
                  
                 

                    return Result<PatientPrEPEvents>.Valid(pts);
                    
                }
                catch(Exception ex)
                {
                    Log.Error($"An error occured while trying to get prep status for PatientId: {request.PatientId} , exception: {ex.Message} {ex.InnerException}");
                    return Result<PatientPrEPEvents>.Invalid($"An error occured while trying to get prep status for PatientId: {request.PatientId} ");

                }
            }

        }
    }
}
