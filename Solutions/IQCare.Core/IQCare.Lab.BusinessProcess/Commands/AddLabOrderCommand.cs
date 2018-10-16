using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class AddLabOrderCommand : IRequest<Result<AddLabOrderResponse>>
    {
        public int Ptn_Pk { get; set; }
        public int PatientId { get; set; }

    }

    public class AddLabOrderResponse
    {

    }
}