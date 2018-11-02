using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    class AddPersonRelationCommand : IRequest<Result<AddPersonRelationResponse>>
    {
        public int PersonId { get; set; }
        public int RelatedToPersonId { get; set; }
        public int RelationshipTypeId { get; set; }
        public int UserId { get; set; }

       
    }


    public class AddPersonRelationResponse
    {
        public int PersonRelationshipId { get; set; }
    }
}
