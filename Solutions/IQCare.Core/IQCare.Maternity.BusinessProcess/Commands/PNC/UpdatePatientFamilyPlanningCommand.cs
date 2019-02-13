using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class UpdatePatientFamilyPlanningCommand : IRequest<Result<UpdatePatientFamilyPlanningResponse>>
    {
        public int Id { get; set; }
        public int FamilyPlanningStatusId { get; set; }
        public int ReasonNotOnFPId { get; set; }
    }

    public class UpdatePatientFamilyPlanningResponse
    {
        public string Message { get; set; }
    }
}