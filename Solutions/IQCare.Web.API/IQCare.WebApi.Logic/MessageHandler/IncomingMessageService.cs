using AutoMapper;
using Entity.WebApi;
using Interface.WebApi;
using IQCare.WebApi.Logic.DtoMapping;
using IQCare.WebApi.Logic.MappingEntities;
using System;
using System.Web.Script.Serialization;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.CCC.UILogic.Interoperability.Appointment;
using IQCare.CCC.UILogic.Interoperability.Enrollment;
using IQCare.DTO;
using IQCare.DTO.PatientAppointment;
using IQCare.DTO.PatientRegistration;

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

                case "ORU^VL":
                    HandleNewViralLoadResults(apiInbox);
                    break;
            }
        }

        private void HandleNewClientRegistration(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

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

                //update message set processed=1, erromsq=null
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);        
            }
            catch (Exception e)
            {
                //update message set processed=1, erromsq
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleUpdatedClientInformation(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

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
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleDrugPrescriptionRaised(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleDrugOrderFulfilment(ApiInbox incomingMessage)
        {
            try
            {
                DrugDispenseEntity drugDispenseEntity = new JavaScriptSerializer().Deserialize<DrugDispenseEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DtoDrugDispensed, DrugDispenseEntity>().ReverseMap();
                    cfg.CreateMap<DTO.MESSAGE_HEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.PATIENT_IDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.COMMON_ORDER_DETAILS, MappingEntities.CommonOrderDetailsDispenseEntity>().ReverseMap();
                    cfg.CreateMap<DTO.PharmacyDispensedDrugs, MappingEntities.PHARMACY_ENCODED_ORDER_DISPENSE>().ReverseMap();
                });
                var dispensedPayload = Mapper.Map<PharmacyDispensedDrugs>(drugDispenseEntity);
                // todo process the new dispense.

            }
            catch(Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
            }
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandleAppointments(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                PatientAppointmentEntity appointmentEntity = new JavaScriptSerializer().Deserialize<PatientAppointmentEntity>(incomingMessage.Message);

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PatientAppointSchedulingDTO, PatientAppointmentEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.APPOINTMENTPATIENTIDENTIFICATION, MappingEntities.APPOINTMENTPATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.APPOINTMENT_INFORMATION, MappingEntities.APPOINTMENT_INFORMATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PLACER_APPOINTMENT_NUMBER, MappingEntities.PLACER_APPOINTMENT_NUMBER>().ReverseMap();
                });
                var appointment = Mapper.Map<PatientAppointSchedulingDTO>(appointmentEntity);
                var processAppoinment = new ProcessPatientAppointmentMessage();
                foreach (var itemAppointment in appointment.APPOINTMENT_INFORMATION){
                    if (itemAppointment.ACTION_CODE == "A")
                    {
                        processAppoinment.Save(appointment);
                    }
                    else if (itemAppointment.ACTION_CODE=="U" || itemAppointment.ACTION_CODE == "D")
                    {
                        processAppoinment.Update(appointment);
                    }
                }

                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleNewViralLoadResults(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                ViralLoadResultEntity entity = new JavaScriptSerializer().Deserialize<ViralLoadResultEntity>(incomingMessage.Message);
                ViralLoadResultsDto vlResultsDto = _dtoMapper.ViralLoadResults(entity);
                var processViralLoadResults = new ProcessViralLoadResults();
                var msg = processViralLoadResults.Save(vlResultsDto);

                incomingMessage.LogMessage = msg;
                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            //incomingMessage.DateProcessed = DateTime.Now;
            //incomingMessage.Processed = true;
            //_apiInboxmanager.AddApiInbox(incomingMessage);
        }
    }
}