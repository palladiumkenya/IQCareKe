namespace IQCare.WebApi.Logic.MessageHandler
{
    public interface IIncomingMessageService
    {
        void Handle(string messageType, string message);

        List<DtoSmartcardPatientList> FetchSmartcardEligibleList();

        DtoShr FetchClientShr(psmartCard psmartCard);

        DtoShr FetchClientShrNew(int id);

        int SaveShrFromMiddleware(Psmart_Store p);

        string ProcessCardSerialNumberIdentifier(psmartCard psmartCard);

        DtoShr ProcessCardSerialNumberIdentifierBluecard(psmartCard psmartCard);

        DtoShr LoadFromEmr(string CARD_SERIAL_NO);
    }
}