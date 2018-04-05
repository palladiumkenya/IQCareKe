using DataAccess.Base;
using Entities.CCC.psmart;
using Interface.PSmart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessProcess.PSmart
{
    public class BPSmartManager : ProcessBase, IRequestHandler
    {
        public string ProcessIncomingSHR(SHR message)
        {
           //this called after logic (validation)
            return "";
        }



        public string ProcessWrittenSHR(string message)
        {
            throw new NotImplementedException();
        }
    }
}
