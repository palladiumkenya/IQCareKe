using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Partners
{
    public class AddPersonRelationshipCommand : IRequest<Result<AddPersonRelationshipResponse>>
    {
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public int RelationshipTypeId { get; set; }
        public int UserId { get; set; }
        public int? BaselineResult { get; set; }
        public DateTime? BaselineDate { get; set; }
    }

    public class AddPersonRelationshipResponse
    {
        public int PersonRelationshipId { get; set; }
    }
}