using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class AddPatientScreeningsCommand : IRequest<Result<PatientScreeningsResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public List<Screenings> Screenings { get; set; }
    }

    public class Screenings
    {
        public int ScreeningTypeId { get; set; }
        public int ScreeningCategoryId { get; set; }
        public int ScreeningValueId { get; set; }

      
        public string Comment { get; set; }
    }
    public class PatientScreeningsResponse
    {
        public string Message { get; set; }
    }
}