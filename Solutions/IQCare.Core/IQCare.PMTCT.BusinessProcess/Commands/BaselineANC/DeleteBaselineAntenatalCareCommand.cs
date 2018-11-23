using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class DeleteBaselineAntenatalCareCommand: IRequest<Result<BaselineAntenatalCare>>
    {
        public int Id { get; set; }
    }
}