using System;
using System.Collections.Generic;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class UpdatePatientScreeningsCommand : IRequest<Result<UpdatePatientScreeningsResult>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public List<Screenings> Screenings { get; set; }
    }

    public class UpdatePatientScreeningsResult
    {
        public string Message { get; set; }
    }
}