namespace IQCare.WebApi.Logic.MessageHandler
{
    public interface IIncomingMessageService
    {
        void Handle(string messageType, string message);
    }
}
