using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetHivPositiveListCommandHandler : IRequestHandler<GetHivPositiveListCommand, Result<List<HivPositiveListView>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetHivPositiveListCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<HivPositiveListView>>> Handle(GetHivPositiveListCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<HivPositiveListView>()
                        .Get(x => x.PersonId == request.PersonId).ToListAsync();

                    return Result<List<HivPositiveListView>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error($"Error fetching hiv positive persons list {e.Message} {e.InnerException}");
                    return Result<List<HivPositiveListView>>.Invalid($"Error fetching hiv positive persons list");
                }
            }
        }
    }
}