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
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetPersonLinkageCommandHandler : IRequestHandler<GetPersonLinkageCommand, Result<List<PatientLinkage>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetPersonLinkageCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientLinkage>>> Handle(GetPersonLinkageCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientLinkage>().Get(x => x.PersonId == request.PersonId)
                        .ToListAsync();

                    return Result<List<PatientLinkage>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PatientLinkage>>.Invalid(e.Message);
                }
            }
        }
    }
}