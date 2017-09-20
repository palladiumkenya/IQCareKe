
using IQCare.Events;

namespace IQCare.Web.ApiLogic.MessageHandler
{
    public interface IOutgoingMessageService
    {
        void Handle(MessageEventArgs message);
    }
}