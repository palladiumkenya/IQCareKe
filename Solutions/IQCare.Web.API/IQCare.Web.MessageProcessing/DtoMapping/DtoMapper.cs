using IQCare.DTO;
using System;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.DtoMapping
{
    public class DtoMapper : IDtoMapper
    {
        public Registration PatientRegistrationMapping(PatientRegistrationEntity entity)
        {
            var registration = new Registration()
            {
                MotherMaidenName = entity.PATIENT_IDENTIFICATION.MOTHER_MAIDEN_NAME,
                MaritalStatus = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS,
                County = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY,
                SubCounty = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY,
                Ward = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD,
                Village = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE,
                DateOfDeath = entity.PATIENT_IDENTIFICATION.DEATH_DATE,
                DeathIndicator = entity.PATIENT_IDENTIFICATION.DEATH_INDICATOR,

            };
            return registration;
        }

        public void PatientTransferIn()
        {
            throw new NotImplementedException();
        }

        public void UpdatedClientInformation()
        {
            throw new NotImplementedException();
        }

        public void PatientTransferOut()
        {
            throw new NotImplementedException();
        }

        public void RegimenChange()
        {
            throw new NotImplementedException();
        }

        public void StopDrugs()
        {
            throw new NotImplementedException();
        }

        public void DrugPrescriptionRaised()
        {
            throw new NotImplementedException();
        }

        public void DrugOrderCancel()
        {
            throw new NotImplementedException();
        }

        public void DrugOrderFulfilment()
        {
            throw new NotImplementedException();
        }

        public void AppointmentScheduling()
        {
            throw new NotImplementedException();
        }

        public void AppointmentUpdated()
        {
            throw new NotImplementedException();
        }

        public void AppointmentRescheduling()
        {
            throw new NotImplementedException();
        }

        public void AppointmentCanceled()
        {
            throw new NotImplementedException();
        }

        public void AppointmentHonored()
        {
            throw new NotImplementedException();
        }

        public void UniquePatientIdentification()
        {
            throw new NotImplementedException();
        }

        public void ViralLoadLabOrder()
        {
            throw new NotImplementedException();
        }

        public void ViralLoadResults()
        {
            throw new NotImplementedException();
        }
    }
}