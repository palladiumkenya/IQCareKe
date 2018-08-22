using IQCare.Common.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Refferal
{
    public class GetRefferalCommand:IRequest<Result<List<PatientRefferal>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}
