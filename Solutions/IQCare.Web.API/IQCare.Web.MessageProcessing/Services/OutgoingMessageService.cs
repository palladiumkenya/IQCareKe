using IQCare.DTO;
namespace IQCare.Web.MessageProcessing.Services
{
    public class OutgoingMessageService : IOutgoingMessageService
    {
        public void Handle(IlMessageEvent messageEvent)
        {
            switch (messageEvent.MessageType)
            {
                case IlMessageType.NewclientRegistration:
                    HandleNewClientRegistration();
                    break;

                case IlMessageType.PatientTransferIn:
                    HandlePatientTransferIn();
                    break;

                case IlMessageType.UpdatedClientInformation:
                    HandleUpdatedClientInformation();
                    break;

                case IlMessageType.PatientTransferOut:
                    HandlePatientTransferOut();
                    break;

                case IlMessageType.RegimenChange:
                    HandleRegimenChange();
                    break;

                case IlMessageType.StopDrugs:
                    HandleStopDrugs();
                    break;

                case IlMessageType.DrugPrescriptionRaised:
                    HandleDrugPrescriptionRaised();
                    break;

                case IlMessageType.DrugOrderCancel:
                    HandleDrugOrdercancel();
                    break;

                case IlMessageType.DrugOrderFulfilment:
                    HandleDrugOrderFulfilment();
                    break;

                case IlMessageType.AppointmentScheduling:
                    HandleAppointmentScheduling();
                    break;

                case IlMessageType.AppointmentUpdated:
                    HandleAppointmentUpdated();
                    break;

                case IlMessageType.AppointmentRescheduling:
                    HandleAppointmentRescheduling();
                    break;

                case IlMessageType.AppointmentCanceled:
                    HandleAppointmentCancelled();
                    break;

                case IlMessageType.AppointmentHonored:
                    HandleAppointmentHonored();
                    break;

                case IlMessageType.UniquePatientIdentification:
                    HandleUniquePatientIdentification();
                    break;

                case IlMessageType.ViralLoadLabOrder:
                    HandleViralLoadLabOrder();
                    break;

                case IlMessageType.ViralLoadResults:
                    HandleNewViralLoadResults();
                    break;
            }
        }

        private void HandleNewClientRegistration()
        {
            
        }

        private void HandlePatientTransferIn()
        {

        }

        private void HandleUpdatedClientInformation()
        {

        }

        private void HandlePatientTransferOut()
        {

        }

        private void HandleRegimenChange()
        {

        }

        private void HandleStopDrugs()
        {

        }

        private void HandleDrugPrescriptionRaised()
        {

        }

        private void HandleDrugOrdercancel()
        {

        }

        private void HandleDrugOrderFulfilment()
        {

        }

        private void HandleAppointmentScheduling()
        {

        }

        private void HandleAppointmentUpdated()
        {

        }

        private void HandleAppointmentRescheduling()
        {

        }

        private void HandleAppointmentHonored()
        {

        }

        private void HandleAppointmentCancelled()
        {

        }

        private void HandleUniquePatientIdentification()
        {

        }

        private void HandleViralLoadLabOrder()
        {

        }

        private void HandleNewViralLoadResults()
        {

        }

    }
}
