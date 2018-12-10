using System.Collections.Generic;
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class GetVisitDetailsCommand: IRequest<Result<List<Core.Models.VisitDetails>>>
    {
        public int PatientId { get; set; }
    }
}