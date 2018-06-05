using System;
using System.Collections.Generic;
using System.Linq;
using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities;
//using IQCare.WebApi.Logic.MappingEntities.drugs;
using HIVTEST = IQCare.DTO.PSmart.HIVTEST;
using IMMUNIZATION = IQCare.DTO.PSmart.IMMUNIZATION;
using INTERNALPATIENTID = IQCare.DTO.PSmart.INTERNALPATIENTID;
using MOTHERIDENTIFIER = IQCare.DTO.PSmart.MOTHERIDENTIFIER;
using NEXTOFKIN = IQCare.DTO.PSmart.NEXTOFKIN;
using AutoMapper;
using Entities.CCC.PSmart;

using IQCare.DTO.PSmart;
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

        public PrescriptionSourceDto DrugPrescriptionRaised(DrugPrescriptionEntity entity)
        {
            // Patient Identification
            var internalIdentifiers = new List<INTERNAL_PATIENT_ID>();

            foreach (var identifier in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                var internalIdentity = new INTERNAL_PATIENT_ID()
                {
                    IDENTIFIER_TYPE = identifier.IDENTIFIER_TYPE,
                    ID = identifier.ID,
                    ASSIGNING_AUTHORITY = identifier.ASSIGNING_AUTHORITY
                };

                internalIdentifiers.Add(internalIdentity);
            }

            var pharmacyEncodedOrders = new List<PHARMACY_ENCODED_ORDER>();

            foreach (var order in entity.PHARMACY_ENCODED_ORDER)
            {
                var prescriptionOrder = new PHARMACY_ENCODED_ORDER()
                {
                    DRUG_NAME = order.DRUG_NAME,
                    CODING_SYSTEM = order.CODING_SYSTEM,
                    STRENGTH = order.STRENGTH,
                    DOSAGE = order.DOSAGE.ToString(),
                    FREQUENCY = order.FREQUENCY,
                    DURATION = Convert.ToInt32(order.DURATION),
                    QUANTITY_PRESCRIBED = order.QUANTITY_PRESCRIBED.ToString(),
                    INDICATION = order.INDICATION,
                    PHARMACY_ORDER_DATE = Convert.ToDateTime(order.PHARMACY_ORDER_DATE),
                    TREATMENT_INSTRUCTION = order.PRESCRIPTION_NOTES
                };
                pharmacyEncodedOrders.Add(prescriptionOrder);
            }

            var patientName = new PATIENT_NAME()
            {
                FIRST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                LAST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                MIDDLE_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME
            };

            var prescriptionSourceDto = new PrescriptionSourceDto()
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
                PHARMACY_ENCODED_ORDER = pharmacyEncodedOrders
            };
            return prescriptionSourceDto;
        }

        public void DrugOrderCancel()
        {
            throw new NotImplementedException();
        }

        public DtoDrugDispensed DrugOrderFulfilment(PharmacyDispenseEntity entity)
        {
            var internalIdentifiers = new List<DTOIdentifier>();
            var pharmacyEncodedOrder = new List<PHARMACY_ENCODED_ORDER>();
            var pharmacyDispense = new List<PHARMACY_DISPENSE>();

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
                var encorder = new PHARMACY_ENCODED_ORDER()
                {
                    DRUG_NAME = pharmacyEncorder.DRUG_NAME,
                    CODING_SYSTEM = pharmacyEncorder.CODING_SYSTEM,
                    STRENGTH = pharmacyEncorder.STRENGTH,
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
                var dispense = new PHARMACY_DISPENSE()
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
                        ASSIGNING_AUTHORITY = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        ID = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID,
                        IDENTIFIER_TYPE = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE
                    },
                    //InternalPatientId = internalIdentifiers,
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
                        NUMBER =entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER,
                        ENTITY = entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY
                    },
                    FILLER_ORDER_NUMBER =
                    {
                        NUMBER=entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER,
                        ENTITY=entity.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY
                    },
                    TRANSACTION_DATETIME =Convert.ToDateTime(entity.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME),
                },
                PHARMACY_ENCODED_ORDER = pharmacyEncodedOrder,
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
                    dateSampleCollected = DateTime.ParseExact(result.DATE_SAMPLE_COLLECTED, "yyyyMMdd", null);
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

        public DtoShr ShrMessageDto(SHR entity)
        {
            var hivtestList = new List<DTO.PSmart.HIVTEST>();
            var immunizationList = new List<DTO.PSmart.IMMUNIZATION>();
            var nextOfKinList = new List<DTO.PSmart.NEXTOFKIN>();
            var internalIdentfierList = new List<DTO.PSmart.INTERNALPATIENTID>();
            var motherIdentifierList = new List<DTO.PSmart.MOTHERIDENTIFIER>();

            try
            {
                foreach (var hivTest in entity.HIV_TEST)
                {
                    var internalHivTest = new DTO.PSmart.HIVTEST()
                    {
                        DATE = hivTest.DATE, //?.ToString("yyyyMMdd") ?? "",
                        FACILITY = hivTest.FACILITY,
                        PROVIDER_DETAILS =
                        {
                            ID = hivTest.PROVIDER_DETAILS.ID,
                            NAME = hivTest.PROVIDER_DETAILS.NAME
                        },
                        RESULT = hivTest.RESULT,
                        STRATEGY = hivTest.STRATEGY,
                        TYPE = hivTest.TYPE
                    };
                    hivtestList.Add(internalHivTest);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            foreach (var imm in entity.IMMUNIZATION)
            {
                var internalImmunization = new IMMUNIZATION()
                {
                    NAME = imm.NAME,
                    DATE_ADMINISTERED = imm.DATE_ADMINISTERED
                };
                immunizationList.Add(internalImmunization);
            }

            foreach (var nextofKin in entity.NEXT_OF_KIN)
            {
                var internalNextofKin = new NEXTOFKIN()
                {
                    ADDRESS = nextofKin.ADDRESS,
                    CONTACT_ROLE = nextofKin.CONTACT_ROLE,
                    DATE_OF_BIRTH = nextofKin.DATE_OF_BIRTH,
                    NOK_NAME =
                    {
                        FIRST_NAME = nextofKin.NOK_NAME.FIRST_NAME,
                        LAST_NAME = nextofKin.NOK_NAME.LAST_NAME,
                        MIDDLE_NAME = nextofKin.NOK_NAME.MIDDLE_NAME
                    },
                    PHONE_NUMBER = nextofKin.PHONE_NUMBER,
                    RELATIONSHIP = nextofKin.RELATIONSHIP,
                    SEX = nextofKin.SEX
                };
                nextOfKinList.Add(internalNextofKin);
            }

            foreach (var internalidentifier in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                var identifier = new INTERNALPATIENTID()
                {
                    ASSIGNING_AUTHORITY = internalidentifier.ASSIGNING_AUTHORITY,
                    ASSIGNING_FACILITY = internalidentifier.ASSIGNING_FACILITY,
                    ID = internalidentifier.ID,
                    IDENTIFIER_TYPE = internalidentifier.IDENTIFIER_TYPE,
                };
                internalIdentfierList.Add(identifier);
            }

            foreach (var identifier in entity.PATIENT_IDENTIFICATION.MOTHER_DETAILS.MOTHER_IDENTIFIER)
            {
                var motherIdentifier = new DTO.PSmart.MOTHERIDENTIFIER()
                {
                    IDENTIFIER_TYPE = identifier.IDENTIFIER_TYPE,
                    ASSIGNING_AUTHORITY = identifier.ASSIGNING_AUTHORITY,
                    ASSIGNING_FACILITY = identifier.ASSIGNING_FACILITY,
                    ID = identifier.ID
                };
                motherIdentifierList.Add(motherIdentifier);
            }
            //

            var dtoShr = new DtoShr()
            {
                CARD_DETAILS =
                {
                    LAST_UPDATED = entity.CARD_DETAILS.LAST_UPDATED,
                    LAST_UPDATED_FACILITY = entity.CARD_DETAILS.LAST_UPDATED_FACILITY,
                    REASON = entity.CARD_DETAILS.REASON,
                    STATUS = entity.CARD_DETAILS.STATUS
                },
                HIV_TEST = hivtestList,
                IMMUNIZATION = immunizationList,
                NEXT_OF_KIN = nextOfKinList,
                PATIENT_IDENTIFICATION =
                {
                    DATE_OF_BIRTH =entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH,//.ToString("yyyyMMdd"),
                    DATE_OF_BIRTH_PRECISION = entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION == "1" ? "ESTIMATED" :"EXACT",
                  //  DEATH_DATE = entity.PATIENT_IDENTIFICATION.DEATH_DATE,
                    
                    DEATH_INDICATOR = entity.PATIENT_IDENTIFICATION.DEATH_INDICATOR,
                    EXTERNAL_PATIENT_ID =
                    {
                        ASSIGNING_AUTHORITY = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY,
                        ASSIGNING_FACILITY = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_FACILITY,
                        ID = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID,
                        IDENTIFIER_TYPE = entity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE
                    },
                    INTERNAL_PATIENT_ID = internalIdentfierList,
                    MARITAL_STATUS = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS,
                    MOTHER_DETAILS =
                    {
                        MOTHER_IDENTIFIER =motherIdentifierList ,
                        MOTHER_NAME =
                        {
                            FIRST_NAME = entity.PATIENT_IDENTIFICATION.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME,
                            LAST_NAME = entity.PATIENT_IDENTIFICATION.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME,
                            MIDDLE_NAME = entity.PATIENT_IDENTIFICATION.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME
                        }
                    },PATIENT_ADDRESS =
                    {
                        PHYSICAL_ADDRESS =
                        {
                            COUNTY = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY,
                            NEAREST_LANDMARK = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK,
                            SUB_COUNTY = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY,
                            VILLAGE = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE,
                            WARD = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD
                        },
                        POSTAL_ADDRESS = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS
                    },PATIENT_NAME =
                    {
                        FIRST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                        LAST_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                        MIDDLE_NAME = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME
                    }
                }
            };

            return dtoShr;
        }

        public DtoShr GenerateDtoShr(SHR entity)
        {
            try
            {
                DtoShr clientDtoShr = new DtoShr();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SHR, DtoShr>().ReverseMap();
                    cfg.CreateMap<Entities.CCC.PSmart.PATIENTIDENTIFICATION, DTO.PSmart.PATIENTIDENTIFICATION>()
                        .ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.EXTERNALPATIENTID, DTO.PSmart.EXTERNALPATIENTID>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.INTERNALPATIENTID, DTO.PSmart.INTERNALPATIENTID>().ReverseMap()
                    .ForMember(x => x.PatientId, opt => opt.Ignore())
                    .ForMember(x => x.personId, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.PATIENTNAME, DTO.PSmart.PATIENTNAME>().ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());

                    cfg.CreateMap<Entities.CCC.PSmart.PHYSICALADDRESS, DTO.PSmart.PHYSICALADDRESS>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());

                    cfg.CreateMap<Entities.CCC.PSmart.PATIENTADDRESS, DTO.PSmart.PATIENTADDRESS>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.MOTHERDETAILS, DTO.PSmart.MOTHERDETAILS>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.MOTHERNAME, DTO.PSmart.MOTHERNAME>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.MOTHERIDENTIFIER, DTO.PSmart.MOTHERIDENTIFIER>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.NEXTOFKIN, DTO.PSmart.NEXTOFKIN>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.NOKNAME, DTO.PSmart.NOKNAME>().ReverseMap()
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.HIVTEST, DTO.PSmart.HIVTEST>().ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.PROVIDERDETAILS, DTO.PSmart.PROVIDERDETAILS>().ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.IMMUNIZATION, DTO.PSmart.IMMUNIZATION>().ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                    cfg.CreateMap<Entities.CCC.PSmart.CARDDETAILS, DTO.PSmart.CARDDETAILS>().ReverseMap()
                        .ForMember(x => x.PatientId, opt => opt.Ignore())
                        .ForMember(x => x.PersonId, opt => opt.Ignore())
                        .ForMember(x => x.CardSerialNumber, opt => opt.Ignore());
                });
                clientDtoShr = Mapper.Map<DtoShr>(entity);
                return clientDtoShr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}