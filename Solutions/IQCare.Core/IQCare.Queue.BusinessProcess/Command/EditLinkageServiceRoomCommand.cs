using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class EditLinkageServiceRoomCommand:IRequest<Result<EditLinkageServiceRoomResponse>>
    {
        public int Id { get; set; }
        public int ServicePointId { get; set; }


        public int RoomId { get; set; }



        public int ServiceAreaId { get; set; }



        public Boolean DeleteFlag { get; set; }

        public Boolean Active { get; set; }




        public int? UpdatedBy { get; set; }


    }

    public class EditLinkageServiceRoomResponse
    {
        public string Message { get; set; }
        public int Id { get; set; }

        public Boolean Updated { get; set; }
    }
   
}
