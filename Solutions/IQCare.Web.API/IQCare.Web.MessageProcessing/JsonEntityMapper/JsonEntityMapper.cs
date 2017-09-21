using System.Collections.Generic;
using System;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.JsonEntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper

    {
        public PatientRegistrationEntity PatientRegistration(Registration entity)
        {
            var identifiers = new List<INTERNALPATIENTID>();
            foreach (var id in entity.InternalPatientIdentifiers)
            {
                var identifier = new INTERNALPATIENTID()
                {
                    ID = id.IdentifierValue,
                    IDENTIFIER_TYPE = id.IdentifierType,
                    ASSIGNING_AUTHORITY = id.AssigningAuthority
                };
                identifiers.Add(identifier);
            }

            var nextOfKin = new List<NEXTOFKIN>();
            var treatmentSupporter = new NEXTOFKIN()
            {
                NOK_NAME = new NOKNAME()
                {
                    FIRST_NAME = entity.TreatmentSupporter.FirstName,
                    MIDDLE_NAME = entity.TreatmentSupporter.MiddleName,
                    LAST_NAME = entity.TreatmentSupporter.LastName
                },
                CONTACT_ROLE = "T",
                RELATIONSHIP = entity.TSRelationshipType,
                PHONE_NUMBER = entity.TreatmentSupporter.MobileNumber,
                SEX = entity.TreatmentSupporter.Sex,
                DATE_OF_BIRTH = entity.TreatmentSupporter.DateOfBirth,
                ADDRESS = entity.TreatmentSupporter.PhysicalAddress
            };
            nextOfKin.Add(treatmentSupporter);
            var patientRegistrationEntity = new PatientRegistrationEntity()
            {
                MESSAGE_HEADER = GetMessageHeader("ADT^A04"),
                PATIENT_IDENTIFICATION = new PATIENTIDENTIFICATION()
                {
                    PATIENT_NAME = new PATIENTNAME()
                    {
                        FIRST_NAME = entity.Patient.FirstName,
                        MIDDLE_NAME = entity.Patient.MiddleName,
                        LAST_NAME = entity.Patient.LastName,
                    },
                    EXTERNAL_PATIENT_ID = new EXTERNALPATIENTID(),
                    DATE_OF_BIRTH = entity.Patient.DateOfBirth,
                    SEX = entity.Patient.Sex,
                    PHONE_NUMBER = entity.Patient.MobileNumber,
                    PATIENT_ADDRESS = new PATIENTADDRESS()
                    {
                        POSTAL_ADDRESS = entity.Patient.PhysicalAddress,
                        PHYSICAL_ADDRESS = new PHYSICALADDRESS()
                        {
                            COUNTY = entity.County,
                            SUB_COUNTY = entity.SubCounty,
                            VILLAGE = entity.Village,
                            WARD = entity.Ward
                        },
                    },
                    MARITAL_STATUS = entity.MaritalStatus,
                    DEATH_INDICATOR = entity.DeathIndicator,
                    DEATH_DATE = entity.DateOfDeath,
                    MOTHER_MAIDEN_NAME = entity.MotherMaidenName,
                    INTERNAL_PATIENT_ID = identifiers
                },
                NEXT_OF_KIN = nextOfKin,
                OBSERVATION_RESULT = new List<OBSERVATIONRESULT>()
            };
            return patientRegistrationEntity;
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
                    INTERNAL_PATIENT_ID =new List<INTERNALPATIENTID>()     
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
                PHARMACY_ENCODED_ORDER =new List<PHARMACYENCODEDORDER>()
                
            };

           // string prescriptionJSON = JsonConvert.SerializeObject(prescribeMessage);

            return prescribeMessage;

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
                RECEIVING_APPLICATION = "IL",
                RECEIVING_FACILITY = "",
                SECURITY = "",
                SENDING_APPLICATION = "IQCARE",
                SENDING_FACILITY = ""
            };
        }
    }
}