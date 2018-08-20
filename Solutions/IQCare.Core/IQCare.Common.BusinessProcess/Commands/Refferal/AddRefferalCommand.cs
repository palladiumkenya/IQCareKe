using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Refferal
{
    public class AddRefferalCommand:IRequest<Result<AddRefferalCommandResponse>>
    {
        public PatientRefferal patientRefferal;
    }

    public class AddRefferalCommandResponse
    {
        public int Id { get; set; }
    }
}
