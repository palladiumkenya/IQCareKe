using IQCare.DTO;

namespace IQCare.Web.ApiLogic.MessageHandler
{
    public interface IOutgoingMessageService
    {
        void Handle(IlMessageEvent message);
    }
}