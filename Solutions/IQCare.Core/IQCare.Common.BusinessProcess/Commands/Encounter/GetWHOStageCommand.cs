
using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
   public class GetWHOStageCommand :IRequest<Result<PatientWHOStage>>
    {
        public int PatientId { get; set; }
        public int Id { get; set; }
    }
}
