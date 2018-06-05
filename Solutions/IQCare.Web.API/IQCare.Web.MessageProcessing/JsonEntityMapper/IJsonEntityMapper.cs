using System.Collections.Generic;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonMappingEntities;


namespace IQCare.Web.MessageProcessing.JsonEntityMapper
{
    public interface IJsonEntityMapper
    {
        PatientRegistrationEntity PatientRegistration(Registration entity);

        void PatientTransferIn();

        void UpdatedClientInformation();

        void PatientTransferOut();

        void RegimenChange();

        void StopDrugs();

        DrugPrescriptionEntity DrugPrescriptionRaised(List<PrescriptionDto> prescription);

        void DrugOrderCancel();

        void DrugOrderFulfilment();

        void AppointmentScheduling();

        void AppointmentUpdated();

        void AppointmentRescheduling();

        void AppointmentCanceled();

        void AppointmentHonored();

        void UniquePatientIdentification();

        void ViralLoadLabOrder();

        void ViralLoadResults();
        object DrugPrescriptionRaised(PrescriptionDto drugOrderDto);
    }
}