using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Application.Presentation;
using AutoMapper;
using Entities.CCC.PSmart;
using Interface.WebApi;
using IQCare.DTO.PSmart;
using IQCare.WebApi.Logic.DtoMapping;
using IQCare.WebApi.Logic.EntityMapper;
using IQCare.WebApi.Logic.MappingEntities.PSmart;
using CARDDETAILS = Entities.CCC.PSmart.CARDDETAILS;
using EXTERNALPATIENTID = Entities.CCC.PSmart.EXTERNALPATIENTID;
using HIVTEST = Entities.CCC.PSmart.HIVTEST;
using IMMUNIZATION = Entities.CCC.PSmart.IMMUNIZATION;
using INTERNALPATIENTID = Entities.CCC.PSmart.INTERNALPATIENTID;
using MOTHERDETAILS = Entities.CCC.PSmart.MOTHERDETAILS;
using MOTHERIDENTIFIER = Entities.CCC.PSmart.MOTHERIDENTIFIER;
using MOTHERNAME = Entities.CCC.PSmart.MOTHERNAME;
using NEXTOFKIN = Entities.CCC.PSmart.NEXTOFKIN;
using NOKNAME = Entities.CCC.PSmart.NOKNAME;
using PATIENTADDRESS = Entities.CCC.PSmart.PATIENTADDRESS;
using PATIENTIDENTIFICATION = Entities.CCC.PSmart.PATIENTIDENTIFICATION;
using PATIENTNAME = Entities.CCC.PSmart.PATIENTNAME;
using PHYSICALADDRESS = Entities.CCC.PSmart.PHYSICALADDRESS;
using PROVIDERDETAILS = Entities.CCC.PSmart.PROVIDERDETAILS;

namespace IQCare.WebApi.Logic.PSmart
{
   public class PsmartShrManager 
    {
        private readonly IPsmartShrManager  _pSmartShrManager = (IPsmartShrManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPsmartShrManager, BusinessProcess.WebApi");
      //  private readonly IShrVersionManager _shrVersionManager  = (IShrVersionManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BShrVersion, BusinessProcess.WebApi");


        public EXTERNALPATIENTID GetExternalpatientid(int personId)
        {
            return _pSmartShrManager.GetExternalpatientid(personId);
        }

        public List<INTERNALPATIENTID> GetInternalpatientids(int personId)
        {
            return _pSmartShrManager.GetInternalpatientids(personId);
        }

        public PATIENTADDRESS GetPatientaddress(int personId)
        {
            var patientAddress= _pSmartShrManager.GetPatientaddress(personId);
            if (patientAddress != null)
            {
                return patientAddress;
            }
            else
            {
                return new PATIENTADDRESS();
            }

        }

        public CARDDETAILS GetPatientCarddetails(int personId)
        {
            return _pSmartShrManager.GetPatientCarddetails(personId);
        }

        public List<HIVTEST>  GetPatientHivtest(int personId)
        {
            return _pSmartShrManager.GetPatientHivtest(personId);
        }

        public PATIENTIDENTIFICATION GetPatientidentification(int personId)
        {

            return _pSmartShrManager.GetPatientidentification(personId);
   
        }

        public List<IMMUNIZATION>  GetPatientImmunization(int personId)
        {
            return _pSmartShrManager.GetPatientImmunization(personId);
        }

        public MOTHERDETAILS GetPatientMotherdetails(int personId)
        {
           var motherDetails=  _pSmartShrManager.GetPatientMotherdetails(personId);
            if (motherDetails != null){return motherDetails;}else{return new MOTHERDETAILS();}
        }

        public List<MOTHERIDENTIFIER> GetPatientMotheridentifier(int personId)
        {
            return _pSmartShrManager.GetPatientMotheridentifier(personId);
        }

        public MOTHERNAME GetPatientMothername(int personId)
        {
            var motherName= _pSmartShrManager.GetPatientMothername(personId);
            if (motherName != null){return motherName;}else{return new MOTHERNAME();}
        }

        public PATIENTNAME GetPatientname(int personId)
        {
            return _pSmartShrManager.GetPatientname(personId);
        }

        public List<NEXTOFKIN>  GetPatientNextofkin(int personId)
        {
            return _pSmartShrManager.GetPatientNextofkin(personId);
        }

        public NOKNAME GetPatientNokname(int personId)
        {
            return _pSmartShrManager.GetPatientNokname(personId);
        }

        public PHYSICALADDRESS GetPatientPhysicaladdress(int personId)
        {
            return _pSmartShrManager.GetPatientPhysicaladdress(personId);
        }

        public PROVIDERDETAILS GetPatientProviderdetails(int personId)
        {
            return _pSmartShrManager.GetPatientProviderdetails(personId);
        }

        public DtoShr GenerateShrForEmr(int patientId)
        {
            //string Version = "VERSION\": \"1.0.0\"";
            var patientIdentification = GetPatientidentification(patientId);
            var carddetails = GetPatientCarddetails(patientId);
            List<IMMUNIZATION> immunizationSource = GetPatientImmunization(patientId);
            List<NEXTOFKIN> nextOfKinSouce = GetPatientNextofkin(patientId);
            List<HIVTEST> hivTestSource = GetPatientHivtest(patientId);

            var clientGeneratedShr=new SHR()
            {
               //VERSION = Version
                PATIENT_IDENTIFICATION = patientIdentification,
                NEXT_OF_KIN = nextOfKinSouce,
                HIV_TEST = hivTestSource,
                IMMUNIZATION = immunizationSource,
                CARD_DETAILS = carddetails
            };

            DtoMapper dtoMapper=new DtoMapper();
            DtoShr clientGeneratedDtoShr=new DtoShr();


            clientGeneratedDtoShr = dtoMapper.GenerateDtoShr(clientGeneratedShr);    

            
             return clientGeneratedDtoShr;

        }

        public DtoShr GenerateShrForEmrUsingCardSerial(int patientId)
        {
            //string Version = "VERSION\": \"1.0.0\"";
            var patientIdentification = GetPatientidentification(patientId);
            var carddetails = GetPatientCarddetails(patientId);
            List<IMMUNIZATION> immunizationSource = GetPatientImmunization(patientId);
            List<NEXTOFKIN> nextOfKinSouce = GetPatientNextofkin(patientId);
            List<HIVTEST> hivTestSource = GetPatientHivtest(patientId);

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
