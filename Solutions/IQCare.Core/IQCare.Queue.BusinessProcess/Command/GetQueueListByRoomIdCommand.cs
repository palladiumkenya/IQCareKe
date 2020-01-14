using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;


namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetQueueListByRoomIdCommand : IRequest<Result<GetQueueListByRoomsResponse>>
    {
        public int RoomId { get; set; }
    }

    public class GetQueueListByRoomsResponse
    {
        public List<WaitingListView> waitingListViews { get; set; }
    }
}
