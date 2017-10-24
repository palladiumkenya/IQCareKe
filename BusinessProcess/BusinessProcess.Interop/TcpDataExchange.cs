using DataAccess.Base;
using Interface.Interop;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessProcess.Interop
{
    public class TcpDataExchange : ProcessBase, ISendData
    {
        //private static ManualResetEvent connectDone = new ManualResetEvent(false);
        //private static ManualResetEvent sendDone = new ManualResetEvent(false);
        //private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        //static string response = string.Empty;
        public async Task SendData(string jsonString, string endPoint)
        {
          
            DataAccess.Interop.SocketClientAsync.SendToClient(jsonString);
           

        }
        
    }
}