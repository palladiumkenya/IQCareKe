using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPersonLocationCommand : IRequest<Result<AddPersonLocationResponse>>
    {
        public int PersonId { get; set; }
        public int CountyId { get; set; }
        public int SubCountyId { get; set; }
        public int WardId { get; set; }
        public string Village { get; set; }
        public string LandMark { get; set; }
        public int UserId { get; set; }
    }

    public class AddPersonLocationResponse
    {
        public int PersonLocationId { get; set; }
    }
}