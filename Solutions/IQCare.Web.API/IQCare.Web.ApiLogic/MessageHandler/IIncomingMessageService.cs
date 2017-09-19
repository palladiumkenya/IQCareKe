namespace IQCare.Web.ApiLogic.MessageHandler
{
    public interface IIncomingMessageService
    {
        void Handle(string messageType, string message);
    }
}
