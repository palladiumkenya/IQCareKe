using IQCare.DTO;
namespace IQCare.Web.MessageProcessing.Services
{
    public interface IOutgoingMessageService
    {
        void Handle(IlMessageEvent message);
    }
}