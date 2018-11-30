using System.Collections.Generic;
using IQCare.Library;
using MediatR;
using PersonKinContactsView = IQCare.Common.Core.Models.PersonKinContactsView;

namespace IQCare.Records.BusinessProcess.Command.Registration
{
    public class GetPersonKinContactsCommand : IRequest<Result<List<PersonKinContactsView>>>
    {
        public int PersonId { get; set; }
    }
}