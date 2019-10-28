using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class GetPrepStatusDateEventCommand : IRequest<Result<PatientPrEPEvents>>
    {
        public int PatientId { get; set; }
       

        public int startitemId { get; set; }


    
    }
}
