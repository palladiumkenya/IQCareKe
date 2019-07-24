using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class AddPatientCareEndingCommand :IRequest<Result<AddPatientCareEndingResponse>>
    {

        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public int PatientMasterVisitId { get; set; }
        public DateTime CareEndedDate { get; set; }

        public string Specify { get; set; }

        public int DisclosureReason { get; set; }

        public DateTime? DeathDate { get; set; }

        public int UserId { get; set; }
    }

    public class AddPatientCareEndingResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }
    }
}
