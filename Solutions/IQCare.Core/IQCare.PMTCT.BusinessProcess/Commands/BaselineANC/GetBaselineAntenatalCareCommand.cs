using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class GetBaselineAntenatalCareCommand: IRequest<Result<List<BaselineAntenatalCare>>>
    {
        public int PatientId { get; set; }
    }
}