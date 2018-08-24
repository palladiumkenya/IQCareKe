using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.DrugAdministration
{
    public class EditDrugAdministrationCommand: IRequest<Result<EditDrugAdministrationResponse>>
    {
        public PatientDrugAdministration patientDrugAdministration;
    }

    public class EditDrugAdministrationResponse
    {
        public int Id { get; set; }
    }
}
