using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Interface.CCC.OneTimeEvent;

namespace IQCare.CCC.UILogic
{
   public class OneTimeEventsTrackerManager
    {
        private int _result;
        IOneTimeEventsTrackerManager _mgr = (IOneTimeEventsTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BOneTimeEventsTrackerManager, BusinessProcess.CCC");
    }
}
