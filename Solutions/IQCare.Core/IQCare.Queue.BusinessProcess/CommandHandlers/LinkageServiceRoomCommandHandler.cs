using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Queue.BusinessProcess.Command;
using IQCare.Queue.Core.Models;
using IQCare.Queue.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Queue.BusinessProcess.CommandHandlers
{
    public class LinkageServiceRoomCommandHandler : IRequestHandler<LinkageServiceRoomCommand, Result<LinkageServiceRoomResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        public LinkageServiceRoomCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<LinkageServiceRoomResponse>> Handle(LinkageServiceRoomCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    if (request.Linkagelist != null)
                    {
                        if (request.Linkagelist.Count > 0)
                        {
                            foreach (var link in request.Linkagelist)
                            {
                                var srvroom = await _queueUnitOfWork.Repository<ServiceRoom>().Get(x => x.ServiceAreaid == link.ServiceAreaId && x.ServicePointId == link.ServicePointId && x.RoomId == link.Roomid && x.DeleteFlag ==false).FirstOrDefaultAsync();

                                if(srvroom !=null)
                                {
                                    srvroom.RoomId = link.Roomid;
                                    srvroom.ServiceAreaid = link.ServiceAreaId;
                                    srvroom.ServicePointId = link.ServicePointId;
                                    srvroom.DeleteFlag = true;
                                    srvroom.UpdateDate = DateTime.Now;
                                    srvroom.UpdatedBy = link.UserId;
                                    _queueUnitOfWork.Repository<ServiceRoom>().Update(srvroom);
                                    await _queueUnitOfWork.SaveAsync();

                                }
                                       
                                ServiceRoom rm = new ServiceRoom();
                                rm.RoomId = link.Roomid;
                                rm.ServiceAreaid = link.ServiceAreaId;
                                rm.ServicePointId = link.ServicePointId;
                                rm.Active = true;
                                rm.CreateDate = DateTime.Now;
                                rm.CreatedBy = link.UserId;

                                await _queueUnitOfWork.Repository<ServiceRoom>().AddAsync(rm);
                                await _queueUnitOfWork.SaveAsync();

                            }
                        }

                    }


                    return Result<LinkageServiceRoomResponse>.Valid(new LinkageServiceRoomResponse()
                    {
                        Message = "The linkage was added successfully"
                    });
                }
                catch(Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<LinkageServiceRoomResponse>.Invalid(ex.Message);
                }
            }

        }
    }
}
