using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BViralLoadMessage : ProcessBase, IViralLoadMessageManager
    {
        public ViralLoadMessage GetViralLoadMessageByEntityId(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var viralLoadMessages = unitOfWork.ViralLoadMessageRepository.FindBy(x => x.Id == entityId).FirstOrDefault();
                unitOfWork.Dispose();
                return viralLoadMessages;
            }
        }
    }
}
