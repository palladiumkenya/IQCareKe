using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class DeleteBaselineAntenatalCareCommand: IRequest<Result<DeleteBaselineAntenatalCareResponse>>
    {
        public int Id { get; set; }
    }

    public class DeleteBaselineAntenatalCareResponse
    {
        public int Id { get; set; }
    }
}