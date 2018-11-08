using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Relationship
{
    public class GetPartnersCommand : IRequest<Result<List<PartnersView>>>
    {
        public int PatientId { get; set; }
        public string[] RelationshipTypes { get; set; }
    }
}