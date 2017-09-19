using IQCare.DTO.DTO;
namespace IQCare.Web.MessageProcessing.Services
{
    public interface IMessageService
    {
        void Handle(IlMessageEvent message);
    }
}