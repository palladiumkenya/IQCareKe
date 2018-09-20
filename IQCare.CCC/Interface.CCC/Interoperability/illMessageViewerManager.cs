using System.Collections.Generic;
using Entities.CCC.IL;

namespace Interface.CCC.Interoperability
{
    public interface illMessageViewerManager
    {
        List<ILMessageViewer> GetIlMessages(string messageType, bool isSuccess);
    }
}