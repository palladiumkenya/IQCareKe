using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class GetConsentTypeCommand:IRequest<Result<GetConsentTypeResponse>>
    {
        public string ItemName;


    }

    public class  GetConsentTypeResponse
    {
        public int ConsentType { get; set; }
    }
}
