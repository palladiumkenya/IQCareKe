using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDrugAdministrationInfoQuery : IRequest<Result<List<PatientDrugAdministrationViewModel>>>
    {
        public int PatientId { get; set; }

    }

    public class PatientDrugAdministrationViewModel
    {
        public int Id { get;  set; }
        public int PatientId { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public string StrDrugAdministered { get; set; }
        public int DrugAdministered { get;  set; }
        public int Value { get;  set; }
        public string StrValue { get; set; }
        public string Description { get;  set; }
        public bool DeleteFlag { get;  set; }
        public int ? CreatedBy { get;  set; }
        public DateTime ? DateCreated { get;  set; }
    }
}
