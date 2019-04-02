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
        public ServiceRoom ServiceRoom { get; set; }
        
    }

    public class EditLinkageServiceRoomResponse
    {
        public string Message { get; set; }
        public int Id { get; set; }
    }
}
