using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{

    public class AddHeiFeedingCommand: IRequest<Result<AddHeiFeedingCommandResponse>>
    {
        public int PatientMasterVisitId;
        public int PatientId;
        public int FeedingModeId;

    }

    public class AddHeiFeedingCommandResponse
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int FeedingModeId { get; set; }
    }
}
