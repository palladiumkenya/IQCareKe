using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
namespace IQCare.Queue.BusinessProcess.Command
{
    public class LinkageServiceRoomCommand : IRequest<Result<LinkageServiceRoomResponse>>
    {
        public List<LinkageList> Linkagelist { get; set; }
    }
    public class LinkageList
    {
            public int ServiceAreaId { get; set; }
            public int Roomid { get; set; }

            public int ServicePointId { get; set; }

            public Boolean DeleteFlag { get; set; }
            public int UserId { get; set; }
        

    }

    public class LinkageServiceRoomResponse
    {
        public string Message { get; set; }
        
    }
}
