using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands
{ 
   public class PersonEducationLevelCommand:IRequest<Result<AddPersonEducationalLevelResponse>>
    {

        public int PersonId { get; set; }
        public int EducationalLevel { get; set; }
        public int UserId { get; set; }
    }

    public class AddPersonEducationalLevelResponse
    {
       

        public string Message { get; set; }
    }
}
