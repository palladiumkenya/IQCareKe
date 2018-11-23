using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class GetBaselineAntenatalCareCommand: IRequest<Result<BaselineAntenatalCare>>
    {
        private int PatientId { get; set; }
    }
}