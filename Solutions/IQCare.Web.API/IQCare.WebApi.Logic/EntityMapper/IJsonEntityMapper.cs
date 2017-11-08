using System.Collections.Generic;
using IQCare.DTO;
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

        DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionDto prescription);

        void DrugOrderCancel();

        string DrugOrderFulfilment();

        void AppointmentScheduling();

        void AppointmentUpdated();

        void AppointmentRescheduling();

        void AppointmentCanceled();

        void AppointmentHonored();

        void UniquePatientIdentification();

        void ViralLoadLabOrder();

        //object DrugPrescriptionRaised(PrescriptionDto drugOrderDto);
    }
}