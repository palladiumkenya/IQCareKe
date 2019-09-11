using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.BusinessProcess.Command
{
   public  class GetQueueListCommand : IRequest<Result<GetQueueListResponse>>
    {
     
    }

    public class GetQueueListResponse
    {
        public List<WaitingListView> waitingListViews { get; set; }
    }
    
}
