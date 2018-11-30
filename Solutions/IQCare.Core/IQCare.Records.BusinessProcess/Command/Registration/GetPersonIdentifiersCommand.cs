using System.Collections.Generic;
using IQCare.Library;
using MediatR;
using PersonIdentifier = IQCare.Common.Core.Models.PersonIdentifier;

namespace IQCare.Records.BusinessProcess.Command.Registration
{
    public class GetPersonIdentifiersCommand : IRequest<Result<List<PersonIdentifier>>>
    {
        public int PersonId { get; set; }
    }
}