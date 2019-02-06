using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
using WardLookup = IQCare.Common.Core.Models.WardLookup;

namespace IQCare.Records.BusinessProcess.Command.Lookup
{
    public class GetWardCommand:IRequest<Result<List<WardLookup>>>
    {
        public int SubcountyId;
    }
}
