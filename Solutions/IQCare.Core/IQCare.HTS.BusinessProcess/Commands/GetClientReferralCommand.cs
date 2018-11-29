using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetClientReferralCommand : IRequest<Result<List<Referral>>>
    {
        public int PersonId { get; set; }
    }
}