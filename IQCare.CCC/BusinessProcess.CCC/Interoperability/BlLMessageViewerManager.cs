using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BlLMessageViewerManager : ProcessBase, illMessageViewerManager
    {
        public List<ILMessageViewer> GetIlMessages(string messageType, bool isSuccess)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var messages = unitOfWork.IIlMessageViewerRepository
                    .FindBy(x => x.MessageType == messageType && x.IsSuccess == isSuccess).ToList();
                return messages;
            }
        }
    }
}