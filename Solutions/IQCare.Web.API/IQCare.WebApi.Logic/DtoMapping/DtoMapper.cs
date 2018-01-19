
using System;
using System.Collections.Generic;
using System.Linq;
using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities;
using IQCare.WebApi.Logic.MappingEntities.drugs;

namespace IQCare.WebApi.Logic.DtoMapping
{
    public class DtoMapper : IDtoMapper
    {
        //todo handle possible null reference exceptions when fetching data from lists
        public Registration PatientRegistrationMapping(PatientRegistrationEntity entity)
        {
            //var patient = DTOPerson.SetDTOPerson(
            //    entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
            //    entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
            //    entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
            //    entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH,
            //    false,
            //    entity.PATIENT_IDENTIFICATION.SEX,
            //    entity.PATIENT_IDENTIFICATION.PHONE_NUMBER,
            //    entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS
            //    );

            var patient = new DTOPerson()
            {
                FirstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                MiddleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                LastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                //DateOfBirth = entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH,
                MobileNumber = entity.PATIENT_IDENTIFICATION.PHONE_NUMBER,
                NationalId = entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(n => n.IDENTIFIER_TYPE == "NATIONAL_ID").ID,
                Sex = entity.PATIENT_IDENTIFICATION.SEX,
                PhysicalAddress = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS,
                //todo update precision once updated in IL
                DobPrecision = false
            };

            //var ts = entity.NEXT_OF_KIN.FirstOrDefault(n => n.CONTACT_ROLE == "T");
            //var treatmentSupporter = new DTOPerson()
            //{
            //    FirstName = ts.NOK_NAME.FIRST_NAME,
            //    MiddleName = ts.NOK_NAME.MIDDLE_NAME,
            //    LastName = ts.NOK_NAME.LAST_NAME,
            //    PhysicalAddress = ts.ADDRESS,
            //    Sex = ts.SEX,
            //    DateOfBirth = ts.DATE_OF_BIRTH,
            //    MobileNumber = ts.PHONE_NUMBER,
            //    //todo update precision once updated in IL
            //    //NationalId = ,
            //    DobPrecision = false
            //};
            var identifiers = new List<DTOIdentifier>();
            foreach (var id in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                var identifier = new DTOIdentifier()
                {
                    AssigningAuthority = id.ASSIGNING_AUTHORITY,
                    IdentifierType = id.IDENTIFIER_TYPE,
                    IdentifierValue = id.ID
                };
                identifiers.Add(identifier);
            }

            var registration = new Registration()
            {
                Patient = patient,
                //MotherMaidenName = entity.PATIENT_IDENTIFICATION.MOTHER_MAIDEN_NAME,
                MaritalStatus = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS,
                County = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY,
                SubCounty = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY,
                Ward = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD,
                Village = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE,
                //DateOfDeath = entity.PATIENT_IDENTIFICATION.DEATH_DATE,
                DeathIndicator = entity.PATIENT_IDENTIFICATION.DEATH_INDICATOR,
                //TreatmentSupporter = treatmentSupporter,
                //TSRelationshipType =  ts.RELATIONSHIP,
                InternalPatientIdentifiers = identifiers,
                //DateOfEnrollment = DateTime.Now,

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

        public PrescriptionDto DrugPrescriptionRaised(DrugPrescriptionEntity entity)
        {

            List<INTERNAL_PATIENT_ID> internalIdentifiers = new List<INTERNAL_PATIENT_ID>();

            foreach (var identifier in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                
                internalIdentifiers.Add(new INTERNAL_PATIENT_ID{ ID =identifier.ID,ASSIGNING_AUTHORITY  = identifier.ASSIGNING_AUTHORITY,IDENTIFIER_TYPE =identifier.IDENTIFIER_TYPE});

                //internalIdentifiers.Add(new INTERNAL_PATIENT_ID { ID = identifier, ASSIGNING_AUTHORITY = identifier.ASSIGNING_AUTHORITY, IDENTIFIER_TYPE = identifier.IDENTIFIER_TYPE });

                //var internalIdentity = new INTERNAL_PATIENT_ID()
                //{
                //    IDENTIFIER_TYPE = identifier.IDENTIFIER_TYPE,
                //    ID = identifier.ID,
                //    ASSIGNING_AUTHORITY = identifier.ASSIGNING_AUTHORITY

                //};

                //internalIdentifiers.Add(internalIdentity);
            }

            var orderEncorder = new List<PHARMACY_ENCODED_ORDER>();

            foreach (var order in entity.PHARMACY_ENCODED_ORDER)
            {
                var prescriptionOrder=new PHARMACY_ENCODED_ORDER()
                {
                    DRUG_NAME = order.DRUG_NAME,
                    CODING_SYSTEM = order.CODING_SYSTEM,
                    STRENGTH = order.STRENGTH,
                    DOSAGE = order.DOSAGE.ToString(),
                    FREQUENCY = order.FREQUENCY,
                    DURATION = Convert.ToInt32(order.DURATION),
                    QUANTITY_PRESCRIBED = order.QUANTITY_PRESCRIBED.ToString(),
                    INDICATION = order.INDICATION,
                    PHARMACY_ORDER_DATE =Convert.ToDateTime(order.PHARMACY_ORDER_DATE),
                    TREATMENT_INSTRUCTION = order.PRESCRIPTION_NOTES
                };
                orderEncorder.Add(prescriptionOrder);
            }

            var patientName = new PATIENT_NAME()
            {
                FIRST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                LAST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                MIDDLE_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME
            };


            var drugOrder = new PrescriptionDto()
            {
                MESSAGE_HEADER = 
                {
                    SENDING_APPLICATION = "IQCARE",
                    SENDING_FACILITY = entity.MESSAGE_HEADER.SENDING_FACILITY,
                    RECEIVING_APPLICATION = entity.MESSAGE_HEADER.RECEIVING_APPLICATION,
                    RECEIVING_FACILITY = entity.MESSAGE_HEADER.RECEIVING_FACILITY,
                    MESSAGE_DATETIME = Convert.ToDateTime(entity.MESSAGE_HEADER.MESSAGE_DATETIME), //DateTime.Now.ToString("yyyyMMddHHmmss"); 
                    SECURITY = entity.MESSAGE_HEADER.SECURITY,
                    MESSAGE_TYPE = entity.MESSAGE_HEADER.MESSAGE_TYPE,
                    PROCESSING_ID = entity.MESSAGE_HEADER.PROCESSING_ID

                },
                PATIENT_IDENTIFICATION = 
                {
                    EXTERNAL_PATIENT_ID = 
                    {
                        ASSIGNING_AUTHORITY = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        ID = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID,
                        IDENTIFIER_TYPE = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE

                    },
                    INTERNAL_PATIENT_ID = internalIdentifiers,
                    PATIENT_NAME = 
                    {
                        FIRST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                        MIDDLE_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
                        LAST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME
                    }
                },
                COMMON_ORDER_DETAILS = 
                {
                    ORDER_CONTROL = entity.COMMON_ORDER_DETAILS.ORDER_CONTROL,
                    PLACER_ORDER_NUMBER= 
                    {
                        NUMBER = entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ToString(),
                        ENTITY = entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY
                    },
                    ORDER_STATUS = entity.COMMON_ORDER_DETAILS.ORDER_STATUS,
                    ORDERING_PHYSICIAN = 
                    {
                        FIRST_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.FIRST_NAME,
                        MIDDLE_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.MIDDLE_NAME,
                        LAST_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.LAST_NAME
                    },
                    TRANSACTION_DATETIME = Convert.ToDateTime(entity.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME),
                    NOTES = entity.COMMON_ORDER_DETAILS.NOTES.ToString()
                },
                PHARMACY_ENCODED_ORDER = orderEncorder 
            };            
            return drugOrder;
        }

        public void DrugOrderCancel()
        {
            throw new NotImplementedException();
        }

        public DtoDrugDispensed DrugOrderFulfilment(DrugDispenseEntity entity)
        {
            var internalIdentifiers = new List<DTOIdentifier>();
            var pharmacyEncodedOrder=new List<PharmacyEncodededOrderDispenseDto>();
            var pharmacyDispense=new List<PharmacyDispensedDrugs>();

            var identify = new DTOIdentifier()
            {

                //ExternalPatientId =
                //{
                //    IdentifierValue = entity.Patientidentification.EXTERNAL_PATIENT_ID.ID,
                //    IdentifierType = entity.Patientidentification.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE,
                //    AssigningAuthority = entity.Patientidentification.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY
                //},
                //PatientName =
                //{
                //    FirstName = entity.Patientidentification.PATIENT_NAME.FIRST_NAME,
                //    LastName = entity.Patientidentification.PATIENT_NAME.LAST_NAME,
                //    MiddleName = entity.Patientidentification.PATIENT_NAME.MIDDLE_NAME
                //}

            };

            foreach (var identifier in internalIdentifiers)
            {
                var internalIdentity = new DTOIdentifier()
                {
                    IdentifierValue = identifier.IdentifierValue,
                    IdentifierType = identifier.IdentifierType,
                    AssigningAuthority = identifier.AssigningAuthority
                };
                
                internalIdentifiers.Add(internalIdentity);
            }

            foreach (var pharmacyEncorder in pharmacyEncodedOrder)
            {
                var encorder=new PharmacyEncodededOrderDispenseDto()
                {
                    DRUG_NAME = pharmacyEncorder.DRUG_NAME,
                    CODING_SYSTEM = pharmacyEncorder.CODING_SYSTEM,
                    STRENGTH=pharmacyEncorder.STRENGTH,
                    DOSAGE = pharmacyEncorder.DOSAGE,
                    FREQUENCY = pharmacyEncorder.FREQUENCY,
                    DURATION = pharmacyEncorder.DURATION,
                    QUANTITY_PRESCRIBED = pharmacyEncorder.QUANTITY_PRESCRIBED,
                    PRESCRIPTION_NOTES = pharmacyEncorder.PRESCRIPTION_NOTES,
                };
                pharmacyEncodedOrder.Add(encorder);
            }

            foreach (var drugDispense in entity.PHARMACY_DISPENSE)
            {
                var dispense = new PharmacyDispensedDrugs()
                {
                    ACTUAL_DRUGS = drugDispense.ACTUAL_DRUGS,
                    CODING_SYSTEM = drugDispense.CODING_SYSTEM,
                    DISPENSING_NOTES = drugDispense.DISPENSING_NOTES,
                    DOSAGE = drugDispense.DOSAGE,
                    DRUG_NAME = drugDispense.DRUG_NAME,
                    DURATION = drugDispense.DURATION,
                    FREQUENCY = drugDispense.FREQUENCY,
                    QUANTITY_DISPENSED = drugDispense.QUANTITY_DISPENSED
                };
                pharmacyDispense.Add(dispense);
            }

            var dispenseOrder = new DtoDrugDispensed()
            {
                MESSAGE_HEADER =
                {
                    SENDING_FACILITY = entity.MESSAGE_HEADER.SENDING_FACILITY,
                    SENDING_APPLICATION = entity.MESSAGE_HEADER.SENDING_APPLICATION,
                    RECEIVING_APPLICATION = entity.MESSAGE_HEADER.RECEIVING_APPLICATION,
                    RECEIVING_FACILITY = entity.MESSAGE_HEADER.RECEIVING_FACILITY,
                    SECURITY = entity.MESSAGE_HEADER.SECURITY,
                    MESSAGE_TYPE = entity.MESSAGE_HEADER.MESSAGE_TYPE,
                    PROCESSING_ID = entity.MESSAGE_HEADER.PROCESSING_ID
                },
                PATIENT_IDENTIFICATION =
                {
                    EXTERNAL_PATIENT_ID = 
                    {
                        AssigningAuthority = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        IdentifierValue = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID,
                        IdentifierType = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE
                    },
                    //InternalPatientId = internalIdentifiers,
                    PATIENT_NAME = 
                    {
                        FirstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                        MiddleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
                        LastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME
                    }
                },
                COMMON_ORDER_DETAILS =
                {
                    OrderControl = entity.COMMON_ORDER_DETAILS.ORDER_CONTROL,
                    NOTES = entity.COMMON_ORDER_DETAILS.NOTES,
                    ORDERING_PHYSICIAN = 
                    {
                        FIRST_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.FIRST_NAME,
                        MIDDLE_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.MIDDLE_NAME,
                        LAST_NAME = entity.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.LAST_NAME
                    },
                    ORDER_STATUS = entity.COMMON_ORDER_DETAILS.ORDER_STATUS,
                    PLACER_ORDER_NUMBER = 
                    {
                        NUMBER =Convert.ToInt32(entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER),
                        ENTITY = entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY
                    },
                    FILLER_ORDER_NUMBER =
                    {
                        NUMBER=Convert.ToInt32(entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER),
                        ENTITY=entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY
                    },
                    TRANSACTION_DATETIME =Convert.ToDateTime(entity.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME),
                },
                PHARMACY_ENCODED_ORDER=pharmacyEncodedOrder,
                PHARMACY_DISPENSE = pharmacyDispense,
              //  PH = drugsDispensed
            };
            return dispenseOrder;
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

        public ViralLoadResultsDto ViralLoadResults(ViralLoadResultEntity entity)
        {
            var internalIdentifiers = new List<DTOIdentifier>();
            var viralLoadResults = new List<VLoadlResult>();
            foreach (var identifier in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                if (identifier.IDENTIFIER_TYPE == "CCC_NUMBER")
                {
                    var internalIdentifier = new DTOIdentifier()
                    {
                        IdentifierValue = identifier.ID,
                        IdentifierType = identifier.IDENTIFIER_TYPE,
                        AssigningAuthority = identifier.ASSIGNING_AUTHORITY
                    };
                    internalIdentifiers.Add(internalIdentifier);
                }
                    
            }

            foreach (var result in entity.VIRAL_LOAD_RESULT)
            {
                DateTime dateSampleCollected = DateTime.Today;
                DateTime dateSampleTested = DateTime.Today;
                if (result.DATE_SAMPLE_COLLECTED != "")
                    dateSampleCollected = DateTime.ParseExact(result.DATE_SAMPLE_COLLECTED, "yyyyMMdd",null);
                if (result.DATE_SAMPLE_TESTED != "")
                    dateSampleTested = DateTime.ParseExact(result.DATE_SAMPLE_TESTED, "yyyyMMdd", null);
                var vlLoadResult = new VLoadlResult()
                {
                    DateSampleCollected = dateSampleCollected,
                    DateSampleTested = dateSampleTested,
                    VlResult = result.VL_RESULT,
                    SampleRejection = result.SAMPLE_REJECTION,
                    Justification = result.JUSTIFICATION,
                    LabTestedIn = result.LAB_TESTED_IN,
                    Regimen = result.REGIMEN,
                    SampleType = result.SAMPLE_TYPE
                };
                viralLoadResults.Add(vlLoadResult);
            }

            var vlResultsDto = new ViralLoadResultsDto()
            {
                MesssageHeader =
                {
                    SendingApplication = entity.MESSAGE_HEADER.SENDING_APPLICATION,
                    SendingFacility = entity.MESSAGE_HEADER.SENDING_FACILITY,
                    ReceivingApplication = entity.MESSAGE_HEADER.RECEIVING_APPLICATION,
                    ReceivingFacility = entity.MESSAGE_HEADER.RECEIVING_FACILITY,
                    MessageDatetime = DateTime.ParseExact(entity.MESSAGE_HEADER.MESSAGE_DATETIME, "yyyyMMddHHmmss", null),
                    Security = entity.MESSAGE_HEADER.SECURITY,
                    MessageType = entity.MESSAGE_HEADER.MESSAGE_TYPE,
                    ProcessingId = entity.MESSAGE_HEADER.PROCESSING_ID

                },
                PatientIdentification =
                {
                   INTERNAL_PATIENT_ID = internalIdentifiers
                },
                ViralLoadResult = viralLoadResults
            };
            return vlResultsDto;
        }
    }
}