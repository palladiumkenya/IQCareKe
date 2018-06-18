using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BIlMessengerManager : ProcessBase, IIlMessengerManager
    {
        public IEnumerable<IlMessengerLog> GetIlMessengerLog(string options=null)
        {
           IEnumerable<IlMessengerLog>  ilMessengerLog=new List<IlMessengerLog>();

            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                switch (options)
                {
                    case "inbox":
                        ilMessengerLog = unitOfWork.IlMessengerRepository.FindBy(x => x.MessageType == "inbox")
                            .ToList();
                            break;
                    case "outbox":
                        ilMessengerLog = unitOfWork.IlMessengerRepository.FindBy(x => x.MessageType == "outbox")
                            .ToList();
                        break;
                         
                        default:
                            ilMessengerLog = unitOfWork.IlMessengerRepository.GetAll().ToList();
                                
                        break;
                }

                return ilMessengerLog;
            }
        }
    }
}