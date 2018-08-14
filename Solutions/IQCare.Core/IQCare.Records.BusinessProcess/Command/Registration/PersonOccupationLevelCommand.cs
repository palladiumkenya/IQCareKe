using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.Command
{
   public class PersonOccupationLevelCommand:IRequest<Result<AddPersonOccupationLevelResponse>>
    {
        public int  PersonId { get; set; }
        public int Occupation { get; set; }
        public int UserId { get; set; }
    }

   public class AddPersonOccupationLevelResponse
    {
        public string Message { get; set; }
        public int OccupationId { get; set; }
    }
}
