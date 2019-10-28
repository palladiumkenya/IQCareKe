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
    public class GetPatientAdherenceOutcomeCommandHandler :IRequestHandler<GetPatientAdherenceOutcomeCommand,Result<List<AdherenceView>>>
    {

        IPrepUnitOfWork _prepUnitOfWork;
        public GetPatientAdherenceOutcomeCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async  Task<Result<List<AdherenceView>>> Handle(GetPatientAdherenceOutcomeCommand request,CancellationToken cancellationToken)

        {
            using (_prepUnitOfWork)
            {
                try
                {

                    var results = await _prepUnitOfWork.Repository<AdherenceView>().Get(x => x.PatientId == request.PatientId).
                        OrderByDescending(x => x.VisitDate).Take(2).ToListAsync();

                    return Result<List<AdherenceView>>.Valid(results);
                    

                    




                }
                catch (Exception ex)
                {
                    return Result<List<AdherenceView>>.Invalid(ex.Message);
                }
            }
        }

    }
}
