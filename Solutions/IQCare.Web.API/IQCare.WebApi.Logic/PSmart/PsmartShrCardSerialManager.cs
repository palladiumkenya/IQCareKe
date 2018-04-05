using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.psmart;
using Interface.WebApi;
using IQCare.DTO.PSmart;
using IQCare.WebApi.Logic.DtoMapping;
using CARDDETAILS = Entities.CCC.psmart.CARDDETAILS;
using EXTERNALPATIENTID = Entities.CCC.psmart.EXTERNALPATIENTID;
using HIVTEST = Entities.CCC.psmart.HIVTEST;
using IMMUNIZATION = Entities.CCC.psmart.IMMUNIZATION;
using INTERNALPATIENTID = Entities.CCC.psmart.INTERNALPATIENTID;
using MOTHERDETAILS = Entities.CCC.psmart.MOTHERDETAILS;
using MOTHERIDENTIFIER = Entities.CCC.psmart.MOTHERIDENTIFIER;
using MOTHERNAME = Entities.CCC.psmart.MOTHERNAME;
using NEXTOFKIN = Entities.CCC.psmart.NEXTOFKIN;
using NOKNAME = Entities.CCC.psmart.NOKNAME;
using PATIENTADDRESS = Entities.CCC.psmart.PATIENTADDRESS;
using PATIENTIDENTIFICATION = Entities.CCC.psmart.PATIENTIDENTIFICATION;
using PATIENTNAME = Entities.CCC.psmart.PATIENTNAME;
using PHYSICALADDRESS = Entities.CCC.psmart.PHYSICALADDRESS;
using PROVIDERDETAILS = Entities.CCC.psmart.PROVIDERDETAILS;

namespace IQCare.WebApi.Logic.PSmart
{
    public class PsmartShrCardSerialManager
    {
        private readonly IBPsmartShrCardSerialManager _bPsmartShrCardSerialManager = (IBPsmartShrCardSerialManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPsmartShrCardSerialManager, BusinessProcess.WebApi");


        public EXTERNALPATIENTID GetExternalpatientid(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetExternalpatientid(cardSerial);
        }

        public List<INTERNALPATIENTID> GetInternalpatientids(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetInternalpatientids(cardSerial);
        }

        public PATIENTADDRESS GetPatientaddress(string cardSerial)
        {
            var patientAddress= _bPsmartShrCardSerialManager.GetPatientaddress(cardSerial);
            if (patientAddress != null){return patientAddress;}else {  return new PATIENTADDRESS();}
        }

        public CARDDETAILS GetPatientCarddetails(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientCarddetails(cardSerial);
        }

        public List<HIVTEST> GetPatientHivtest(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientHivtest(cardSerial);
        }

        public PATIENTIDENTIFICATION GetPatientidentification(string cardSerial)
        {

            return _bPsmartShrCardSerialManager.GetPatientidentification(cardSerial);

        }

        public List<IMMUNIZATION> GetPatientImmunization(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientImmunization(cardSerial);
        }

        public MOTHERDETAILS GetPatientMotherdetails(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientMotherdetails(cardSerial);
        }

        public List<MOTHERIDENTIFIER> GetPatientMotheridentifier(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientMotheridentifier(cardSerial);
        }

        public MOTHERNAME GetPatientMothername(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientMothername(cardSerial);
        }

        public PATIENTNAME GetPatientname(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientname(cardSerial);
        }

        public List<NEXTOFKIN> GetPatientNextofkin(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientNextofkin(cardSerial);
        }

        public NOKNAME GetPatientNokname(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientNokname(cardSerial);
        }

        public PHYSICALADDRESS GetPatientPhysicaladdress(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientPhysicaladdress(cardSerial);
           // if (physicalAddress != null) { return physicalAddress; } else { return new PHYSICALADDRESS(); }             
        }

        public PROVIDERDETAILS GetPatientProviderdetails(string cardSerial)
        {
            return _bPsmartShrCardSerialManager.GetPatientProviderdetails(cardSerial);
        }

        public DtoShr GenerateShrForEmr(string cardSerial)
        {
            //string Version = "VERSION\": \"1.0.0\"";
            var patientIdentification = GetPatientidentification(cardSerial);
            var carddetails = GetPatientCarddetails(cardSerial);
            List<IMMUNIZATION> immunizationSource = GetPatientImmunization(cardSerial);
            List<NEXTOFKIN> nextOfKinSouce = GetPatientNextofkin(cardSerial);
            List<HIVTEST> hivTestSource = GetPatientHivtest(cardSerial);

            var clientGeneratedShr = new SHR()
            {
                //VERSION = Version
                PATIENT_IDENTIFICATION = patientIdentification,
                NEXT_OF_KIN = nextOfKinSouce,
                HIV_TEST = hivTestSource,
                IMMUNIZATION = immunizationSource,
                CARD_DETAILS = carddetails
            };

            DtoMapper dtoMapper = new DtoMapper();
            DtoShr clientGeneratedDtoShr = new DtoShr();


            clientGeneratedDtoShr = dtoMapper.GenerateDtoShr(clientGeneratedShr);


            return clientGeneratedDtoShr;

        }

        public DtoShr GenerateShrForEmrUsingCardSerial(string cardSerial)
        {
            //string Version = "VERSION\": \"1.0.0\"";
            var patientIdentification = GetPatientidentification(cardSerial);
            var carddetails = GetPatientCarddetails(cardSerial);
            List<IMMUNIZATION> immunizationSource = GetPatientImmunization(cardSerial);
            List<NEXTOFKIN> nextOfKinSouce = GetPatientNextofkin(cardSerial);
            List<HIVTEST> hivTestSource = GetPatientHivtest(cardSerial);

            var clientGeneratedShr = new SHR()
            {
                //VERSION = Version
                PATIENT_IDENTIFICATION = patientIdentification,
                NEXT_OF_KIN = nextOfKinSouce,
                HIV_TEST = hivTestSource,
                IMMUNIZATION = immunizationSource,
                CARD_DETAILS = carddetails
            };

            DtoMapper dtoMapper = new DtoMapper();
            DtoShr clientGeneratedDtoShr = new DtoShr();


            clientGeneratedDtoShr = dtoMapper.GenerateDtoShr(clientGeneratedShr);


            return clientGeneratedDtoShr;

        }
    }
}