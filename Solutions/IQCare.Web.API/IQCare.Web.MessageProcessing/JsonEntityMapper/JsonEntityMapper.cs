using System.Collections.Generic;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.JsonEntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper

    {
        public PatientRegistrationEntity PatientRegistration(Registration entity)
        {
            throw new System.NotImplementedException();
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

        public DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionDto entity)
        {
            var piList = new List<INTERNALPATIENTID>();
            var drgulist=new INTERNALPATIENTID()
            {
                
            };
            
            var prescribeMessage=new DrugPrescriptionEntity()
            {
                MESSAGE_HEADER =
                {
                    SENDING_APPLICATION = "IQCARE",
                    SENDING_FACILITY = "13050",
                    RECEIVING_APPLICATION = "IL",
                    RECEIVING_FACILITY = "",
                    MESSAGE_DATETIME = entity.CommonOrderDetails.TransactionDatetime,
                    SECURITY = "",
                    MESSAGE_TYPE = "RDE^001",
                    PROCESSING_ID = "P"
                },
                PATIENT_IDENTIFICATION =
                {
                    INTERNAL_PATIENT_ID = 
                    {
                       

                    }
                },
                COMMON_ORDER_DETAILS =
                {
                    ORDER_CONTROL = entity.CommonOrderDetails.OrderControl,
                    PLACER_ORDER_NUMBER = { NUMBER = entity.CommonOrderDetails.PlacerOrderNumber.Number.ToString(),ENTITY = "IQCARE"},
                    ORDER_STATUS = entity.CommonOrderDetails.OrderStatus,
                    ORDERING_PHYSICIAN =
                    {
                        FIRST_NAME = entity.CommonOrderDetails.OrderingPhysician.FirstName,
                        MIDDLE_NAME = entity.CommonOrderDetails.OrderingPhysician.MiddleName,
                        LAST_NAME = entity.CommonOrderDetails.OrderingPhysician.LastName
                    },
                    TRANSACTION_DATETIME = entity.CommonOrderDetails.TransactionDatetime.ToShortDateString()
                },
                PHARMACY_ENCODED_ORDER =
                {
                    
                }
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

        void IJsonEntityMapper.DrugPrescriptionRaised(PrescriptionDto prescription)
        {
            throw new System.NotImplementedException();
        }
    }
}