using Entity.WebApi.PSmart;
using IQCare.DTO.PSmart;
using IQCare.WebApi.Logic.PSmart;
using System;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MessageHandler
{
    //enum PSmartMessageType
    //{
    //    ForProcessing=1,
    //    ForTransmission = 2
    //}
    public class PSmartMesssageService : IIncomingMessageService
    {
        ShrApiManager _apiManager=new ShrApiManager();
        public string FetchClientShr(object id)
        {
            throw new NotImplementedException();
        }

        public string FetchSmartcardEligibleList()
        {
            throw new NotImplementedException();
        }

        public void Handle(string messageType, string message)
        {
            if(messageType == "ForProcessing")
            {
                _apiManager.ProcessIncomingShr(message.ToString());
                //handleprocessing
            }
            else if (messageType == "ForTransmission")
            {
                //handletransmission

            }
        }

        public string ProcessCardSerialNumberIdentifier(psmartCard psmartCard)
        {
            throw new NotImplementedException();
        }

        public DtoShr ProcessCardSerialNumberIdentifierBluecard(psmartCard psmartCard)
        {
            throw new NotImplementedException();
        }

        public DtoShr LoadFromEmr(string CARD_SERIAL_NO)
        {
            throw new NotImplementedException();
        }

        public DtoShr FetchClientShrNew(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveShrFromMiddleware(Psmart_Store p)
        {
            throw new NotImplementedException();
        }

        DtoShr IIncomingMessageService.FetchClientShr(psmartCard psmartCard)
        {
            throw new NotImplementedException();
        }

        List<DtoSmartcardPatientList> IIncomingMessageService.FetchSmartcardEligibleList()
        {
            throw new NotImplementedException();
        }
    }
}
