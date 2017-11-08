
using IQCare.Events;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public interface IOutgoingMessageService
    {
        void Handle(MessageEventArgs message);
    }
}