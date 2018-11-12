using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Common.Core.Models;
using IQCare.Library;

namespace IQCareRecords.Common.BusinessProcess.Command
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
        public int EducationLevelId { get; set; }
    }
}
