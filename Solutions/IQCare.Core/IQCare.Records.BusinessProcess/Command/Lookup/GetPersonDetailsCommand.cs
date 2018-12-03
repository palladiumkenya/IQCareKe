using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;


namespace IQCare.Records.BusinessProcess.Command
{
    public class GetPersonDetailsCommand :IRequest<Result<List<PersonDetailsView>>>
    {
        public int PersonId { get; set; }
    }
}
