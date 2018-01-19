using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;
using System.Web.UI;
using Application.Presentation;
using Interface.CCC.Interoperability;
using IQCare.DTO;
using IQCare.DTO.CommonEntities;

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

               // prescriptionDtoMessage = prescriptionDtoMessage.All();

                //instantiate new PrescriptionDto 
                PrescriptionDto prescriptionDtoPayLoad = new PrescriptionDto(); 

                //todo -- MesasgeHeader-Automap
                prescriptionDtoPayLoad.MESSAGE_HEADER.MESSAGE_TYPE = "RDE^001";
                prescriptionDtoPayLoad.MESSAGE_HEADER.MESSAGE_DATETIME =Convert.ToDateTime(prescriptionDtoMessage[0].TRANSACTION_DATETIME);
                prescriptionDtoPayLoad.MESSAGE_HEADER.PROCESSING_ID = "P";
                prescriptionDtoPayLoad.MESSAGE_HEADER.RECEIVING_APPLICATION = "IL";
                prescriptionDtoPayLoad.MESSAGE_HEADER.RECEIVING_FACILITY = prescriptionDtoMessage[0].SENDING_FACILITY;
                prescriptionDtoPayLoad.MESSAGE_HEADER.SECURITY = "";
                prescriptionDtoPayLoad.MESSAGE_HEADER.SENDING_APPLICATION = "IQCARE";
                prescriptionDtoPayLoad.MESSAGE_HEADER.SENDING_FACILITY = prescriptionDtoMessage[0].SENDING_FACILITY;
                
                
                //todo - automap
                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE = prescriptionDtoMessage[0].EXT_IDENTIFIER_TYPE;
                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID =prescriptionDtoMessage[0].EXT_ID;
                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = prescriptionDtoMessage[0].EXT_ASSIGNING_AUTHOURITY;

               // var internalIdentifiers = new List<INTERNALPATIENTID>();
                foreach (var internalPatientId in prescriptionDtoMessage)
                {   
                    var internalIdentity=new INTERNAL_PATIENT_ID()
                    {
                        ASSIGNING_AUTHORITY = internalPatientId.ASSIGNING_AUTHORITY,
                        IDENTIFIER_TYPE = internalPatientId.IDENTIFIER_TYPE,
                        ID = internalPatientId.Id
                    };
                    var internalIdentityId=new INTERNAL_PATIENT_ID()
                    {
                        ASSIGNING_AUTHORITY = internalPatientId.ASSIGNING_AUTHORITY2,
                        ID = internalPatientId.Id2,
                        IDENTIFIER_TYPE = internalPatientId.IDENTIFIER_TYPE2
                    };
                    prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalIdentity);
                    prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalIdentityId);
                }

                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = prescriptionDtoMessage[0].FIRST_NAME;
                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = prescriptionDtoMessage[0].MIDDLE_NAME;
                prescriptionDtoPayLoad.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = prescriptionDtoMessage[0].LAST_NAME;

                if (null!= prescriptionDtoPayLoad.COMMON_ORDER_DETAILS)
                {
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.NOTES = prescriptionDtoMessage[0].NOTES;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.ORDER_STATUS = prescriptionDtoMessage[0].ORDER_STATUS;
                    
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.FIRST_NAME =prescriptionDtoMessage[0].ORDERING_PHYSICIAN_FIRST_NAME;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.MIDDLE_NAME = "";
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN.LAST_NAME =prescriptionDtoMessage[0].ORDERING_PHYSICIAN_LAST_NAME;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME =prescriptionDtoMessage[0].TRANSACTION_DATETIME;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.ORDER_CONTROL = prescriptionDtoMessage[0].ORDER_CONTROL;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.ENTITY =prescriptionDtoMessage[0].ENTITY;
                    prescriptionDtoPayLoad.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER = prescriptionDtoMessage[0].NUMBER.ToString();
                }

                foreach (var entity in prescriptionDtoMessage)
                {
                    PHARMACY_ENCODED_ORDER drugLoadOrder=new PHARMACY_ENCODED_ORDER()
                    {
                        DRUG_NAME = entity.DRUG_NAME,
                        DOSAGE =entity.DOSAGE.ToString(),
                        CODING_SYSTEM = entity.CODING_SYSTEM,
                        DURATION =Convert.ToInt32(entity.DURATION),
                        FREQUENCY = entity.FREQUENCY,
                        INDICATION = entity.INDICATION,
                        PHARMACY_ORDER_DATE = entity.PHARMACY_ORDER_DATE,
                        QUANTITY_PRESCRIBED = entity.QUANTITY_PRESCRIBED.ToString() ,
                        STRENGTH = entity.STRENGTH,
                        TREATMENT_INSTRUCTION = entity.TREATMENT_INSTRUCTION   ,
                        PRESCRIPTION_NOTES=entity.PRESCRIPTION_NOTES
                    };
                    prescriptionDtoPayLoad.PHARMACY_ENCODED_ORDER.Add(drugLoadOrder);                 
                }
                return prescriptionDtoPayLoad;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
