using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetHtsEncountersCommandHandler : IRequestHandler<GetHtsEncountersCommand, Result<List<HTSEncountersView>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public GetHtsEncountersCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<HTSEncountersView>>> Handle(GetHtsEncountersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork.Repository<HTSEncountersView>()
                    .Get(x => x.PatientId == request.PatientId).ToListAsync();

                _unitOfWork.Dispose();

                return Result<List<HTSEncountersView>>.Valid(results);
            }
            catch (Exception e)
            {
                return Result<List<HTSEncountersView>>.Invalid(e.Message);
            }
        }
    }
}