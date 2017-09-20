using IQCare.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.DtoMapping
{
    public class DtoMapper : IDtoMapper
    {
        //todo handle possible null reference exceptions when fetching data from lists
        public Registration PatientRegistrationMapping(PatientRegistrationEntity entity)
        {
            var patient = new DTOPerson()
            {
                FirstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                MiddleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                LastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                DateOfBirth = entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH,
                MobileNumber = entity.PATIENT_IDENTIFICATION.PHONE_NUMBER,
                NationalId = entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(n=>n.IDENTIFIER_TYPE == "NATIONAL_ID").ID,
                Sex = entity.PATIENT_IDENTIFICATION.SEX,
                PhysicalAddress = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS,
                //todo update precision once updated in IL
                DobPrecision = false
            };

            var ts = entity.NEXT_OF_KIN.FirstOrDefault(n => n.CONTACT_ROLE == "T");
            var treatmentSupporter = new DTOPerson()
            {
                FirstName = ts.NOK_NAME.FIRST_NAME,
                MiddleName = ts.NOK_NAME.MIDDLE_NAME,
                LastName = ts.NOK_NAME.LAST_NAME,
                PhysicalAddress = ts.ADDRESS,
                Sex = ts.SEX,
                DateOfBirth = ts.DATE_OF_BIRTH,
                MobileNumber = ts.PHONE_NUMBER,
                //todo update precision once updated in IL
                //NationalId = ,
                DobPrecision = false
            };
            var identifiers = new List<DTOIdentifier>();
            foreach (var id in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
            {
               var identifier = new DTOIdentifier()
               {
                   AssigningAuthority = id.ASSIGNING_AUTHORITY,
                   IdentifierType = id.IDENTIFIER_TYPE,
                   IdentifierValue = id.ID
               };
                identifiers.Add(identifier);
            }
            var registration = new Registration()
            {
                Patient = patient,
                MotherMaidenName = entity.PATIENT_IDENTIFICATION.MOTHER_MAIDEN_NAME,
                MaritalStatus = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS,
                County = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY,
                SubCounty = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY,
                Ward = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD,
                Village = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE,
                DateOfDeath = entity.PATIENT_IDENTIFICATION.DEATH_DATE,
                DeathIndicator = entity.PATIENT_IDENTIFICATION.DEATH_INDICATOR,
                TreatmentSupporter = treatmentSupporter,
                TSRelationshipType =  ts.RELATIONSHIP,
                InternalPatientIdentifiers = identifiers,
                DateOfEnrollment = DateTime.Now,
                
            };
            return registration;
        }

        public void PatientTransferIn()
        {
            throw new NotImplementedException();
        }

        public void UpdatedClientInformation()
        {
            throw new NotImplementedException();
        }

        public void PatientTransferOut()
        {
            throw new NotImplementedException();
        }

        public void RegimenChange()
        {
            throw new NotImplementedException();
        }

        public void StopDrugs()
        {
            throw new NotImplementedException();
        }

        public void DrugPrescriptionRaised()
        {
            throw new NotImplementedException();
        }

        public void DrugOrderCancel()
        {
            throw new NotImplementedException();
        }

        public void DrugOrderFulfilment()
        {
            throw new NotImplementedException();
        }

        public void AppointmentScheduling()
        {
            throw new NotImplementedException();
        }

        public void AppointmentUpdated()
        {
            throw new NotImplementedException();
        }

        public void AppointmentRescheduling()
        {
            throw new NotImplementedException();
        }

        public void AppointmentCanceled()
        {
            throw new NotImplementedException();
        }

        public void AppointmentHonored()
        {
            throw new NotImplementedException();
        }

        public void UniquePatientIdentification()
        {
            throw new NotImplementedException();
        }

        public void ViralLoadLabOrder()
        {
            throw new NotImplementedException();
        }

        public ViralLoadDto ViralLoadResults(ViralLoadResultEntity entity)
        {
            var resultsDto =new ViralLoadDto()
            {
                Id = entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Where(d=>d.IDENTIFIER_TYPE=="CCC_NUMBER").Select(x=>x.ID).ToString(),
                IdentifierType = "CCC_NUMBER",
                AssigningAuthourity = "CCC",
                DateSampleCollected = entity.VIRAL_LOAD_RESULT[0].DATE_SAMPLE_COLLECTED,
                DateSampleTested = entity.VIRAL_LOAD_RESULT[0].DATE_SAMPLE_TESTED,
                Regimen = entity.VIRAL_LOAD_RESULT[0].REGIMEN,
                VlResult = entity.VIRAL_LOAD_RESULT[0].VL_RESULT,
                LabTestedIn = entity.VIRAL_LOAD_RESULT[0].LAB_TESTED_IN,
                Justification = entity.VIRAL_LOAD_RESULT[0].JUSTIFICATION
            };

            return resultsDto;
        }
    }
}