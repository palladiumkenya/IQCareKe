﻿
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

            var internalIdentifiers = new List<DTOIdentifier>();

            foreach (var identifier in entity.Patientidentification.INTERNAL_PATIENT_ID)
            {
                var internalIdentity = new DTOIdentifier()
                {
                    IdentifierType = identifier.IDENTIFIER_TYPE,
                    IdentifierValue = identifier.ID,
                    AssigningAuthority = identifier.ASSIGNING_AUTHORITY

                };
                internalIdentifiers.Add(internalIdentity);
            }

            var orderEncorder = new List<PharmacyEncorderOrderDto>();

            foreach (var order in entity.PharmacyEncorderEntity)
            {
                var prescriptionOrder=new PharmacyEncorderOrderDto()
                {
                    DrugName = order.DrugName,
                    CodingSystem = order.CodingSystem,
                    Strength = order.Strength,
                    Dosage = Convert.ToDecimal(order.Dosage),
                    Frequency = order.Frequency,
                    Duration =Convert.ToInt32(order.Duration),
                    QuantityPrescribed =Convert.ToInt32(order.QuantityPrescribed),
                    PrescriptionNotes = order.PrescriptionNotes
                };
                orderEncorder.Add(prescriptionOrder);
            }

            var patientName = new PATIENTNAME()
            {
                FIRST_NAME = entity.Patientidentification.PATIENT_NAME.FIRST_NAME,
                LAST_NAME = entity.Patientidentification.PATIENT_NAME.LAST_NAME,
                MIDDLE_NAME = entity.Patientidentification.PATIENT_NAME.MIDDLE_NAME
            };


            var drugOrder = new PrescriptionDto()
            {
                MesssageHeader =
                {
                    SendingApplication = "IQCARE",
                    SendingFacility = entity.MessageHeaderEntity.SendingFacility,
                    ReceivingApplication = entity.MessageHeaderEntity.ReceivingApplication,
                    ReceivingFacility = entity.MessageHeaderEntity.ReceivingFacility,
                    MessageDatetime =Convert.ToDateTime(entity.MessageHeaderEntity.MessageDatetime), //DateTime.Now.ToString("yyyyMMddHHmmss"); 
                    Security = entity.MessageHeaderEntity.Security,
                    MessageType = entity.MessageHeaderEntity.MessageType,
                    ProcessingId = entity.MessageHeaderEntity.ProcessingId

                },
                PatientIdentification =
                {
                    ExternalPatientId =
                    {
                        AssigningAuthority = entity.Patientidentification.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        IdentifierValue = entity.Patientidentification.EXTERNAL_PATIENT_ID.ID,
                        IdentifierType = entity.Patientidentification.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE

                    },
                    InternalPatientId =internalIdentifiers,
                    PatientName =
                    {
                        FirstName = entity.Patientidentification.PATIENT_NAME.FIRST_NAME,
                        MiddleName = entity.Patientidentification.PATIENT_NAME.MIDDLE_NAME,
                        LastName = entity.Patientidentification.PATIENT_NAME.LAST_NAME
                    }
                },
                CommonOrderDetails = 
                {
                    OrderControl = entity.CommonOrderDetailsEntity.OrderControl,
                    PlacerOrderNumberDto= 
                    {
                        Number = Convert.ToInt32(entity.CommonOrderDetailsEntity.PlacerOrderNumberEntity.Number),
                        Entity = entity.CommonOrderDetailsEntity.PlacerOrderNumberEntity.Entity
                    },
                    OrderStatus = entity.CommonOrderDetailsEntity.OrderStatus,
                    OrderingPhysicianDto = 
                    {
                        FirstName = entity.CommonOrderDetailsEntity.OrderingPhysicianEntity.FirstName,
                        MiddleName = entity.CommonOrderDetailsEntity.OrderingPhysicianEntity.MiddleName,
                        LastName = entity.CommonOrderDetailsEntity.OrderingPhysicianEntity.LastName
                    },
                    TransactionDatetime = Convert.ToDateTime(entity.CommonOrderDetailsEntity.TransactionDatetime),
                    Notes = entity.CommonOrderDetailsEntity.Notes.ToString()
                },
                PharmacyEncodedOrderDto = orderEncorder 
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
            var drugsOrderdList=new List<PharmacyEncorderOrderDto>();
            var drugsDispensed=new List<PharmacyDispensedDrugs>();

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

            foreach (var encoded in drugsOrderdList)
            {
                var encorder=new PharmacyEncorderOrderDto()
                {
                    DrugName = encoded.DrugName,
                    Dosage = encoded.Dosage,
                    CodingSystem = encoded.CodingSystem,
                    Duration = encoded.Duration,
                    Frequency = encoded.Frequency,
                    PrescriptionNotes = encoded.PrescriptionNotes,
                    QuantityPrescribed = encoded.QuantityPrescribed
                };
                drugsOrderdList.Add(encorder);
            }

            foreach (var drugDispense in entity.PharmacyDispense)
            {
                var dispense = new PharmacyDispensedDrugs()
                {
                    ActualDrugs = drugDispense.ActualDrugs,
                    CodingSystem = drugDispense.CodingSystem,
                    DispensingNotes = drugDispense.DispensingNotes,
                    Dosage = drugDispense.Dosage,
                    DrugName = drugDispense.DrugName,
                    Duration = drugDispense.Duration,
                    Frequency = drugDispense.Frequency,
                    QuantityDispensed = drugDispense.QuantityDispensed
                };
                drugsDispensed.Add(dispense);
            }

            var dispenseOrder = new DtoDrugDispensed()
            {
                MessageHeader =
                {
                    SendingFacility = entity.Messageheader.SENDING_FACILITY,
                    SendingApplication = entity.Messageheader.SENDING_APPLICATION,
                    ReceivingApplication = entity.Messageheader.RECEIVING_APPLICATION,
                    ReceivingFacility = entity.Messageheader.RECEIVING_FACILITY,
                    Security = entity.Messageheader.SECURITY,
                    MessageType = entity.Messageheader.MESSAGE_TYPE,
                    ProcessingId = entity.Messageheader.PROCESSING_ID
                },
                PatientIdentification =
                {
                    ExternalPatientId =
                    {
                        AssigningAuthority = entity.Patientidentification.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        IdentifierValue = entity.Patientidentification.EXTERNAL_PATIENT_ID.ID,
                        IdentifierType = entity.Patientidentification.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE
                    },
                    //InternalPatientId = internalIdentifiers,
                    PatientName =
                    {
                        FirstName = entity.Patientidentification.PATIENT_NAME.FIRST_NAME,
                        MiddleName = entity.Patientidentification.PATIENT_NAME.MIDDLE_NAME,
                        LastName = entity.Patientidentification.PATIENT_NAME.LAST_NAME
                    }
                },
                CommonOrderDetails =
                {
                    OrderControl = entity.Commonorderdetails.OrderControl,
                    Notes = entity.Commonorderdetails.Notes,
                    OrderingPhysicianDto = 
                    {
                        FirstName = entity.Commonorderdetails.OrderingPhysicianEntity.FirstName,
                        MiddleName = entity.Commonorderdetails.OrderingPhysicianEntity.MiddleName,
                        LastName = entity.Commonorderdetails.OrderingPhysicianEntity.LastName
                    },
                    OrderStatus = entity.Commonorderdetails.OrderStatus,
                    PlacerOrderNumberDto = 
                    {
                        Number =Convert.ToInt32(entity.Commonorderdetails.PlacerOrderNumberEntity.Number),
                        Entity = entity.Commonorderdetails.PlacerOrderNumberEntity.Entity
                    },
                    TransactionDatetime =Convert.ToDateTime(entity.Commonorderdetails.TransactionDatetime),
                },
                PharmacyEncodedOrder = drugsOrderdList,
                PharmacyDispense = drugsDispensed
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
            var internalIdentifier = new DTOIdentifier();
            var viralLoadResults = new List<VLoadlResult>();
            foreach (var identifier in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                if (identifier.IDENTIFIER_TYPE == "CCC_NUMBER")
                    internalIdentifier = new DTOIdentifier()
                    {
                        IdentifierValue = identifier.ID,
                        IdentifierType = identifier.IDENTIFIER_TYPE,
                        AssigningAuthority = identifier.ASSIGNING_AUTHORITY
                    };
            }

            foreach (var result in entity.VIRAL_LOAD_RESULT)
            {
                var vlLoadResult = new VLoadlResult()
                {
                    DateSampleCollected = result.DATE_SAMPLE_COLLECTED,
                    DateSampleTested = result.DATE_SAMPLE_TESTED,
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
                    MessageDatetime =Convert.ToDateTime(entity.MESSAGE_HEADER.MESSAGE_DATETIME), //DateTime.Now.ToString("yyyyMMddHHmmss"); 
                    Security = entity.MESSAGE_HEADER.SECURITY,
                    MessageType = entity.MESSAGE_HEADER.MESSAGE_TYPE,
                    ProcessingId = entity.MESSAGE_HEADER.PROCESSING_ID

                },
                PatientIdentification =
                {
                   // InternalPatientId = internalIdentifiers,
                    ExternalPatientId =
                    {
                        IdentifierValue = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID,
                        AssigningAuthority = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        IdentifierType = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE
                    },
                    PatientName =
                    {
                        LastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                        MiddleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
                        FirstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME
                    }
                },
                ViralLoadResult = viralLoadResults
            };
            return vlResultsDto;
        }
    }
}