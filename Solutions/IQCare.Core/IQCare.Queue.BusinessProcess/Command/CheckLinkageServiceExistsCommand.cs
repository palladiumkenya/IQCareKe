using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Queue.Core.Models;
using MediatR;
namespace IQCare.Queue.BusinessProcess.Command
{
    public  class CheckLinkageServiceExistsCommand:IRequest<Result<CheckLinkageServiceExistsResponse>>
    {
        public int ServiceAreaId;
        public int RoomId;
        public int ServicePointId;

    }

    public class CheckLinkageServiceExistsResponse
    {
        public Boolean Exists;
        public List<ServiceRoom> ServiceRooms { get; set; }
    }
}
