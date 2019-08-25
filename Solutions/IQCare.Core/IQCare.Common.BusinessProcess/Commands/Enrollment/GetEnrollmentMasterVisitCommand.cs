using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class GetEnrollmentMasterVisitCommand : IRequest<Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>>
    {
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
    }
}
