using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class UpdatePatientFamilyPlanningMethodCommand : IRequest<Result<UpdatePatientFamilyPlanningMethodResponse>>
    {
        public int Id { get; set; }
        public int FPMethodId { get; set; }
        public int PatientId { get; set; }
        public int PatientFPId { get; set; }
        public int UserId { get; set; }
    }

    public class UpdatePatientFamilyPlanningMethodResponse
    {
        public string Message { get; set; }
    }
}