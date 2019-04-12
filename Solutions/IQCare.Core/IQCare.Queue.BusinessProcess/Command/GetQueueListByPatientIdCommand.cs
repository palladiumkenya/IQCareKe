using IQCare.Queue.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.Queue.BusinessProcess.Command
{
    public class GetQueueListByPatientIdCommand:IRequest<Result<GetQueueListByPatientidResponse>>
    {
        public int PatientId { get; set; }
    }

    public class GetQueueListByPatientidResponse
    {
        public List<WaitingListView> waitingListViews { get; set; }
    }
    
        
}
