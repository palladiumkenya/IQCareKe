using IQCare.Queue.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class CheckRoomExistCommand : IRequest<Result<RoomExistResponse>>
    {
        public string RoomName { get; set; }

    }

    public class RoomExistResponse

    {
        public string Message { get; set; }
        public List<Rooms> Rooms { get; set; }

        public Boolean Exists { get; set; }
    }

}