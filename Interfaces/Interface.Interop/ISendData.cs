using System.Threading.Tasks;

namespace Interface.Interop
{
    public interface ISendData
    {
        Task SendData(string jsonString, string endPoint);
    }
}
