using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;
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
using IQCare.DTO.ObservationResult;
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
            

            //var orderEncorder = new List<PharmacyEncorderOrderEntity>();


            DrugPrescriptionEntity raisedPrescriptionEntity = new DrugPrescriptionEntity();

            raisedPrescriptionEntity.MESSAGE_HEADER.SENDING_FACILITY = entityDto.MESSAGE_HEADER.SENDING_FACILITY;
            raisedPrescriptionEntity.MESSAGE_HEADER.MESSAGE_DATETIME =entityDto.MESSAGE_HEADER.MESSAGE_DATETIME.ToString("yyyyMMddHmmss");
            raisedPrescriptionEntity.MESSAGE_HEADER.MESSAGE_TYPE = entityDto.MESSAGE_HEADER.MESSAGE_TYPE;
            raisedPrescriptionEntity.MESSAGE_HEADER.PROCESSING_ID = entityDto.MESSAGE_HEADER.PROCESSING_ID;
            raisedPrescriptionEntity.MESSAGE_HEADER.RECEIVING_APPLICATION = entityDto.MESSAGE_HEADER.RECEIVING_APPLICATION;
            raisedPrescriptionEntity.MESSAGE_HEADER.RECEIVING_FACILITY = entityDto.MESSAGE_HEADER.RECEIVING_FACILITY;
            raisedPrescriptionEntity.MESSAGE_HEADER.SECURITY = entityDto.MESSAGE_HEADER.SECURITY;
            raisedPrescriptionEntity.MESSAGE_HEADER.SENDING_APPLICATION = entityDto.MESSAGE_HEADER.SENDING_APPLICATION;

            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID =entityDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID;
            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE =entityDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE;
            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = entityDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY;

            //var internalIdentifiers = new List<INTERNALPATIENTID>();

            foreach (var identifier in entityDto.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
                var internalIdentity = new INTERNALPATIENTID()
                {
                    IDENTIFIER_TYPE = identifier.IDENTIFIER_TYPE,
                    ASSIGNING_AUTHORITY = identifier.ASSIGNING_AUTHORITY,
                    ID = identifier.ID
                };
                raisedPrescriptionEntity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalIdentity);
                //internalIdentifiers.Add(internalIdentity);
            }
            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME =entityDto.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME =entityDto.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
            raisedPrescriptionEntity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = entityDto.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;

            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.OrderingPhysicianEntity.FIRST_NAME =entityDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.FIRST_NAME;
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.OrderingPhysicianEntity.MIDDLE_NAME = entityDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.MIDDLE_NAME;
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.OrderingPhysicianEntity.LAST_NAME = entityDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.LAST_NAME;

            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.NOTES = entityDto.COMMON_ORDER_DETAILS.NOTES;
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.ORDER_CONTROL = entityDto.COMMON_ORDER_DETAILS.ORDER_CONTROL;
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.ORDER_STATUS = entityDto.COMMON_ORDER_DETAILS.ORDER_STATUS;
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.PlacerOrderNumberEntity.NUMBER = Convert.ToString(entityDto.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER);
            raisedPrescriptionEntity.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME = entityDto.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME.ToString("yyyyMMddHmmss");

            foreach (var order in entityDto.PHARMACY_ENCODED_ORDER)
            {
                var prescriptionOrder = new PharmacyEncorderOrderEntity()
                {
                    DRUG_NAME = order.DRUG_NAME,
                    CODING_SYSTEM = order.CODING_SYSTEM,
                    STRENGTH = order.STRENGTH,
                    DOSAGE = Convert.ToDecimal(order.DOSAGE),
                    FREQUENCY = order.FREQUENCY,
                    DURATION = order.DURATION,
                    QUANTITY_PRESCRIBED =Convert.ToDecimal(order.QUANTITY_PRESCRIBED),
                    TREATMENT_INSTRUCTION = order.TREATMENT_INSTRUCTION
                    
                };
                raisedPrescriptionEntity.PHARMACY_ENCODED_ORDER.Add(prescriptionOrder);
            }
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
                cfg.CreateMap<DTO.CommonEntities.APPOINTMENTPATIENTIDENTIFICATION, MappingEntities.APPOINTMENTPATIENTIDENTIFICATION>().ReverseMap();
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

        public ObservationResultEntity ObservationResult(ObservationResultDTO observation, MessageEventArgs messageEvent)
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