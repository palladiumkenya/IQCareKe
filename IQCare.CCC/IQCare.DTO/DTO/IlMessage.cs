namespace IQCare.DTO.DTO
{
    public class IlMessage
    {
        public int PatientId { get; set; }
        public int EntityId { get; set; }
        public IlMessageType MessageType { get; set; }
        public string EventOccurred { get; set; }
    }

    public enum IlMessageType
    {
        NewclientRegistration,
        PatientTransferIn,
        Updatedclientinformation,
        PatientTransferOut,
        Regimenchange,
        StopDrugs,
        DrugPrescriptionRaised,
        DrugOrderCancel,
        DrugOrderFulfilment,
        AppointmentScheduling,
        AppointmentUpdated,
        AppointmentRescheduling,
        AppointmentCanceled,
        AppointmentHonored,
        UniquePatientIdentification,
        ViralLoadLabOrder,
        ViralLoadResults
    }
}