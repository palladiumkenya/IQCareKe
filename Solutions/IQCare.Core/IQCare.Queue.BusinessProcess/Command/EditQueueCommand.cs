using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class EditQueueCommand :IRequest<Result<EditQueueResponse>>
    {
        public int Id { get; set; }


        public Boolean DeleteFlag { get; set; }

        public Boolean Status { get; set; }

        public int UpdatedBy { get; set; }
    }

    public class EditQueueResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public bool Updated { get; set; }
    }
}
