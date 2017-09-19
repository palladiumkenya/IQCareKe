namespace IQCare.DTO
{
    public class IlMessageEvent
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
        UpdatedClientInformation,
        PatientTransferOut,
        RegimenChange,
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