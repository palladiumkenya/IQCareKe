using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class GetPersonIdentificationCommand : IRequest<Result<GetPersonIdentificationResponse>>
    {

        public string CodeName { get; set; }

    }

    public class GetPersonIdentificationResponse
    {
        public List<Identifier> Identifers { get; set; }
    }
}

