using IQCare.CCC.UILogic.Interoperability;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonEntityMapper;
using Newtonsoft.Json;

namespace IQCare.Web.ApiLogic.MessageHandler
{
    public class OutgoingMessageService : IOutgoingMessageService
    {
        private readonly IJsonEntityMapper _jsonEntityMapper;

        event InteropEventHandler handler;
        protected virtual void OnInterop(IlMessageEventArgs e)
        {
            Handle(e);


        }

       

        public OutgoingMessageService(IJsonEntityMapper jsonEntityMapper)
        {
            _jsonEntityMapper = jsonEntityMapper;
        }

        public void Handle(IlMessageEventArgs messageEvent)
        {
            switch (messageEvent.MessageType)
            {
                case IlMessageType.NewClientRegistration:
                    HandleNewClientRegistration(messageEvent);
                    break;

                case IlMessageType.PatientTransferIn:
                    HandlePatientTransferIn(messageEvent);
                    break;

                case IlMessageType.UpdatedClientInformation:
                    HandleUpdatedClientInformation(messageEvent);
                    break;

                case IlMessageType.PatientTransferOut:
                    HandlePatientTransferOut(messageEvent);
                    break;

                case IlMessageType.RegimenChange:
                    HandleRegimenChange(messageEvent);
                    break;

                case IlMessageType.StopDrugs:
                    HandleStopDrugs(messageEvent);
                    break;

                case IlMessageType.DrugPrescriptionRaised:
                    HandleDrugPrescriptionRaised(messageEvent);
                    break;

                case IlMessageType.DrugOrderCancel:
                    HandleDrugOrdercancel(messageEvent);
                    break;

                case IlMessageType.DrugOrderFulfilment:
                    HandleDrugOrderFulfilment(messageEvent);
                    break;

                case IlMessageType.AppointmentScheduling:
                    HandleAppointmentScheduling(messageEvent);
                    break;

                case IlMessageType.AppointmentUpdated:
                    HandleAppointmentUpdated(messageEvent);
                    break;

                case IlMessageType.AppointmentRescheduling:
                    HandleAppointmentRescheduling(messageEvent);
                    break;

                case IlMessageType.AppointmentCanceled:
                    HandleAppointmentCancelled(messageEvent);
                    break;

                case IlMessageType.AppointmentHonored:
                    HandleAppointmentHonored(messageEvent);
                    break;

                case IlMessageType.UniquePatientIdentification:
                    HandleUniquePatientIdentification(messageEvent);
                    break;

                case IlMessageType.ViralLoadLabOrder:
                    HandleViralLoadLabOrder(messageEvent);
                    break;

                case IlMessageType.ViralLoadResults:
                    HandleNewViralLoadResults(messageEvent);
                    break;
            }
        }

        private void HandleNewClientRegistration(IlMessageEventArgs messageEvent)
        {
            var processRegistration = new ProcessRegistration();
            var registrationDto = processRegistration.Get(messageEvent.PatientId);
            var registrationEntity = _jsonEntityMapper.PatientRegistration(registrationDto);
            string registrationJson = JsonConvert.SerializeObject(registrationEntity);
            //save/send
           
        }

        private void HandlePatientTransferIn(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleUpdatedClientInformation(IlMessageEventArgs messageEvent)
        {

        }

        private void HandlePatientTransferOut(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleRegimenChange(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleStopDrugs(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleDrugPrescriptionRaised(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleDrugOrdercancel(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleDrugOrderFulfilment(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentScheduling(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentUpdated(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentRescheduling(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentHonored(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleAppointmentCancelled(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleUniquePatientIdentification(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleViralLoadLabOrder(IlMessageEventArgs messageEvent)
        {

        }

        private void HandleNewViralLoadResults(IlMessageEventArgs messageEvent)
        {

        }

    }
}
