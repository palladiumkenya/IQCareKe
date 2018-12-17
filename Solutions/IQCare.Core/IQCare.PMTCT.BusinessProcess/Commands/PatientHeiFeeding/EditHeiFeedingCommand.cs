
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{
    public class EditHeiFeedingCommand:IRequest<Result<EditHeiFeedingCommandResult>>
    {
        public HeiFeedingUpdate heiFeeding;
    }

    public class HeiFeedingUpdate
    {
        public int Id { get; set; }
        public int FeedingModeId { get; set; }
    }

    public class EditHeiFeedingCommandResult
    {
         public int Id {get;set;}
    }
}
