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
            if (entity.TreatmentSupporter != null)
            {
                var treatmentSupporter = new NEXTOFKIN()
                {

                    NOK_NAME = new NOKNAME()
                    {
                        FIRST_NAME = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.FirstName) ? entity.TreatmentSupporter.FirstName : "",
                        MIDDLE_NAME = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.MiddleName) ? entity.TreatmentSupporter.MiddleName : "",
                        LAST_NAME = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.LastName) ? entity.TreatmentSupporter.LastName : ""
                    },
                    CONTACT_ROLE = "T",
                    RELATIONSHIP = !string.IsNullOrWhiteSpace(entity.TSRelationshipType) ? entity.TSRelationshipType : "",
                    PHONE_NUMBER = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.MobileNumber) ? entity.TreatmentSupporter.MobileNumber : "",
                    SEX = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.Sex) ? entity.TreatmentSupporter.Sex : "",
                    DATE_OF_BIRTH = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.DateOfBirth) ? entity.TreatmentSupporter.DateOfBirth : "",
                    ADDRESS = !string.IsNullOrWhiteSpace(entity.TreatmentSupporter.PhysicalAddress) ? entity.TreatmentSupporter.PhysicalAddress : ""
                };
                nextOfKin.Add(treatmentSupporter);
            }
            
            
            var patientRegistrationEntity = new PatientRegistrationEntity()
            {
                MESSAGE_HEADER = GetMessageHeader("ADT^A04"),
                PATIENT_IDENTIFICATION = new PATIENTIDENTIFICATION()
                {
                    PATIENT_NAME = new PATIENTNAME()
                    {
                        FIRST_NAME = !string.IsNullOrWhiteSpace(entity.Patient.FirstName)? entity.Patient.FirstName:"",
                        MIDDLE_NAME = !string.IsNullOrWhiteSpace(entity.Patient.MiddleName)? entity.Patient.MiddleName:"",
                        LAST_NAME = !string.IsNullOrWhiteSpace(entity.Patient.LastName)? entity.Patient.LastName:"",
                    },
                    EXTERNAL_PATIENT_ID = new EXTERNALPATIENTID()
                    {
                        ID = "",
                        IDENTIFIER_TYPE = "GODS_NUMBER",
                        ASSIGNING_AUTHORITY = "MPI"
                    },
                    DATE_OF_BIRTH = !string.IsNullOrWhiteSpace(entity.Patient.DateOfBirth)?entity.Patient.DateOfBirth:null,
                    SEX = !string.IsNullOrWhiteSpace(entity.Patient.Sex)? entity.Patient.Sex:"",
                    PHONE_NUMBER = !string.IsNullOrWhiteSpace(entity.Patient.MobileNumber)? entity.Patient.MobileNumber:"",
                    PATIENT_ADDRESS = new PATIENTADDRESS()
                    {
                        POSTAL_ADDRESS = !string.IsNullOrWhiteSpace(entity.Patient.PhysicalAddress)? entity.Patient.PhysicalAddress:"",
                        PHYSICAL_ADDRESS = new PHYSICALADDRESS()
                        {
                            COUNTY = !string.IsNullOrWhiteSpace(entity.County)? entity.County:"",
                            SUB_COUNTY = !string.IsNullOrWhiteSpace(entity.SubCounty)? entity.SubCounty:"",
                            VILLAGE = !string.IsNullOrWhiteSpace(entity.Village)? entity.Village:"",
                            WARD = !string.IsNullOrWhiteSpace(entity.Ward)? entity.Ward:""
                        },
                    },
                    MARITAL_STATUS = !string.IsNullOrWhiteSpace(entity.MaritalStatus)? entity.MaritalStatus:"",
                    DEATH_INDICATOR = !string.IsNullOrWhiteSpace(entity.DeathIndicator)? entity.DeathIndicator:"",
                    DEATH_DATE = "",
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
                    MESSAGE_DATETIME = "201709221034",// entity.CommonOrderDetails.TransactionDatetime,
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
                MESSAGE_DATETIME = "201709221034",// DateTime.Now.ToString("yyyyMMddHHmm"),
                PROCESSING_ID = "",
                RECEIVING_APPLICATION = "IL",
                RECEIVING_FACILITY = "10829",
                SECURITY = "",
                SENDING_APPLICATION = "IQCARE",
                SENDING_FACILITY = "10829"
            };
        }
    }
}