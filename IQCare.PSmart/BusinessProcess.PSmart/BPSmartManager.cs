using DataAccess.Base;
using Entities.CCC.PSmart;
using Interface.PSmart;
using System;

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
