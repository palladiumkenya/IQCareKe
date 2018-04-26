using Entities.CCC.PSmart;
using IQCare.DTO;
using IQCare.DTO.ObservationResult;
using IQCare.DTO.PatientAppointment;
using IQCare.DTO.PatientRegistration;
using IQCare.DTO.PSmart;
using IQCare.Events;
using IQCare.WebApi.Logic.MappingEntities;

namespace IQCare.WebApi.Logic.EntityMapper
{
    public interface IJsonEntityMapper
    {
        PatientRegistrationEntity PatientRegistration(PatientRegistrationDTO entity, MessageEventArgs messageEvent);

        void PatientTransferIn();

        void UpdatedClientInformation();

        void PatientTransferOut();

        void RegimenChange();

        void StopDrugs();

        DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionSourceDto prescription);

        void DrugOrderCancel();

        string DrugOrderFulfilment();

        PatientAppointmentEntity AppointmentScheduling(PatientAppointSchedulingDTO appointment, MessageEventArgs messageEvent);

        void AppointmentUpdated();

        void AppointmentRescheduling();

        void AppointmentCanceled();

        void AppointmentHonored();

        void UniquePatientIdentification();

        void ViralLoadLabOrder();

        //object DrugPrescriptionRaised(PrescriptionDto drugOrderDto);
        ObservationResultEntity ObservationResult(ObservationResultDTO observation, MessageEventArgs messageEvent);

        SHR ShrMessageEntity(DtoShr shrDto);
    }
}