using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
namespace IQCare.Common.BusinessProcess.Commands.PersonVitals
{
   public  class GetPersonVitalsCommand : IRequest<Result<List<PersonVitalsView>>>
    {
        public int PersonId { get; set; }

    }

}
