using System;
using System.Web.Script.Serialization;
using Application.Presentation;
using DataAccess.Base;
using Interface.Interop;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.Events;
using IQCare.Web.ApiLogic.Infrastructure.Interface;
using IQCare.Web.ApiLogic.Infrastructure.UiLogic;
using IQCare.Web.ApiLogic.Model;
using IQCare.Web.MessageProcessing.JsonEntityMapper;


namespace IQCare.Web.ApiLogic.MessageHandler
{
    public class OutgoingMessageService : ProcessBase, IOutgoingMessageService, ISendData
    {
        private readonly IJsonEntityMapper _jsonEntityMapper;
        private readonly IApiOutboxManager _apiOutboxManager;

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

        public  void Handle(MessageEventArgs messageEvent)
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

                case MessageType.ViralLoadResults:
                    HandleNewViralLoadResults(messageEvent);
                    break;
            }
        }

        private void HandleNewClientRegistration(MessageEventArgs messageEvent)
        {
            var processRegistration = new ProcessRegistration();
            var registrationDto = processRegistration.Get(messageEvent.PatientId);
            var registrationEntity = _jsonEntityMapper.PatientRegistration(registrationDto);
            string registrationJson = new JavaScriptSerializer().Serialize(registrationEntity);
            //save/send


            //Send
            SendData(registrationJson,"");

        }
        public void SendData(string jsonString, string endPoint)
        {
            ISendData mgr = (ISendData)ObjectFactory.CreateInstance("BusinessProcess.Interop.TcpDataExchange, BusinessProcess.Interop");
            mgr.SendData(jsonString, "");
        }
        private void HandlePatientTransferIn(MessageEventArgs messageEvent)
        {

        }

        private void HandleUpdatedClientInformation(MessageEventArgs messageEvent)
        {

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
                var processDrugPrescription = new DrugPrescriptionMessage();

                var prescriptionDto =processDrugPrescription.GetPrescriptionMessage(messageEvent.EntityId, messageEvent.PatientId);
                //var prescriptionEntity = _jsonEntityMapper.DrugPrescriptionRaised(prescriptionDto);
                //string prescriptionJson = JsonConvert.SerializeObject(prescriptionEntity);
                var apiOutbox = new ApiOutbox()
                {
                    DateRead = DateTime.Now,
                    //Message = prescriptionJson,

                };
                _apiOutboxManager.AddApiOutbox(apiOutbox);
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

        private void HandleNewViralLoadResults(MessageEventArgs messageEvent)
        {

        }

        
    }
}
