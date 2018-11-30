using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Refferal
{
    public class EditRefferalCommand:IRequest<Result<EditRefferalCommandResponse>>
    {
        public PatientRefferal PatientRefferal;
    }

    public class EditRefferalCommandResponse
    {
        public int Id { get; set; }
    }
}
