using System.Collections.Generic;
using System;
using IQCare.DTO;
using IQCare.Events;
using IQCare.WebApi.Logic.MappingEntities;
using Newtonsoft.Json;
using IQCare.DTO.PatientRegistration;
using AutoMapper;
using IQCare.DTO.CommonEntities;
using IQCare.WebApi.Logic.MappingEntities.drugs;
using MESSAGEHEADER = IQCare.WebApi.Logic.MappingEntities.MESSAGEHEADER;
using PATIENTIDENTIFICATION = IQCare.WebApi.Logic.MappingEntities.PATIENTIDENTIFICATION;
using IQCare.DTO.PatientAppointment;
using INTERNALPATIENTID = IQCare.WebApi.Logic.MappingEntities.INTERNALPATIENTID;

namespace IQCare.WebApi.Logic.EntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper
    {
        public PatientRegistrationEntity PatientRegistration(PatientRegistrationDTO entity, MessageEventArgs messageEvent)
        {
            PatientRegistrationEntity patientRegistration = new PatientRegistrationEntity();

            string messageType = null;
            if (messageEvent.MessageType == MessageType.NewClientRegistration)
            {
                messageType = "ADT^A04";
            }
            else if (messageEvent.MessageType == MessageType.UpdatedClientInformation)
            {
                messageType = "ADT^A08";
            }

            int facilityId = messageEvent.FacilityId;

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

            patientRegistration = Mapper.Map<PatientRegistrationEntity>(entity);

            patientRegistration.MESSAGE_HEADER = GetMessageHeader(messageType, facilityId.ToString(), "P");
            return patientRegistration;
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

        public DrugPrescriptionEntity DrugPrescriptionRaised(PrescriptionDto entityDto)
        {
            var internalIdentifiers = new List<INTERNALPATIENTID>();

            foreach (var identifier in entityDto.PatientIdentification.InternalPatientId)
            {
                var internalIdentity = new INTERNALPATIENTID()
                {
                    IDENTIFIER_TYPE =identifier.IdentifierType,
                    ASSIGNING_AUTHORITY = identifier.AssigningAuthority,
                    ID = identifier.IdentifierValue
                };
                internalIdentifiers.Add(internalIdentity);
            }

            var orderEncorder = new List<PharmacyEncorderOrderEntity>();

            foreach (var order in entityDto.PharmacyEncodedOrderDto)
            {
                var prescriptionOrder = new PharmacyEncorderOrderEntity()
                {
                    DrugName = order.DrugName,
                    CodingSystem = order.CodingSystem,
                    Strength = order.Strength,
                    Dosage = order.Dosage,
                    Frequency = order.Frequency,
                    Duration = order.Duration,
                    QuantityPrescribed = order.QuantityPrescribed,
                    PrescriptionNotes = order.PrescriptionNotes
                };
                orderEncorder.Add(prescriptionOrder);
            }


            DrugPrescriptionEntity raisedPrescriptionEntity =new DrugPrescriptionEntity()
            {
                
                MessageHeaderEntity = 
                {
                    MessageType = entityDto.MesssageHeader.MessageType,
                    MessageDatetime = DateTime.Now.ToString("yyyyMMddHmmss"),
                    ProcessingId = entityDto.MesssageHeader.ProcessingId,
                    ReceivingApplication = entityDto.MesssageHeader.ReceivingApplication,
                    ReceivingFacility = entityDto.MesssageHeader.ReceivingFacility,
                    Security = entityDto.MesssageHeader.Security,
                    SendingApplication = entityDto.MesssageHeader.SendingApplication,
                    SendingFacility = entityDto.MesssageHeader.SendingFacility
                },
                CommonOrderDetailsEntity = 
                {
                    OrderControl = entityDto.CommonOrderDetails.OrderControl,
                    PlacerOrderNumberEntity = 
                    {
                        Number = entityDto.CommonOrderDetails.PlacerOrderNumberDto.Number.ToString(),
                        Entity = entityDto.CommonOrderDetails.PlacerOrderNumberDto.Entity
                    },
                    OrderStatus = entityDto.CommonOrderDetails.OrderStatus,
                    OrderingPhysicianEntity = 
                    {
                        FirstName = entityDto.CommonOrderDetails.OrderingPhysicianDto.FirstName,
                        MiddleName = entityDto.CommonOrderDetails.OrderingPhysicianDto.MiddleName,
                        LastName = entityDto.CommonOrderDetails.OrderingPhysicianDto.LastName
                    },
                    TransactionDatetime = entityDto.CommonOrderDetails.TransactionDatetime.ToShortDateString(),
                    Notes = entityDto.CommonOrderDetails.Notes
                },
                Patientidentification = 
                {
                    EXTERNAL_PATIENT_ID =
                    {
                        ASSIGNING_AUTHORITY = entityDto.PatientIdentification.ExternalPatientId.AssigningAuthority,
                        ID = entityDto.PatientIdentification.ExternalPatientId.IdentifierValue,
                        IDENTIFIER_TYPE = entityDto.PatientIdentification.ExternalPatientId.AssigningAuthority
                    },
                    INTERNAL_PATIENT_ID =internalIdentifiers,
                    PATIENT_NAME =
                    {
                        FIRST_NAME = entityDto.PatientIdentification.PatientName.FirstName,
                        MIDDLE_NAME = entityDto.PatientIdentification.PatientName.MiddleName,
                        LAST_NAME = entityDto.PatientIdentification.PatientName.LastName
                    }
                },
                PharmacyEncorderEntity = orderEncorder
            };
            return raisedPrescriptionEntity;

        }

        public void DrugOrderCancel()
        {
            throw new System.NotImplementedException();
        }

        public string DrugOrderFulfilment()
        {
            throw new System.NotImplementedException();
        }

        public PatientAppointmentEntity AppointmentScheduling(PatientAppointSchedulingDTO appointment, MessageEventArgs messageEvent)
        {
            PatientAppointmentEntity entity = new PatientAppointmentEntity();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PatientAppointSchedulingDTO, PatientAppointmentEntity>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.PATIENTIDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.APPOINTMENT_INFORMATION, MappingEntities.APPOINTMENT_INFORMATION>().ReverseMap();
                cfg.CreateMap<DTO.CommonEntities.PLACER_APPOINTMENT_NUMBER, MappingEntities.PLACER_APPOINTMENT_NUMBER>().ReverseMap();
            });

            entity = Mapper.Map<PatientAppointmentEntity>(appointment);
            entity.MESSAGE_HEADER = GetMessageHeader("SIU^S12", messageEvent.FacilityId.ToString(), "P");
            return entity;
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

        //object IJsonEntityMapper.DrugPrescriptionRaised(PrescriptionDto drugOrderDto)
        //{
        //    return DrugPrescriptionRaised(drugOrderDto);
        //}

        private MESSAGEHEADER GetMessageHeader(string messageType, string sendingFacility, string processingId)
        {
            return new MESSAGEHEADER()
            {
                MESSAGE_TYPE = messageType,
                MESSAGE_DATETIME = DateTime.Now.ToString("yyyyMMddHmmss"),
                PROCESSING_ID = processingId,
                RECEIVING_APPLICATION = "IL",
                RECEIVING_FACILITY = "",
                SECURITY = "",
                SENDING_APPLICATION = "IQCARE",
                SENDING_FACILITY = sendingFacility
            };
        }

        string IJsonEntityMapper.DrugOrderFulfilment()
        {
            throw new NotImplementedException();
        }

       /* public DrugPrescriptionEntity DrugPrescriptionRaised(List<PrescriptionDto> prescription)
        {
            throw new NotImplementedException();
        }*/

     /*   object IJsonEntityMapper.DrugPrescriptionRaised(PrescriptionDto drugOrderDto)
        {
            throw new NotImplementedException();
        }*/
    }
}