using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class AddPatientScreeningCommand : IRequest<Result<AddPatientScreeningResponse>>
    {
        public List<KeyValuePair<string, int>> ScreeningType { get; set; }
        public DateTime ScreeningDate { get; set; }
        public string Occupation { get; set; }
        public DateTime? BookingDate { get; set; }
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int UserId { get; set; }
    }

    public class AddPatientScreeningResponse
    {
        public bool IsScreeningDone { get; set; }
    }
}