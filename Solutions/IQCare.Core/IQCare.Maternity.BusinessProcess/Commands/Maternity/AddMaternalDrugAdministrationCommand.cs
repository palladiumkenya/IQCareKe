using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddMaternalDrugAdministrationCommand : IRequest<Result<AddMaternalDrugAdministrationResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CreatedBy { get; set; }
        public List<AdministredDrugInfo> AdministredDrugs { get; set; }
    }

    public class AdministredDrugInfo
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class AddMaternalDrugAdministrationResponse
    {
        public int PatientMasterVisitId { get; set; }

    }

}
