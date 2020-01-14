using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class MergeDuplicateRecordsCommand : IRequest<Result<MergeDuplicateRecordsResponse>>
    {
        public int preferredPersonId { get; set; }
        public int unPreferredPersonId { get; set; }
        public int userid { get; set; }
    }

    public class MergeDuplicateRecordsResponse
    {
        public string Message { get; set; }
    }
}
