using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;

namespace IQCare.Queue.BusinessProcess.Command
{
    public  class CheckQueueExistCommand : IRequest<Result<CheckQueueResponse>>
    {
        public int ServiceRoomId { get; set; }
        public int PatientId { get; set; }

    }

    public class CheckQueueResponse
    {
        public string Message { get; set; }
        public Boolean Exists { get; set; }
    }
}
