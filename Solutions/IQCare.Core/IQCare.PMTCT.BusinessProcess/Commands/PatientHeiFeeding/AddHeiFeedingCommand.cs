using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{

    public class AddHeiFeedingCommand: IRequest<Result<AddHeiFeedingCommandResponse>>
    {
        public int PatientMasterVisitId;
        public int PatientId;
        public int FeedingModeId;
        public int UserId { get; set; }
    }

    public class AddHeiFeedingCommandResponse
    {
        public int HeiFeedingId { get; set; }
    }
}
