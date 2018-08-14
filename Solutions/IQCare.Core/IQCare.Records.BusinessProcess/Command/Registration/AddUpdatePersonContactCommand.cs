using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class AddUpdatePersonContactCommand: IRequest<Result<AddUpdatePersonContactResponse>>
    {
        public int PersonId { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string EmailAddress { get; set; }
        public int UserId { get; set; }
    }

    public class AddUpdatePersonContactResponse
    {
        public string Message { get; set; }
        public int PersonContactId { get; set; } 
    }
}

