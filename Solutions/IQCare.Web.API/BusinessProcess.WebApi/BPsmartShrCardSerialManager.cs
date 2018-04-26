using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BPsmartShrCardSerialManager :ProcessBase, IBPsmartShrCardSerialManager
    {

        public EXTERNALPATIENTID GetExternalpatientid(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var externalPatientId = unitOfWork.ExternalPatientIdRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new EXTERNALPATIENTID()
                    {
                        ID = x.ID,
                        ASSIGNING_AUTHORITY = x.ASSIGNING_AUTHORITY,
                        IDENTIFIER_TYPE = x.IDENTIFIER_TYPE,
                        ASSIGNING_FACILITY = x.ASSIGNING_FACILITY,
                        PersonId = x.PersonId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();


                    //EXTERNALPATIENTID newExternalObject=new EXTERNALPATIENTID()
                    //{
                    //    ASSIGNING_AUTHORITY = "",
                    //    ID = "",ASSIGNING_FACILITY = "",CardSerialNumber = ""
                        
                    //};
                    //return newExternalObject;
                    return externalPatientId==null?new EXTERNALPATIENTID() : externalPatientId;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<INTERNALPATIENTID> GetInternalpatientids(string cardSerial)
        {
            try
            {
                List<INTERNALPATIENTID> internalpatientid = new List<INTERNALPATIENTID>();
                //INTERNALPATIENTID cardDetails = new INTERNALPATIENTID()
                //{
                //    ID = "12345678-ADFGHJY-0987654-NHYI890",
                //    IDENTIFIER_TYPE = "CARD_SERIAL_NUMBER",
                //    ASSIGNING_AUTHORITY = "CARD_REGISTRY",
                //    ASSIGNING_FACILITY = "1089"
                //};

                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    internalpatientid = unitOfWork.InternalPatientIdRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new INTERNALPATIENTID()
                    {
                        ID = x.ID,
                        ASSIGNING_AUTHORITY = x.ASSIGNING_AUTHORITY,
                        ASSIGNING_FACILITY = x.ASSIGNING_FACILITY,
                        IDENTIFIER_TYPE = x.IDENTIFIER_TYPE,
                        personId = x.personId,
                        PatientId = x.PatientId
                    }).ToList();
                    unitOfWork.Dispose();

                }
              //  internalpatientid.Add(cardDetails);
                return internalpatientid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTADDRESS GetPatientaddress(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var physicalAddress = this.GetPatientPhysicaladdress(cardSerial);
                    var patientAddress = unitOfWork.PatientAddressRepository.FindBy(x => x.CardSerialNumber == cardSerial ).Select(x => new PATIENTADDRESS()
                    {
                        PHYSICAL_ADDRESS = physicalAddress,
                        POSTAL_ADDRESS = x.POSTAL_ADDRESS,
                        PersonId = x.PersonId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return patientAddress;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CARDDETAILS GetPatientCarddetails(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var cardDetails = unitOfWork.CardDetailsRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new CARDDETAILS()
                    {
                        STATUS = x.STATUS,
                        REASON = x.REASON,
                        LAST_UPDATED_FACILITY = x.LAST_UPDATED_FACILITY,
                        LAST_UPDATED = x.LAST_UPDATED,
                        PersonId = x.PersonId,
                        PatientId = x.PatientId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return cardDetails;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<HIVTEST> GetPatientHivtest(string cardSerial)
        {
            try
            {
                List<HIVTEST> hivTest;
                var providerDeatils = this.GetPatientProviderdetails(cardSerial);
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    hivTest = unitOfWork.HivTestRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new HIVTEST()
                    {
                        DATE = x.DATE,
                        FACILITY = x.FACILITY,
                        PROVIDER_DETAILS = providerDeatils,
                        RESULT = x.RESULT,
                        TYPE = x.TYPE,
                        STRATEGY = x.STRATEGY,
                        PersonId = x.PersonId,
                        PatientId = x.PatientId
                    }).ToList();
                    unitOfWork.Dispose();
                }

                return hivTest;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTIDENTIFICATION GetPatientidentification(string cardSerial)
        {
            try
            {
                var patientName = this.GetPatientname(cardSerial);
                var externalPatientId = this.GetExternalpatientid(cardSerial);
                var internalPatientId = this.GetInternalpatientids(cardSerial);
                var patientAddress = this.GetPatientaddress(cardSerial);
                var motherDetails = this.GetPatientMotherdetails(cardSerial);

                PATIENTIDENTIFICATION patientIdentification = new PATIENTIDENTIFICATION();

                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    patientIdentification = unitOfWork.PatientIdentificationRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new PATIENTIDENTIFICATION()
                    {
                        DATE_OF_BIRTH_PRECISION = x.DATE_OF_BIRTH_PRECISION,
                        DATE_OF_BIRTH = x.DATE_OF_BIRTH,
                        DEATH_DATE = x.DEATH_DATE,
                        DEATH_INDICATOR = x.DEATH_INDICATOR,
                        EXTERNAL_PATIENT_ID = externalPatientId,
                        INTERNAL_PATIENT_ID = internalPatientId,
                        MARITAL_STATUS = x.MARITAL_STATUS,
                        MOTHER_DETAILS = motherDetails,
                        PATIENT_ADDRESS = patientAddress,
                        PATIENT_NAME = patientName,
                        PHONE_NUMBER = x.PHONE_NUMBER,
                        SEX = x.SEX
                    }).FirstOrDefault();
                    unitOfWork.Dispose();

                }
                //  patientIdentification.EXTERNAL_PATIENT_ID = externalPatientId;
                //patientIdentification.INTERNAL_PATIENT_ID = internalPatientId;
                //patientIdentification.PATIENT_NAME = patientName;
                //patientIdentification.PATIENT_ADDRESS = patientAddress;
                return patientIdentification;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IMMUNIZATION> GetPatientImmunization(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var immunization = unitOfWork.ImmunizationRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new IMMUNIZATION()
                    {
                        DATE_ADMINISTERED = x.DATE_ADMINISTERED,
                        NAME = x.NAME,
                        PersonId = x.PersonId,
                        PatientId = x.PatientId

                    }).ToList();
                    unitOfWork.Dispose();
                    return immunization;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MOTHERDETAILS GetPatientMotherdetails(string cardSerial)
        {
            try
            {
                MOTHERDETAILS motherDetails;
                var motherName = this.GetPatientMothername(cardSerial);
                var motherIdentifier = this.GetPatientMotheridentifier(cardSerial);

                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    motherDetails = unitOfWork.MotherDetailsRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new MOTHERDETAILS()
                    {
                        MOTHER_NAME = motherName,
                        MOTHER_IDENTIFIER = motherIdentifier,
                        PersonId = x.PersonId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                }
                // motherDetails.MOTHER_IDENTIFIER = motherIdentifier;
                //motherDetails.MOTHER_NAME = motherName;
                return motherDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MOTHERIDENTIFIER> GetPatientMotheridentifier(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherIdentifier = unitOfWork.MotherIdentifierRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new MOTHERIDENTIFIER()
                    {
                        ASSIGNING_AUTHORITY = x.ASSIGNING_AUTHORITY,
                        ASSIGNING_FACILITY = x.ASSIGNING_FACILITY,
                        ID = x.ID,
                        PersonId = x.PersonId,
                        IDENTIFIER_TYPE = x.IDENTIFIER_TYPE
                    }).ToList();
                    unitOfWork.Dispose();
                    return motherIdentifier;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MOTHERNAME GetPatientMothername(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherName = unitOfWork.MotherNameRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new MOTHERNAME()
                    {
                        FIRST_NAME = x.FIRST_NAME,
                        LAST_NAME = x.LAST_NAME,
                        MIDDLE_NAME = x.MIDDLE_NAME,
                        PersonId = x.PersonId

                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return motherName;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTNAME GetPatientname(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var patientName = unitOfWork.PatientNameRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new PATIENTNAME()
                    {
                        FIRST_NAME = x.FIRST_NAME,
                        LAST_NAME = x.LAST_NAME,
                        MIDDLE_NAME = x.MIDDLE_NAME,
                        PatientId = x.PatientId,
                        PersonId = x.PersonId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return patientName;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NEXTOFKIN> GetPatientNextofkin(string cardSerial)
        {
            try
            {
                var nokNameDetails = this.GetPatientNokname(cardSerial);

                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var nextOfKin = unitOfWork.NextOfKinRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new NEXTOFKIN()
                    {
                        ADDRESS = x.ADDRESS,
                        CONTACT_ROLE = x.CONTACT_ROLE,
                        DATE_OF_BIRTH = x.DATE_OF_BIRTH,
                        NOK_NAME = nokNameDetails,
                        PHONE_NUMBER = x.PHONE_NUMBER,
                        RELATIONSHIP = x.RELATIONSHIP,
                        SEX = x.SEX,
                        PersonId = x.PersonId
                    }).ToList();
                    unitOfWork.Dispose();
                    return nextOfKin;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOKNAME GetPatientNokname(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var nokName = unitOfWork.NokNameRepository.FindBy(x => x.CardSerialNumber==cardSerial).Select(x => new NOKNAME()
                    {
                        FIRST_NAME = x.FIRST_NAME,
                        LAST_NAME = x.LAST_NAME,
                        MIDDLE_NAME = x.MIDDLE_NAME,
                        PersonId = x.PersonId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return nokName;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PHYSICALADDRESS GetPatientPhysicaladdress(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var physicalAddress = unitOfWork.PhysicalAddressRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new PHYSICALADDRESS()
                    {
                        COUNTY = x.COUNTY,
                        NEAREST_LANDMARK = x.NEAREST_LANDMARK,
                        SUB_COUNTY = x.SUB_COUNTY,
                        VILLAGE = x.VILLAGE,
                        WARD = x.WARD,
                        PersonId = x.PersonId,
                        PatientId = x.PatientId
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return physicalAddress;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PROVIDERDETAILS GetPatientProviderdetails(string cardSerial)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var providerDetails = unitOfWork.ProviderDetailsRepository.FindBy(x => x.CardSerialNumber == cardSerial).Select(x => new PROVIDERDETAILS()
                    {
                        NAME = x.NAME,
                        ID = x.ID
                    }).FirstOrDefault();
                    unitOfWork.Dispose();
                    return providerDetails;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}