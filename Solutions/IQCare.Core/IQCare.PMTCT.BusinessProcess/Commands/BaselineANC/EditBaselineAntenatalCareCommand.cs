using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class EditBaselineAntenatalCareCommand: IRequest<Result<BaselineAntenatalCare>>
    {
        public BaselineAntenatalCare BaselineAntenatalCare { get; set; }
    }
}