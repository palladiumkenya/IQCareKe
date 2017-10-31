using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ViralLoadMessageManager
    {
        IViralLoadMessageManager _mgr = (IViralLoadMessageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BViralLoadMessage, BusinessProcess.CCC");

        public ViralLoadMessage GetViralLoadMessageByEntityId(int entityId)
        {
            try
            {
                return _mgr.GetViralLoadMessageByEntityId(entityId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
