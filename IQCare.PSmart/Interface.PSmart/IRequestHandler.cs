using Entities.CCC.psmart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.PSmart
{
    public interface IRequestHandler
    {
        string ProcessIncomingSHR(SHR message);
        string ProcessWrittenSHR(string message);
       

        
    }
}
