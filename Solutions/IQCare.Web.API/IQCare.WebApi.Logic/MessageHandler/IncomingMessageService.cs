﻿using AutoMapper;
using Entity.WebApi;
using Interface.WebApi;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.DTO.PatientRegistration;
using IQCare.WebApi.Logic.DtoMapping;
using IQCare.WebApi.Logic.MappingEntities;
using System;
using System.Web.Script.Serialization;
using IQCare.DTO;
using IQCare.DTO.CommonEntities;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public class IncomingMessageService : IIncomingMessageService
    {
        private readonly IApiInboxManager _apiInboxmanager;
        private readonly IDtoMapper _dtoMapper;

        public IncomingMessageService()
        {
            _apiInboxmanager = new ApiInboxManager();
            _dtoMapper = new DtoMapper();
        }

        public IncomingMessageService(IApiInboxManager apiInboxmanager, IDtoMapper dtoMapper)
        {
            _apiInboxmanager = apiInboxmanager;
            _dtoMapper = dtoMapper;
        }

        public void Handle(string messageType, string message)
        {
            var apiInbox = new ApiInbox()
            {
                DateReceived = DateTime.Now,
                Message = message,
                //Todo get sender Id from interop table
                SenderId = 1
            };


            switch (messageType)
            {
                case "ADT^A04":
                    HandleNewClientRegistration(apiInbox);
                    break;

                case "ADT^A08":
                    HandleUpdatedClientInformation(apiInbox);
                    break;

                case "RDE^001 ":
                    HandleDrugPrescriptionRaised(apiInbox);
                    break;

                case "RDS^O13":
                    HandleDrugOrderFulfilment(apiInbox);
                    break;

                case "SIU^S12":
                    HandleAppointments(apiInbox);
                    break;

                case "ORU^R01":
                    HandleNewViralLoadResults(apiInbox);
                    break;
            }
        }

        private void HandleNewClientRegistration(ApiInbox incomingMessage)
        {
            try
            {
                PatientRegistrationEntity entity =new  JavaScriptSerializer().Deserialize<PatientRegistrationEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg => {
                    cfg.CreateMap<PatientRegistrationDTO, PatientRegistrationEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTIDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NEXTOFKIN, MappingEntities.NEXTOFKIN>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.VISIT, MappingEntities.VISIT>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTADDRESS, MappingEntities.PATIENTADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PHYSICAL_ADDRESS, MappingEntities.PHYSICALADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NOKNAME, MappingEntities.NOKNAME>().ReverseMap();
                });
                var register = Mapper.Map<PatientRegistrationDTO>(entity);

                var processRegistration = new ProcessRegistration();
                processRegistration.Save(register);

                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.AddApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
                Console.WriteLine(e);
                throw;
            }
        }

        private void HandleUpdatedClientInformation(ApiInbox incomingMessage)
        {
            try
            {
                PatientRegistrationEntity entity = new JavaScriptSerializer().Deserialize<PatientRegistrationEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg => {
                    cfg.CreateMap<PatientRegistrationDTO, PatientRegistrationEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTIDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NEXTOFKIN, MappingEntities.NEXTOFKIN>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.VISIT, MappingEntities.VISIT>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTADDRESS, MappingEntities.PATIENTADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PHYSICAL_ADDRESS, MappingEntities.PHYSICALADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NOKNAME, MappingEntities.NOKNAME>().ReverseMap();
                });
                var register = Mapper.Map<PatientRegistrationDTO>(entity);
                var processRegistration = new ProcessRegistration();
                processRegistration.Update(register);

                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.AddApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
            }
        }

        private void HandleDrugPrescriptionRaised(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleDrugOrderFulfilment(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleAppointments(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleNewViralLoadResults(ApiInbox incomingMessage)
        {
            try
            {
                ViralLoadResultEntity entity = new JavaScriptSerializer().Deserialize<ViralLoadResultEntity>(incomingMessage.Message);
                ViralLoadResultsDto vlResultsDto = _dtoMapper.ViralLoadResults(entity);
                var processViralLoadResults = new ProcessViralLoadResults();
                var msg = processViralLoadResults.Save(vlResultsDto);
                incomingMessage.LogMessage = msg;
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
                Console.WriteLine(e);
                throw;
            }
            incomingMessage.DateProcessed = DateTime.Now;
            incomingMessage.Processed = true;
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }
    }
}