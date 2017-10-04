
using IQCare.Events;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public interface IOutgoingMessageService
    {
        int Handle(MessageEventArgs message);
    }
}