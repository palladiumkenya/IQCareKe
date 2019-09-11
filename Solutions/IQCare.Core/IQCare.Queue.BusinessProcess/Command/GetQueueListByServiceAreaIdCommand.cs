using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetQueueListByServiceAreaIdCommand : IRequest<Result<GetQueueListByServiceAreaResponse>>
    {
        public int ServiceAreaId { get; set; }
    }

    public class GetQueueListByServiceAreaResponse
    {
        public List<WaitingListView> waitingListViews { get; set; }
    }
}
