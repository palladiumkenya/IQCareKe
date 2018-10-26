using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
     public class GetMaternetyAndPncProfileCommand : IRequest<Result<PatientProfile>>
    {
        public int PatientId { get; set; }
    }
}
