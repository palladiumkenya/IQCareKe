using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.PNC
{
    public class PatientScreeningDetailsQuery : IRequest<Result<List<PatientScreeningDetailsViewModel>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
    public class PatientScreeningDetailsViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int? ScreeningTypeId { get; set; }
        public int? ScreeningCategoryId { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public Boolean ScreeningDone { get; set; }
        public int? ScreeningValueId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public Boolean DeleteFlag { get; set; }
        public string screeningDisplayName { get; set; }
        public string ScreeningTypeName { get; set; }
    }
}
