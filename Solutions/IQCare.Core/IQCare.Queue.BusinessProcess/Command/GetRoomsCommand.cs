using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetRoomsCommand:IRequest<Result<GetRoomsResponse>>
    {

    }

    public class GetRoomsResponse
    {
        public List<Rooms> RoomsList;
    }
}
