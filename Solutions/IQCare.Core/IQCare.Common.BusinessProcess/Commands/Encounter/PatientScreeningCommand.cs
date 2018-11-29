using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class PatientScreeningCommand : IRequest<Result<PatientScreeningResponse>>
    {
        public List<Screening> Screening { get; set; }
        public DateTime ScreeningDate { get; set; }
        public string Occupation { get; set; }
        public DateTime? BookingDate { get; set; }
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int UserId { get; set; }
    }

    public class Screening
    {
        public int ScreeningTypeId { get; set; }
        public int ScreeningCategoryId { get; set; }
        public int ScreeningValueId { get; set; }
    }

    public class PatientScreeningResponse
    {
        public bool ScreeningDone { get; set; }
    }
}