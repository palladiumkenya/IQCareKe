using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public  class DrugPrescriptionMessage 
    {
        private readonly IDrugPrescriptionManager _drugPrescriptionManager  = (IDrugPrescriptionManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BDrugPrescriptionManager, BusinessProcess.CCC");

        public PrescriptionDto  GetPrescriptionMessage(int ptntpk,int orderId,int patientMasterVisitId)
        {
            try
            {
                var prescriptionDtoMessage =_drugPrescriptionManager.GetPatientPrescriptionMessage(ptntpk, orderId, patientMasterVisitId);

                var internalIdentifiers = new List<DTOIdentifier>();

                foreach (var identifier in prescriptionDtoMessage)
                {
                    var internalIdentity = new DTOIdentifier()
                    {
                        IdentifierValue = identifier.Id,
                        AssigningAuthority = identifier.ASSIGNING_AUTHORITY,
                        IdentifierType = identifier.IDENTIFIER_TYPE
                    };
                    internalIdentifiers.Add(internalIdentity);
                }
                List<PharmacyEncorderOrderDto> drugsPayLoad = new List<PharmacyEncorderOrderDto>();
                foreach (var message in prescriptionDtoMessage)
                {
                    var messageOrder = new PharmacyEncorderOrderDto()
                    {
                        DrugName = message.DRUG_NAME,
                        CodingSystem = message.CODING_SYSTEM,
                        Strength = message.STRENGTH,
                        Dosage = message.DOSAGE,
                        Frequency = message.FREQUENCY,
                        Duration = message.DURATION,
                        QuantityPrescribed = message.QUANTITY_PRESCRIBED,
                        PrescriptionNotes = message.NOTES
                    };
                    drugsPayLoad.Add(messageOrder);
                }

                var prescrptionDtoPayLoad = new PrescriptionDto()
                {
                    MesssageHeader = 
                     {
                         MessageDatetime = prescriptionDtoMessage[0].TRANSACTION_DATETIME,
                         MessageType = "RDE^001",
                         ProcessingId = "P",
                         ReceivingApplication = "IL",
                         ReceivingFacility = "IQCare",
                         Security = "",
                         SendingApplication = "IQCare",
                         SendingFacility = "IQCare"
                     },
                    PatientIdentification =
                    {
                        ExternalPatientId =
                        {
                            AssigningAuthority = prescriptionDtoMessage[0].ASSIGNING_AUTHORITY2,
                            IdentifierType = prescriptionDtoMessage[0].IDENTIFIER_TYPE2,
                            IdentifierValue = prescriptionDtoMessage[0].Id2
                        },
                        InternalPatientId =internalIdentifiers,
                        PatientName =
                        {
                            FirstName = prescriptionDtoMessage[0].FIRST_NAME,
                            MiddleName = prescriptionDtoMessage[0].MIDDLE_NAME,
                            LastName = prescriptionDtoMessage[0].LAST_NAME
                        }
                    }, 
                    CommonOrderDetails = 
                    {
                         Notes = prescriptionDtoMessage[0].NOTES,
                         OrderControl = prescriptionDtoMessage[0].ORDER_CONTROL,
                         OrderStatus = prescriptionDtoMessage[0].ORDER_STATUS,
                         OrderingPhysicianDto = 
                         {
                             LastName = prescriptionDtoMessage[0].ORDERING_PHYSICIAN_FIRST_NAME,
                             FirstName = prescriptionDtoMessage[0].ORDERING_PHYSICIAN_LAST_NAME
                         },
                         ptnpk = prescriptionDtoMessage[0].ptnpk,
                         PatientId = prescriptionDtoMessage[0].PatientId,
                         PlacerOrderNumberDto = 
                         {
                             Entity = prescriptionDtoMessage[0].ENTITY,
                             Number = prescriptionDtoMessage[0].NUMBER
                         },
                         TransactionDatetime = prescriptionDtoMessage[0].TRANSACTION_DATETIME
                     },
                     PharmacyEncodedOrderDto = drugsPayLoad
                };
                return prescrptionDtoPayLoad;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
