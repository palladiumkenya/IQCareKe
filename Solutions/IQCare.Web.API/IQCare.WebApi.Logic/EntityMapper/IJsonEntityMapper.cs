using System.Collections.Generic;
using IQCare.DTO;
using IQCare.DTO.ObservationResult;
using IQCare.DTO.PatientAppointment;
using IQCare.Events;
using IQCare.WebApi.Logic.MappingEntities;
using IQCare.DTO.PatientRegistration;

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

        DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionSourceDto prescriptionSourceDto);

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
    }
}