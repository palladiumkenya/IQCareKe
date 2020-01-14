using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetRoomsAndServicePointCommand : IRequest<Result<GetRoomsAndServicePointResponse>>
    {
        public int? ServiceArea { get; set; }

        //public int? ServicePoint { get; set; }


    }


    public class GetRoomsAndServicePointResponse
    {
       public List<ServiceRoomView> ServiceRoomViews { get; set; }
    }

}