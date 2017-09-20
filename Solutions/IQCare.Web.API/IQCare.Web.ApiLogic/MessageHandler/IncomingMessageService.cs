using System;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.DTO;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Model;
using IQCare.Web.MessageProcessing.DtoMapping;
using IQCare.Web.MessageProcessing.JsonMappingEntities;
using Newtonsoft.Json;

namespace IQCare.Web.ApiLogic.MessageHandler
{
    public class IncomingMessageService : IIncomingMessageService
    {
        private readonly IApiInboxmanager _apiInboxmanager;
        private readonly IDtoMapper _dtoMapper;

        public IncomingMessageService(IApiInboxmanager apiInboxmanager, IDtoMapper dtoMapper)
        {
            _apiInboxmanager = apiInboxmanager;
            _dtoMapper = dtoMapper;
        }

        public void Handle(string messageType, string message)
        {
            var apiInbox = new ApiInbox()
            {
                DateReceived = DateTime.Now,
                Message = message,
                //Todo get sender Id from interop table
                SenderId = 1
            };


            switch (messageType)
            {
                case "ADT^A04":
                    HandleNewClientRegistration(apiInbox);
                    break;

                case "ADT^A08":
                    HandleUpdatedClientInformation(apiInbox);
                    break;

                case "RDE^001 ":
                    HandleDrugPrescriptionRaised(apiInbox);
                    break;

                case "RDS^O13":
                    HandleDrugOrderFulfilment(apiInbox);
                    break;

                case "SIU^S12":
                    HandleAppointments(apiInbox);
                    break;

                case "ORM^O01":
                    HandleViralLoadLabOrder(apiInbox);
                    break;

                case "ORU^R01":
                    HandleNewViralLoadResults(apiInbox);
                    break;
            }
        }

        private void HandleNewClientRegistration(ApiInbox incomingMessage)
        {
            try
            {
                PatientRegistrationEntity entity = JsonConvert.DeserializeObject<PatientRegistrationEntity>(incomingMessage.Message);
                Registration register = _dtoMapper.PatientRegistrationMapping(entity);
                var processRegistration = new ProcessRegistration();
                processRegistration.Save(register);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
                Console.WriteLine(e);
                throw;
            }
            incomingMessage.DateProcessed = DateTime.Now;
            incomingMessage.Processed = true;
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleUpdatedClientInformation(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleDrugPrescriptionRaised(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleDrugOrderFulfilment(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleAppointments(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleViralLoadLabOrder(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleNewViralLoadResults(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }
    }
}