using System.Collections.Generic;
using System;
using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities;
using Newtonsoft.Json;

namespace IQCare.WebApi.Logic.EntityMapper
{
    public class JsonEntityMapper : IJsonEntityMapper

    {
        public PatientRegistrationEntity PatientRegistration(Registration entity)
        {
            PatientRegistrationEntity patientRegistration = new PatientRegistrationEntity();

            patientRegistration.MESSAGE_HEADER = GetMessageHeader("ADT^A04", "13122", "P");
            patientRegistration.PATIENT_IDENTIFICATION = PATIENTIDENTIFICATION.GetPatientidentification(entity);
            patientRegistration.NEXT_OF_KIN = NEXTOFKIN.GetNextOfKins(entity);
            patientRegistration.VISIT = VISIT.GetVisit(entity);

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

        public DrugPrescriptionEntity DrugPrescriptionRaised(List<PrescriptionDto> prescription)
        {
            throw new System.NotImplementedException();


        }

        public string DrugPrescriptionRaised(PrescriptionDto entity)
        {

            var internalIdentifiers = new List<DTOIdentifier>();

            foreach (var identifier in entity.PatientIdentification.InternalPatientId)
            {
                var internalIdentity = new DTOIdentifier()
                {
                    IdentifierType = identifier.IdentifierType,
                    IdentifierValue = identifier.IdentifierValue,
                    AssigningAuthority = identifier.AssigningAuthority

                };
                internalIdentifiers.Add(internalIdentity);
            }

            var orderEncorder = new List<PharmacyEncodedOrder>();

            foreach (var order in entity.PharmacyEncodedOrder)
            {
                var prescriptionOrder = new PharmacyEncodedOrder()
                {
                    DrugName = order.DrugName,
                    CodingSystem = order.CodingSystem,
                    Strength = order.Strength,
                    Dosage = order.Dosage,
                    Frequency = order.Frequency,
                    Duration = Convert.ToInt32(order.Duration),
                    QuantityPrescribed = Convert.ToInt32(order.QuantityPrescribed),
                    PrescriptionNotes = order.PrescriptionNotes
                };
                orderEncorder.Add(prescriptionOrder);
            }
            var drugOrder = new PrescriptionDto()
            {
                MesssageHeader =
                {
                    SendingApplication = "IQCARE",
                    SendingFacility = entity.MesssageHeader.SendingFacility,
                    ReceivingApplication = "IL",
                    ReceivingFacility = entity.MesssageHeader.SendingFacility,
                    MessageDatetime =Convert.ToDateTime(entity.MesssageHeader.MessageDatetime), //DateTime.Now.ToString("yyyyMMddHHmmss"); 
                    Security = entity.MesssageHeader.Security,
                    MessageType = "RDE^001",
                    ProcessingId = "P"

                },
                PatientIdentification =
                {
                    ExternalPatientId = {},
                    InternalPatientId = internalIdentifiers,
                    PatientName =
                    {
                        FirstName = entity.PatientIdentification.PatientName.FirstName,
                        LastName = entity.PatientIdentification.PatientName.LastName,
                        MiddleName = entity.PatientIdentification.PatientName.MiddleName
                    }
                },
                CommonOrderDetails =
                {
                    OrderControl = entity.CommonOrderDetails.OrderControl,
                    PlacerOrderNumber=
                    {
                        Number = Convert.ToInt32(entity.CommonOrderDetails.PlacerOrderNumber.Number),
                        Entity = entity.CommonOrderDetails.PlacerOrderNumber.Entity
                    },
                    OrderStatus = entity.CommonOrderDetails.OrderStatus,
                    OrderingPhysician =
                    {
                        FirstName = entity.CommonOrderDetails.OrderingPhysician.FirstName,
                        MiddleName = entity.CommonOrderDetails.OrderingPhysician.MiddleName,
                        LastName = entity.CommonOrderDetails.OrderingPhysician.LastName
                    },
                    TransactionDatetime = Convert.ToDateTime(entity.CommonOrderDetails.TransactionDatetime),
                    Notes = entity.CommonOrderDetails.Notes.ToString()
                },
                PharmacyEncodedOrder = orderEncorder
            };

            //Serialise the Object to JSON
            return JsonConvert.SerializeObject(drugOrder);
        }

        public void DrugOrderCancel()
        {
            throw new System.NotImplementedException();
        }

        public string DrugOrderFulfilment(List<DispenseDto> dispenseDtos)
        {
            throw new System.NotImplementedException();
        }

        public void AppointmentScheduling()
        {
            throw new System.NotImplementedException();
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

        object IJsonEntityMapper.DrugPrescriptionRaised(PrescriptionDto drugOrderDto)
        {
            return DrugPrescriptionRaised(drugOrderDto);
        }

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

       
    }
}