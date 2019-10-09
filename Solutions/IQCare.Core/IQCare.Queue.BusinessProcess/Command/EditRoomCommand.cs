using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
namespace IQCare.Queue.BusinessProcess.Command
{
   public  class EditRoomCommand:IRequest<Result<EditRoomsResponse>>
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public Boolean Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdatedBy { get; set; }
    }

    public  class EditRoomsResponse
    {
        public int RoomId { get; set; }
        public string Message { get; set; }
    }
}
