using System;

namespace IQCare.Events
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs()
        {
            ObservationType = ObservationType.WhoStage;
        }
        public int PatientId { get; set; }
        public int EntityId { get; set; }
        public MessageType MessageType { get; set; }
        public string EventOccurred { get; set; }
        public int FacilityId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public ObservationType ObservationType { get; set; }
    }

    public enum ObservationType
    {
        WhoStage
    }
    public enum MessageType
    {
        NewClientRegistration,
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
        ViralLoadResults,
        ObservationResult
    }
}
