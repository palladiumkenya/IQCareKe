using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class PatientScreeningCommand : IRequest<Result<AddPatientScreeningResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ScreeningTypeId { get; set; }
        public Boolean ScreeningDone { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public int? ScreeningCategoryId { get; set; }
        public int? ScreeningValueId { get; set; }
        public string Comment { get; set; }
        public Boolean Active { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public DateTime? VisitDate { get; set; }
    }

    public class AddPatientScreeningResponse
    {
        public bool IsScreeningDone { get; set; }
    }
}