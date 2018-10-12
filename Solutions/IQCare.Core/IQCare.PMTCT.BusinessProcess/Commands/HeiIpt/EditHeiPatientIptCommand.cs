﻿using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIpt
{
    public class EditHeiPatientIptCommand: IRequest<Result<HeiPatientIpt>>
    {
        public HeiPatientIpt HeiPatientIpt { get; set; }
    }
}