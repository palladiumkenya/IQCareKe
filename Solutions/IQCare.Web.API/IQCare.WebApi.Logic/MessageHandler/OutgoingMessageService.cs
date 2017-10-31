using Application.Presentation;
using DataAccess.Base;
using Entity.WebApi;
using Interface.Interop;
using Interface.WebApi;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.DTO;
using IQCare.Events;
using IQCare.WebApi.Logic.EntityMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace IQCare.WebApi.Logic.MessageHandler
{
    public class OutgoingMessageService : ProcessBase, IOutgoingMessageService, ISendData
    {
        private readonly IJsonEntityMapper _jsonEntityMapper;
        private readonly IApiOutboxManager _apiOutboxManager = (IApiOutboxManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BApiOutBox, BusinessProcess.WebApi");


        //public event InteropEventHandler OnDataExchage;


        ////public event InteropEventHandler ILHandler;

        ////protected virtual void OnInterop(IlMessageEventArgs e)
        ////{
        ////    Handle(e);


        ////}
        //IDataExchange d;//= dataExchange;
        ////public OutgoingMessageService(IDataExchange dataExchange)
        ////{
        ////    IDataExchange d = dataExchange;
        ////    d.OnDataExchage += D_OnDataExchage; 
        ////}

        //private void D_OnDataExchage(MessageEventArgs e)
        //{
        //    Handle(e);
        //}
        //public void Subscribe(Publisher p)
        //{
        //    p.DataExchangeEvent += OnDataExchangeEvent;
        //}

        //private void OnDataExchangeEvent(object sender, MessageEventArgs args)
        //{
        //    Handle(args);
        //}


        //public void OnNotification(IDataExchange dataExchange)
        //{
        //    d = dataExchange;
        //    d.OnDataExchage += D_OnDataExchage;
        //}

        //public void NotifyListeners()
        //{
        //    throw new System.NotImplementedException();
        //}
        public OutgoingMessageService()
        {
            _apiOutboxManager = new ApiOutboxmanager();
            _jsonEntityMapper = new JsonEntityMapper();
        }


        public OutgoingMessageService(IJsonEntityMapper jsonEntityMapper, IApiOutboxManager apiOutboxManager)
        {
            _jsonEntityMapper = jsonEntityMapper;
            _apiOutboxManager = apiOutboxManager;
        }

        public  int Handle(MessageEventArgs messageEvent)
        {
            switch (messageEvent.MessageType)
            {
                case MessageType.NewClientRegistration:
                    HandleNewClientRegistration(messageEvent);
                    break;

                case MessageType.PatientTransferIn:
                    HandlePatientTransferIn(messageEvent);
                    break;

                case MessageType.UpdatedClientInformation:
                    HandleUpdatedClientInformation(messageEvent);
                    break;

                case MessageType.PatientTransferOut:
                    HandlePatientTransferOut(messageEvent);
                    break;

                case MessageType.RegimenChange:
                    HandleRegimenChange(messageEvent);
                    break;

                case MessageType.StopDrugs:
                    HandleStopDrugs(messageEvent);
                    break;

                case MessageType.DrugPrescriptionRaised:
                    HandleDrugPrescriptionRaised(messageEvent);
                    break;

                case MessageType.DrugOrderCancel:
                    HandleDrugOrdercancel(messageEvent);
                    break;

                case MessageType.DrugOrderFulfilment:
                    HandleDrugOrderFulfilment(messageEvent);
                    break;

                case MessageType.AppointmentScheduling:
                    HandleAppointmentScheduling(messageEvent);
                    break;

                case MessageType.AppointmentUpdated:
                    HandleAppointmentUpdated(messageEvent);
                    break;

                case MessageType.AppointmentRescheduling:
                    HandleAppointmentRescheduling(messageEvent);
                    break;

                case MessageType.AppointmentCanceled:
                    HandleAppointmentCancelled(messageEvent);
                    break;

                case MessageType.AppointmentHonored:
                    HandleAppointmentHonored(messageEvent);
                    break;

                case MessageType.UniquePatientIdentification:
                    HandleUniquePatientIdentification(messageEvent);
                    break;

                case MessageType.ViralLoadLabOrder:
                    HandleViralLoadLabOrder(messageEvent);
                    break;

            }

            return 1;
        }

        private void HandleNewClientRegistration(MessageEventArgs messageEvent)
        {
            var processRegistration = new ProcessRegistration();
            var registrationDto = processRegistration.Get(messageEvent.PatientId);
            var registrationEntity = _jsonEntityMapper.PatientRegistration(registrationDto, messageEvent);
            string registrationJson = new JavaScriptSerializer().Serialize(registrationEntity);
            
            //save/send
            //var apiOutbox = new ApiOutbox()
            //{
            //    DateRead = DateTime.Now,
            //    Message = registrationJson

            //};

            //_apiOutboxManager.AddApiOutbox(apiOutbox);

            //Send
            SendData(registrationJson, "").ConfigureAwait(false);

        }
        public async Task SendData(string jsonString, string endPoint)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(endPoint);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsoncontent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    // HTTP POST
                    using (HttpResponseMessage response = await httpClient.PostAsync("/api/", jsoncontent).ConfigureAwait(false))
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            string result = await content.ReadAsStringAsync();

                            // ... Display the result.
                            if (result != null &&
                                result.Length >= 50)
                            {
                                Console.WriteLine(result.Substring(0, 50) + "...");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void HandlePatientTransferIn(MessageEventArgs messageEvent)
        {

        }

        private void HandleUpdatedClientInformation(MessageEventArgs messageEvent)
        {
            var processRegistration = new ProcessRegistration();
            var registrationDto = processRegistration.Get(messageEvent.PatientId);
            var registrationEntity = _jsonEntityMapper.PatientRegistration(registrationDto, messageEvent);
            string registrationJson = new JavaScriptSerializer().Serialize(registrationEntity);

            ////save/send
            ////var apiOutbox = new ApiOutbox()
            ////{
            ////    DateRead = DateTime.Now,
            ////    Message = registrationJson

            ////};

            ////_apiOutboxManager.AddApiOutbox(apiOutbox);

            //Send
            SendData(registrationJson, "").ConfigureAwait(false);
        }

        private void HandlePatientTransferOut(MessageEventArgs messageEvent)
        {

        }

        private void HandleRegimenChange(MessageEventArgs messageEvent)
        {

        }

        private void HandleStopDrugs(MessageEventArgs messageEvent)
        {

        }

        private void HandleDrugPrescriptionRaised(MessageEventArgs messageEvent)
        {
            try
            {
                //var prescriptionManager = new DrugPrescriptionMessage();
                DrugPrescriptionMessage drugPrescriptionMessage = new DrugPrescriptionMessage();

                var prescriptionPayLoad =
                    drugPrescriptionMessage.GetPrescriptionMessage(messageEvent.PatientId, messageEvent.EntityId);

                List<DtoPatientIdentification> patientIdentification=new List<DtoPatientIdentification>();
                List<PharmacyEncodedOrder> drugsPayLoad = new List<PharmacyEncodedOrder>();

                foreach (var message in prescriptionPayLoad)
                {
                    var messageOrder = new PharmacyEncodedOrder()
                    {
                        DrugName = message.DRUG_NAME,
                        CodingSystem = message.CODING_SYSTEM,
                        Strength = message.STRENGTH,
                        Dosage = message.DOSAGE,
                        Frequency = message.FREQUENCY,
                        Duration = message.DURATION,
                        QuantityPrescribed = Convert.ToInt32(message.QUANTITY_PRESCRIBED),
                        PrescriptionNotes = message.NOTES
                    };
                    drugsPayLoad.Add(messageOrder);
                }



                PrescriptionDto prescriptionDtoPayLoad=new PrescriptionDto()
                {
                    MesssageHeader =
                    {
                        ProcessingId = "P",
                        SendingApplication = "IQCare",
                        SendingFacility = messageEvent.FacilityId.ToString(),
                        ReceivingApplication = "IL",
                        ReceivingFacility = messageEvent.FacilityId.ToString(),
                        MessageDatetime = prescriptionPayLoad[0].TRANSACTION_DATETIME,
                        Security = "",
                        MessageType = "RDE^001"
                    },
                    PatientIdentification =
                    {
                        ExternalPatientId =
                        {
                            AssigningAuthority = "",
                            IdentifierType = "",
                            IdentifierValue = ""
                        },
                        InternalPatientId =
                        {
                           AssigningAuthority = prescriptionPayLoad[0].ASSIGNING_AUTHORITY,
                           IdentifierValue = prescriptionPayLoad[0].Id,
                           IdentifierType = prescriptionPayLoad[0].IDENTIFIER_TYPE
                        },
                        PatientName =
                        {
                            FirstName = prescriptionPayLoad[0].FIRST_NAME,
                            MiddleName = prescriptionPayLoad[0].MIDDLE_NAME,
                            LastName = prescriptionPayLoad[0].LAST_NAME
                        }
                    },
                    CommonOrderDetails =
                    {
                        OrderControl = "NW",
                        PlacerOrderNumber =
                        {
                            Number = prescriptionPayLoad[0].ptn_pharmacy_pk,
                            Entity = prescriptionPayLoad[0].ENTITY
                        },
                        OrderStatus = prescriptionPayLoad[0].ORDER_STATUS,
                        OrderingPhysician =
                        {
                            FirstName = "",
                            MiddleName = "",
                            LastName = "" 
                        },
                        TransactionDatetime = prescriptionPayLoad[0].TRANSACTION_DATETIME,
                        Notes = prescriptionPayLoad[0].NOTES
                    },
                    PharmacyEncodedOrder = drugsPayLoad                   
                };

                var prescriptionEntityPayLoad = _jsonEntityMapper.DrugPrescriptionRaised(prescriptionDtoPayLoad);

                string prescriptionJson = new JavaScriptSerializer().Serialize(prescriptionEntityPayLoad);
                   var apiOutbox = new ApiOutbox()
                   {
                       DateRead = DateTime.Now,
                       Message = prescriptionJson,
                   };

                _apiOutboxManager.AddApiOutbox(apiOutbox);
                SendData(prescriptionJson, "").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               throw;
            }
        }

        private void HandleDrugOrdercancel(MessageEventArgs messageEvent)
        {

        }

        private void HandleDrugOrderFulfilment(MessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentScheduling(MessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentUpdated(MessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentRescheduling(MessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentHonored(MessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentCancelled(MessageEventArgs messageEvent)
        {

        }

        private void HandleUniquePatientIdentification(MessageEventArgs messageEvent)
        {

        }

        private void HandleViralLoadLabOrder(MessageEventArgs messageEvent)
        {

        }

    }
}
