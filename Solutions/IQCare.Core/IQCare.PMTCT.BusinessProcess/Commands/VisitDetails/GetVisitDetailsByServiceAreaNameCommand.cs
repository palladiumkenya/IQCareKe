using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.Views;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class GetVisitDetailsByServiceAreaNameCommand : IRequest<Result<List<VisitDetailsView>>>
    {
        public int PatientId { get; set; }
        public string ServiceAreaName { get; set; }
    }
}