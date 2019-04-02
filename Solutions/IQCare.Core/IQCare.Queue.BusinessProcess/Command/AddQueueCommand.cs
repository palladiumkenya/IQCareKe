using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;
namespace IQCare.Queue.BusinessProcess.Command
{
   public  class AddQueueCommand :IRequest<Result<AddQueueResponse>>
    {
        public int ServiceRoomId { get; set; }
        public int PatientId { get; set; }

        public int Priority { get; set; }

        public Boolean DeleteFlag { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

     
    }

    public class AddQueueResponse
    {
        public int PatientId { get; set; }
        public string Message { get; set; }
    }
}
