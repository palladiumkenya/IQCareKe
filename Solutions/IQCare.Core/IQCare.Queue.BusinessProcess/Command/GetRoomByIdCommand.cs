using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.BusinessProcess.Command
{
   public class GetRoomByIdCommand:IRequest<Result<GetRoomByIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetRoomByIdResponse
    {
        public Rooms RoomsList;
    }
}
