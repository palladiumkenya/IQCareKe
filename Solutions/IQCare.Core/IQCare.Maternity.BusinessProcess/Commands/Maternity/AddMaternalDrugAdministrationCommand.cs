using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddPatientDrugAdministrationCommand : IRequest<Result<AddPatientDrugAdministrationResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CreatedBy { get; set; }
        public List<AdministeredDrugInfo> AdministeredDrugs { get; set; }

    }

    public class AdministeredDrugInfo
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class AddPatientDrugAdministrationResponse
    {
        public int PatientMasterVisitId { get; set; }

    }

    public class UpdateDrugAdministrationCommand : IRequest<Result<object>>
    {
        public int Id { get; set; }
        public int DrugAdministered { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

    }

    public class DeactivateDrugAdministrationCommand : IRequest<Result<object>>
    {
        public int Id { get; set; }

    }

}
