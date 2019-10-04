using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;

namespace IQCare.Queue.BusinessProcess.Command
{
    public  class GetPrioritiesListCommand:IRequest<Result<GetPrioritiesListResponse>>
    {

    }

    public class GetPrioritiesListResponse
    {
        public List<LookupItemView> Priority { get; set; }
    }
}
