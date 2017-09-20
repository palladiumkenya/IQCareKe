using IQCare.CCC.UILogic.Interoperability;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonEntityMapper;
using Newtonsoft.Json;

namespace IQCare.Web.ApiLogic.MessageHandler
{
    public class OutgoingMessageService : IOutgoingMessageService
    {
        private readonly IJsonEntityMapper _jsonEntityMapper;

        public OutgoingMessageService(IJsonEntityMapper jsonEntityMapper)
        {
            _jsonEntityMapper = jsonEntityMapper;
        }

        public void Handle(IlMessageEvent messageEvent)
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

        private void HandleNewClientRegistration(IlMessageEvent messageEvent)
        {
            var processRegistration = new ProcessRegistration();
            var registrationDto = processRegistration.Get(messageEvent.PatientId);
            var registrationEntity = _jsonEntityMapper.PatientRegistration(registrationDto);
            string registrationJson = JsonConvert.SerializeObject(registrationEntity);
        }

        private void HandlePatientTransferIn(IlMessageEvent messageEvent)
        {

        }

        private void HandleUpdatedClientInformation(IlMessageEvent messageEvent)
        {

        }

        private void HandlePatientTransferOut(IlMessageEvent messageEvent)
        {

        }

        private void HandleRegimenChange(IlMessageEvent messageEvent)
        {

        }

        private void HandleStopDrugs(IlMessageEvent messageEvent)
        {

        }

        private void HandleDrugPrescriptionRaised(IlMessageEvent messageEvent)
        {

        }

        private void HandleDrugOrdercancel(IlMessageEvent messageEvent)
        {

        }

        private void HandleDrugOrderFulfilment(IlMessageEvent messageEvent)
        {

        }

        private void HandleAppointmentScheduling(IlMessageEvent messageEvent)
        {

        }

        private void HandleAppointmentUpdated(IlMessageEvent messageEvent)
        {

        }

        private void HandleAppointmentRescheduling(IlMessageEvent messageEvent)
        {

        }

        private void HandleAppointmentHonored(IlMessageEvent messageEvent)
        {

        }

        private void HandleAppointmentCancelled(IlMessageEvent messageEvent)
        {

        }

        private void HandleUniquePatientIdentification(IlMessageEvent messageEvent)
        {

        }

        private void HandleViralLoadLabOrder(IlMessageEvent messageEvent)
        {

        }

        private void HandleNewViralLoadResults(IlMessageEvent messageEvent)
        {

        }

    }
}
