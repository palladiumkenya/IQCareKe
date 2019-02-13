using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class UpdatePatientPartnerTestingCommand : IRequest<Result<UpdatePatientPartnerTestingResponse>>
    {
        public int Id { get; set; }
        public int PartnerTested { get; set; }
        public int PartnerHIVResult { get; set; }
    }

    public class UpdatePatientPartnerTestingResponse
    {
        public string Message { get; set; }
    }
}