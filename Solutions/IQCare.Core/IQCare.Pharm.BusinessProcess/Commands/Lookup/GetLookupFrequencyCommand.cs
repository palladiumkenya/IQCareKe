using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.Infrastructure;
using IQCare.Library;
using MediatR;


namespace IQCare.Pharm.BusinessProcess.Commands.Lookup
{
   public  class GetLookupFrequencyCommand : IRequest<Result<GetLookupFrequencyResponse>>
    {
    }

    public class GetLookupFrequencyResponse
    {
        public List<Frequency> FrequencyItems { get; set; }
    }
}
