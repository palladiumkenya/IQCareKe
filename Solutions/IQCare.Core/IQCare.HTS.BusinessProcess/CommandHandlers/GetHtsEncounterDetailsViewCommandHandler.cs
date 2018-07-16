using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetHtsEncounterDetailsViewCommandHandler : IRequestHandler<GetHtsEncounterDetailsViewCommand, Result<List<EncountersDetailView>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public GetHtsEncounterDetailsViewCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<EncountersDetailView>>> Handle(GetHtsEncounterDetailsViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<EncountersDetailView>()
                        .Get(x => x.EncounterId == request.EncounterId).ToListAsync();

                    return Result<List<EncountersDetailView>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Result<List<EncountersDetailView>>.Invalid(e.Message);
                }
            }
        }
    }
}