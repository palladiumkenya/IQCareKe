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

namespace IQCare.Queue.BusinessProcess.CommandHandlers
{
    public class EditLinkageServiceRoomCommandHandler :IRequestHandler<EditLinkageServiceRoomCommand,Result<EditLinkageServiceRoomResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<EditLinkageServiceRoomCommandHandler>();

        public int id;
        public string Message;
        public EditLinkageServiceRoomCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }



        public  async Task<Result<EditLinkageServiceRoomResponse>> Handle (EditLinkageServiceRoomCommand request,CancellationToken cancellationToken)
        {
            try
            {
                
                    var serviceroomresult = _queueUnitOfWork.Repository<ServiceRoom>().Get(x => x.Id == request.Id).FirstOrDefault();

                    if(serviceroomresult !=null)
                    {
                        serviceroomresult.RoomId = request.RoomId;
                        serviceroomresult.ServiceAreaid = request.ServiceAreaId;
                        serviceroomresult.ServicePointId = request.ServicePointId;
                        serviceroomresult.Active = request.Active;
                        serviceroomresult.DeleteFlag = request.DeleteFlag;
                        serviceroomresult.UpdateDate = DateTime.Now;
                        serviceroomresult.UpdatedBy = request.UpdatedBy;
                        serviceroomresult.DeleteFlag = request.DeleteFlag;
                        _queueUnitOfWork.Repository<ServiceRoom>().Update(serviceroomresult);
                       await  _queueUnitOfWork.SaveAsync();
                        id = serviceroomresult.Id;
                    }


                

                return Result<EditLinkageServiceRoomResponse>.Valid(new EditLinkageServiceRoomResponse()
                {
                    Message = "The data has been updated successfully",
                    Id = id,
                    Updated = true
                });
            }
            catch(Exception ex)
            {
                return Result<EditLinkageServiceRoomResponse>.Invalid(ex.Message);
            }
        }
    }
}
