namespace IQCare.Web.ApiLogic.MessageHandler
{
    public class IncomingMessageService : IIncomingMessageService
    {
        public void Handle(string messageType, string message)
        {
            switch (messageType)
            {
                case "ADT^A04":
                    HandleNewClientRegistration(message);
                    break;

                case "ADT^A08":
                    HandleUpdatedClientInformation(message);
                    break;

                case "RDE^001 ":
                    HandleDrugPrescriptionRaised(message);
                    break;

                case "RDS^O13":
                    HandleDrugOrderFulfilment(message);
                    break;

                case "SIU^S12":
                    HandleAppointments(message);
                    break;

                case "ORM^O01":
                    HandleViralLoadLabOrder(message);
                    break;

                case "ORU^R01":
                    HandleNewViralLoadResults(message);
                    break;
            }
        }

        private void HandleNewClientRegistration(string message)
        {
        }

        private void HandleUpdatedClientInformation(string message)
        {
        }

        private void HandleDrugPrescriptionRaised(string message)
        {
        }

        private void HandleDrugOrderFulfilment(string message)
        {
        }

        private void HandleAppointments(string message)
        {
        }

        private void HandleViralLoadLabOrder(string message)
        {
        }

        private void HandleNewViralLoadResults(string message)
        {
        }
    }
}