using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetClientReferralCommandHandler : IRequestHandler<GetClientReferralCommand, Result<List<Referral>>>
    {
        private readonly IHTSUnitOfWork _hTSUnitOfWork;
        public GetClientReferralCommandHandler(IHTSUnitOfWork hTSUnitOfWork)
        {
            _hTSUnitOfWork = hTSUnitOfWork ?? throw new ArgumentNullException(nameof(hTSUnitOfWork));
        }

        public async Task<Result<List<Referral>>> Handle(GetClientReferralCommand request, CancellationToken cancellationToken)
        {
            using (_hTSUnitOfWork)
            {
                try
                {
                    var clientReferral = await _hTSUnitOfWork.Repository<Referral>().Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).OrderByDescending(y=>y.Id).ToListAsync();
                    return Result<List<Referral>>.Valid(clientReferral);
                }
                catch (Exception e)
                {
                    return Result<List<Referral>>.Invalid(e.Message);
                }
            }
        }
    }
}