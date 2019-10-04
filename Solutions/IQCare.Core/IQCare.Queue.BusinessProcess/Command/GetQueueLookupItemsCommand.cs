using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.BusinessProcess.Command
{
   public  class GetQueueLookupItemsCommand:IRequest<Result<GetQueueLookupItemsResponse>>
    {

    }
    public class GetQueueLookupItemsResponse
    {
        public List<LookupItemView> Priority { get; set; }
        public List<Rooms> rooms { get; set; }
        public List<LookupItemView> ServicePoint { get; set; }

        public List<ServiceArea> serviceAreas { get; set; }

       
    }
}
