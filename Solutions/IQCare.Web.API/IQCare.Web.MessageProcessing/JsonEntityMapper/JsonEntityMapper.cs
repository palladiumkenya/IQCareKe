using System;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.JsonEntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper

    {
        public PatientRegistrationEntity PatientRegistration(Registration entity)
        {
            
            var patientReegistrationEntiy = new PatientRegistrationEntity()
            {
                MESSAGE_HEADER = GetMessageHeader("ADT^A04"),
                //PATIENT_IDENTIFICATION = ,
                //NEXT_OF_KIN = ,
                //OBSERVATION_RESULT = 
            };
            return patientReegistrationEntiy;
        }

        public void PatientTransferIn()
        {
            throw new System.NotImplementedException();
        }

        public void UpdatedClientInformation()
        {
            throw new System.NotImplementedException();
        }

        public void PatientTransferOut()
        {
            throw new System.NotImplementedException();
        }

        public void RegimenChange()
        {
            throw new System.NotImplementedException();
        }

        public void StopDrugs()
        {
            throw new System.NotImplementedException();
        }

        public void DrugPrescriptionRaised()
        {
           var identifiersDto=new DTOIdentifier()
           {
               //IdentifierType = 
           };
        }

        public void DrugOrderCancel()
        {
            throw new System.NotImplementedException();
        }

        public void DrugOrderFulfilment()
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentScheduling()
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentUpdated()
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentRescheduling()
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentCanceled()
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentHonored()
        {
            throw new System.NotImplementedException();
        }

        public void UniquePatientIdentification()
        {
            throw new System.NotImplementedException();
        }

        public void ViralLoadLabOrder()
        {
            throw new System.NotImplementedException();
        }

        public void ViralLoadResults()
        {
            throw new System.NotImplementedException();
        }

        private MESSAGEHEADER GetMessageHeader(string messageType)
        {
            return new MESSAGEHEADER()
            {
                MESSAGE_TYPE = messageType,
                MESSAGE_DATETIME = DateTime.Now,
                PROCESSING_ID = "",
                RECEIVING_APPLICATION = "",
                RECEIVING_FACILITY = "",
                SECURITY = "",
                SENDING_APPLICATION = "IQCARE",
                SENDING_FACILITY = ""
            };
        }
    }
}