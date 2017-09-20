using DataAccess.Base;
using Interface.Interop;
using System.Threading;

namespace BusinessProcess.Interop
{
    public class TcpDataExchange : ProcessBase, ISendData
    {
        //private static ManualResetEvent connectDone = new ManualResetEvent(false);
        //private static ManualResetEvent sendDone = new ManualResetEvent(false);
        //private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        //static string response = string.Empty;
        public void SendData(string jsonString, string endPoint)
        {
          
            DataAccess.Interop.SocketClientAsync.SendToClient(jsonString);
           

        }
        
    }
}