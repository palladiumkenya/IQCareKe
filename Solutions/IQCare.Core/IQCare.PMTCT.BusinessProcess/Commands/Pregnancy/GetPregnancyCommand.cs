using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
    public class GetPregnancyCommand: IRequest<Result<PatientPregnancy>>
    {
        public int PatientId { get; set; }
    }

}
