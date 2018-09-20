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

        public List<DrugPrescriptionSourceEntity> GetPrescriptionSourceEntities(int ptntpk, int orderId,int patientMasterVisitId)
        {
            try
            {
                return _drugPrescriptionManager.GetPatientPrescriptionMessage(ptntpk, orderId, patientMasterVisitId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public PrescriptionSourceDto PreparePrescriptionSourceDto(int ptntpk, int orderId, int patientMasterVisitId)
        {
            try
            {
                var drugPrescriptionSourceData = _drugPrescriptionManager.GetPatientPrescriptionMessage(ptntpk, orderId, patientMasterVisitId);

                // prescriptionDtoMessage = prescriptionDtoMessage.All();

                //instantiate new PrescriptionDto 
                PrescriptionSourceDto prescriptionSourceDto = new PrescriptionSourceDto();

                ////todo -- MesasgeHeader-Automap
                prescriptionSourceDto.MESSAGE_HEADER.MESSAGE_TYPE = "RDE^001";
                prescriptionSourceDto.MESSAGE_HEADER.MESSAGE_DATETIME = Convert.ToDateTime(drugPrescriptionSourceData[0].TRANSACTION_DATETIME);
                prescriptionSourceDto.MESSAGE_HEADER.PROCESSING_ID = "P";
                prescriptionSourceDto.MESSAGE_HEADER.RECEIVING_APPLICATION = "IL";
                prescriptionSourceDto.MESSAGE_HEADER.RECEIVING_FACILITY = drugPrescriptionSourceData[0].SENDING_FACILITY;
                prescriptionSourceDto.MESSAGE_HEADER.SECURITY = "";
                prescriptionSourceDto.MESSAGE_HEADER.SENDING_APPLICATION = "IQCARE";
                prescriptionSourceDto.MESSAGE_HEADER.SENDING_FACILITY = drugPrescriptionSourceData[0].SENDING_FACILITY;


                ////todo - automap
                prescriptionSourceDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE = drugPrescriptionSourceData[0].EXT_IDENTIFIER_TYPE;
                prescriptionSourceDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID = drugPrescriptionSourceData[0].EXT_ID;
                prescriptionSourceDto.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = drugPrescriptionSourceData[0].EXT_ASSIGNING_AUTHOURITY;

                // var internalIdentifiers = new List<INTERNALPATIENTID>();
                var internalIdentity = new INTERNAL_PATIENT_ID()
                {
                    ASSIGNING_AUTHORITY = drugPrescriptionSourceData[0].ASSIGNING_AUTHORITY,
                    IDENTIFIER_TYPE = drugPrescriptionSourceData[0].IDENTIFIER_TYPE,
                    ID = drugPrescriptionSourceData[0].Id
                };

                prescriptionSourceDto.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalIdentity);

                var internalIdentityId = new INTERNAL_PATIENT_ID()
                {
                    ASSIGNING_AUTHORITY = drugPrescriptionSourceData[0].ASSIGNING_AUTHORITY2,
                    ID = drugPrescriptionSourceData[0].Id2,
                    IDENTIFIER_TYPE = drugPrescriptionSourceData[0].IDENTIFIER_TYPE2
                };
                prescriptionSourceDto.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalIdentityId);


                prescriptionSourceDto.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = drugPrescriptionSourceData[0].FIRST_NAME;
                prescriptionSourceDto.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = drugPrescriptionSourceData[0].MIDDLE_NAME;
                prescriptionSourceDto.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = drugPrescriptionSourceData[0].LAST_NAME;

                prescriptionSourceDto.COMMON_ORDER_DETAILS.NOTES = drugPrescriptionSourceData[0].NOTES;
                prescriptionSourceDto.COMMON_ORDER_DETAILS.ORDER_STATUS = drugPrescriptionSourceData[0].ORDER_STATUS;

                prescriptionSourceDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.FIRST_NAME = drugPrescriptionSourceData[0].ORDERING_PHYSICIAN_FIRST_NAME;
                prescriptionSourceDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.MIDDLE_NAME = "";
                prescriptionSourceDto.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.LAST_NAME = drugPrescriptionSourceData[0].ORDERING_PHYSICIAN_LAST_NAME;

                prescriptionSourceDto.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME = drugPrescriptionSourceData[0].TRANSACTION_DATETIME;
                prescriptionSourceDto.COMMON_ORDER_DETAILS.ORDER_CONTROL = drugPrescriptionSourceData[0].ORDER_CONTROL;

                prescriptionSourceDto.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY = drugPrescriptionSourceData[0].ENTITY;
                prescriptionSourceDto.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER = drugPrescriptionSourceData[0].NUMBER.ToString();

                var pharmacyEncodedOrder=new List<PHARMACY_ENCODED_ORDER>();

                foreach (var entity in drugPrescriptionSourceData)
                {
                    PHARMACY_ENCODED_ORDER drugLoadOrder = new PHARMACY_ENCODED_ORDER()
                    {
                        DRUG_NAME = entity.DRUG_NAME,
                        DOSAGE = entity.DOSAGE.ToString(),
                        CODING_SYSTEM = entity.CODING_SYSTEM,
                        DURATION = Convert.ToInt32(entity.DURATION),
                        FREQUENCY = entity.FREQUENCY,
                        INDICATION = entity.INDICATION,
                        PHARMACY_ORDER_DATE = entity.PHARMACY_ORDER_DATE,
                        QUANTITY_PRESCRIBED = entity.QUANTITY_PRESCRIBED.ToString(),
                        STRENGTH = entity.STRENGTH,
                        TREATMENT_INSTRUCTION = entity.TREATMENT_INSTRUCTION,
                        PRESCRIPTION_NOTES = entity.PRESCRIPTION_NOTES
                    };
                    pharmacyEncodedOrder.Add(drugLoadOrder);
                   // prescriptionSourceDto.PHARMACY_ENCODED_ORDER.Add(drugLoadOrder);
                }
                prescriptionSourceDto.PHARMACY_ENCODED_ORDER = pharmacyEncodedOrder;
                return prescriptionSourceDto;
               // return drugPrescriptionSourceData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
