using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddPatientDiagnosisCommand : IRequest<Result<AddPatientDiagnosisResponse>>
    {
        public int ? Id { get;  set; }
        public int PatientId { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public string Diagnosis { get;  set; }
        public string ManagementPlan { get;  set; }
        public int CreatedBy { get;  set; }
    }

    public class AddPatientDiagnosisResponse
    {
        public int DiagnosisId { get; set; }

    }
}
