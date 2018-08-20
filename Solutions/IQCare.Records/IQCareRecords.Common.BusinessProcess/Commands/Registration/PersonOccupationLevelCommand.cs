using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using MediatR;

namespace IQCareRecords.Common.BusinessProcess.Commands
{
    public class PersonOccupationLevelCommand:IRequest<Result<AddPersonOccupationLevelResponse>>
    {

        public int PersonId { get; set; }
        public int Occupation { get; set; }


        public int UserId { get; set; }


      

    }


    public class AddPersonOccupationLevelResponse
    {
        public string Message { get; set; }
    }

}
