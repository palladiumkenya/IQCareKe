using IQCare.Queue.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetLinkedRoomsCommand:IRequest<Result<GetLinkedRoomsResponse>>
    {
    }

    public class GetLinkedRoomsResponse
    {
        public List<ServiceRoomView> ServiceRoomViews { get; set; }
        public List<RoomServiceList> RoomServices { get; set; }
    }
}
