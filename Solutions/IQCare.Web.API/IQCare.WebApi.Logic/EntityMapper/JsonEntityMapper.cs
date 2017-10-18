using System.Collections.Generic;
using System;
using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities;

namespace IQCare.WebApi.Logic.EntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper

    {
        public PatientRegistrationEntity PatientRegistration(Registration entity)
        {
            PatientRegistrationEntity patientRegistration = new PatientRegistrationEntity();

            patientRegistration.MESSAGE_HEADER = GetMessageHeader("ADT^A04", "13122", "P");
            patientRegistration.PATIENT_IDENTIFICATION = PATIENTIDENTIFICATION.GetPatientidentification(entity);
            patientRegistration.NEXT_OF_KIN = NEXTOFKIN.GetNextOfKins(entity);
            patientRegistration.VISIT = VISIT.GetVisit(entity);

            return patientRegistration;
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

        public DrugPrescriptionEntity DrugPrescriptionRaised(List<PrescriptionDto> prescription)
        {
            throw new NotImplementedException();
        }

        public DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionDto entity)
        {
            // var prescribeMessage=new DrugPrescriptionEntity()
            // {
            //     MESSAGE_HEADER =
            //     {
            //         SENDING_APPLICATION = "IQCARE",
            //         SENDING_FACILITY = "13050",
            //         RECEIVING_APPLICATION = "IL",
            //         RECEIVING_FACILITY = "",
            //         MESSAGE_DATETIME = entity.CommonOrderDetails.TransactionDatetime,
            //         SECURITY = "",
            //         MESSAGE_TYPE = "RDE^001",
            //         PROCESSING_ID = "P"
            //     },
            //     PATIENT_IDENTIFICATION =
            //     {
            //         INTERNAL_PATIENT_ID =new List<INTERNALPATIENTID>()     
            //     },
            //     COMMON_ORDER_DETAILS =
            //     {
            //         ORDER_CONTROL = entity.CommonOrderDetails.OrderControl,
            //         PLACER_ORDER_NUMBER = { NUMBER = entity.CommonOrderDetails.PlacerOrderNumber.Number.ToString(),ENTITY = "IQCARE"},
            //         ORDER_STATUS = entity.CommonOrderDetails.OrderStatus,
            //         ORDERING_PHYSICIAN =
            //         {
            //             FIRST_NAME = entity.CommonOrderDetails.OrderingPhysician.FirstName,
            //             MIDDLE_NAME = entity.CommonOrderDetails.OrderingPhysician.MiddleName,
            //             LAST_NAME = entity.CommonOrderDetails.OrderingPhysician.LastName
            //         },
            //         TRANSACTION_DATETIME = entity.CommonOrderDetails.TransactionDatetime.ToShortDateString()
            //     },
            //     PHARMACY_ENCODED_ORDER =new List<PHARMACYENCODEDORDER>()

            // };

            //// string prescriptionJSON = JsonConvert.SerializeObject(prescribeMessage);

            // return prescribeMessage;

            throw new NotImplementedException();

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

        object IJsonEntityMapper.DrugPrescriptionRaised(PrescriptionDto drugOrderDto)
        {
            return DrugPrescriptionRaised(drugOrderDto);
        }

        private MESSAGEHEADER GetMessageHeader(string messageType, string sendingFacility, string processingId)
        {
            return new MESSAGEHEADER()
            {
                MESSAGE_TYPE = messageType,
                MESSAGE_DATETIME = DateTime.Now.ToString("yyyyMMddHmmss"),
                PROCESSING_ID = processingId,
                RECEIVING_APPLICATION = "IL",
                RECEIVING_FACILITY = "",
                SECURITY = "",
                SENDING_APPLICATION = "IQCARE",
                SENDING_FACILITY = sendingFacility
            };
        }

       
    }
}