using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;



namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
   public class GetDecodeByCodeIdCommand : IRequest<Result<GetDecodeByCodeIdResponse>>
    {
        public int CodeId { get; set; }
    }


    public class GetDecodeByCodeIdResponse
    {
        public List<Decode> DecodeItems { get; set; }
    }
}
