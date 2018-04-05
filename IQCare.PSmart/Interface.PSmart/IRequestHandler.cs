using Entities.CCC.PSmart;

namespace Interface.PSmart
{
    public interface IRequestHandler
    {
        string ProcessIncomingSHR(SHR message);
        string ProcessWrittenSHR(string message);
       

        
    }
}
