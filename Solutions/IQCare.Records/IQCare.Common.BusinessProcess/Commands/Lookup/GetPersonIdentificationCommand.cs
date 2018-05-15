using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using Entities.Records.Enrollment;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands
{
  public  class GetPersonIdentificationCommand:IRequest<Result<GetPersonIdentificationResponse>>
    { 

        public string CodeName { get; set; }
          
    }

    public class GetPersonIdentificationResponse
    {
        public List<Identifier> Identifers;
    }
}
