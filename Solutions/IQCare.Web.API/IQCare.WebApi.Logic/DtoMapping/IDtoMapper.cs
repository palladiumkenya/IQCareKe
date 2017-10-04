using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities;

namespace IQCare.WebApi.Logic.DtoMapping
{
    public interface IDtoMapper
    {
        Registration PatientRegistrationMapping(PatientRegistrationEntity entity);
        void PatientTransferIn();
        void UpdatedClientInformation();
        void PatientTransferOut();
        void RegimenChange();
        void StopDrugs();
        void DrugPrescriptionRaised();
        void DrugOrderCancel();
        void DrugOrderFulfilment();
        void AppointmentScheduling();
        void AppointmentUpdated();
        void AppointmentRescheduling();
        void AppointmentCanceled();
        void AppointmentHonored();
        void UniquePatientIdentification();
        void ViralLoadLabOrder();
        ViralLoadResultEntity ViralLoadResults(ViralLoadResultEntity entity);
    }
}
