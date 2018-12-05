using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDiagnosisInfo : IRequest<Result<List<PatientDiagnosisViewModel>>>
    {
        public int ? PatientId { get; set; }

        public int ? PatientMasterVisitId  { get; set; }
    }

    public class PatientDiagnosisViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Diagnosis { get; set; }
        public string ManagementPlan { get; set; }
        public int CreatedBy { get; set; }
    }
}
