using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class ReferPatientCommand : IRequest<Result<ReferPatientResponse>>
    {
        public string ReferredTo { get; set; }
        public DateTime DateToBeEnrolled { get; set; }
    }

    public class ReferPatientResponse
    {
       
    }
}
