﻿using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class AddBaselineAntenatalCareCommand: IRequest<Result<BaselineAntenatalCare>>
    {
        public BaselineAntenatalCare BaselineAntenatalCare { get; set; }
    }
}