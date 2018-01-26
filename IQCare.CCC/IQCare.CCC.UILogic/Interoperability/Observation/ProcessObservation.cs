using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO.CommonEntities;
using IQCare.DTO.ObservationResult;
using Entities.CCC.Enrollment;
using Entities.CCC.Interoperability;
using Entities.CCC.Visit;
using IQCare.CCC.UILogic.Interoperability.CommonMessageSegments;
using IQCare.CCC.UILogic.Interoperability.Enrollment;
using IQCare.CCC.UILogic.Visit;

namespace IQCare.CCC.UILogic.Interoperability.Observation
{
    public class ProcessObservation
    {
        public static ObservationResultDTO GetWHOStage(int entityId)
        {
            try
            {
                ObservationResultDTO observationResult = new ObservationResultDTO();
                PatientWhoStageManager whoStageManager = new PatientWhoStageManager();
                var whoStage = whoStageManager.GetWhoStageById(entityId);

                if (whoStage != null)
                {
                    LookupLogic lookupLogic = new LookupLogic();
                    PatientMasterVisitManager visitManager = new PatientMasterVisitManager();
                    PersonIdentifierManager personIdentifierManager = new PersonIdentifierManager();
                    IdentifierManager identifierManager = new IdentifierManager();
                    Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");
                    PatientMessageManager patientMessageManager = new PatientMessageManager();
                    PatientMessage patientMessage = patientMessageManager.GetPatientMessageByEntityId(whoStage.PatientId);
                    PatientMasterVisit visit = visitManager.GetVisitById(whoStage.PatientMasterVisitId);

                    List<PersonIdentifier> personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patientMessage.Id, identifier.Id);
                    string whoStageString  = lookupLogic.GetLookupItemNameByMasterNameItemId(whoStage.WHOStage, "WHOStage");

                    //Initialize default values
                    observationResult.PATIENT_IDENTIFICATION = observationResult.PATIENT_IDENTIFICATION == null ? new OBSERVATIONPATIENTIDENTIFICATION() : observationResult.PATIENT_IDENTIFICATION;
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID == null ? new EXTERNALPATIENTID() : observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME = observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME;
                    observationResult.OBSERVATION_RESULT = observationResult.OBSERVATION_RESULT == null ? new List<OBSERVATION_RESULT>() : observationResult.OBSERVATION_RESULT;

                    //External Patient Id
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = PatientIdentificationSegment.GetExternalPatientId(personIdentifiers);

                    //CCC Number
                    INTERNALPATIENTID internalPatientId = PatientIdentificationSegment.getInternalPatientIdCCC(patientMessage);
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalPatientId);

                    //National ID
                    INTERNALPATIENTID internalNationalId = PatientIdentificationSegment.getInternalPatientIdNationalId(patientMessage);
                    if (internalNationalId != null)
                    {
                        observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalNationalId);
                    }
                    
                    //Patient Names
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME = PatientIdentificationSegment.GetPatientName(patientMessage);

                    //set visitdate value
                    DateTime visitDate = visit.VisitDate.HasValue ? visit.VisitDate.Value : DateTime.Now;
                    string observationDate = visitDate.ToString("yyyyMMddHmmss", CultureInfo.InvariantCulture);

                    string observationValue = String.Empty;
                    switch (whoStageString)
                    {
                        case "Stage1":
                            observationValue = "1";
                            break;
                        case "Stage2":
                            observationValue = "2";
                            break;
                        case "Stage3":
                            observationValue = "3";
                            break;
                        case "Stage4":
                            observationValue = "4";
                            break;
                    }
                    //WHO STAGE
                    OBSERVATION_RESULT observation = new OBSERVATION_RESULT()
                    {
                        OBSERVATION_IDENTIFIER = "WHO_STAGE",
                        OBSERVATION_SUB_ID = "",
                        CODING_SYSTEM = "WHO",
                        VALUE_TYPE = "NM",
                        OBSERVATION_VALUE = observationValue,
                        UNITS = "",
                        OBSERVATION_RESULT_STATUS = "F",
                        OBSERVATION_DATETIME = observationDate,
                        ABNORMAL_FLAGS = "N"
                    };

                    observationResult.OBSERVATION_RESULT.Add(observation);
                }

                return observationResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static ObservationResultDTO GetVitals(int patientId, int patientMasterVisitId)
        {
            ObservationResultDTO observationResult = new ObservationResultDTO();

            try
            {
                PatientMessageManager patientMessageManager = new PatientMessageManager();
                PatientMessage patientMessage = patientMessageManager.GetPatientMessageByEntityId(patientId);
                if (patientMessage != null)
                {
                    PersonIdentifierManager personIdentifierManager = new PersonIdentifierManager();
                    IdentifierManager identifierManager = new IdentifierManager();
                    Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");

                    //Initialize default values
                    observationResult.PATIENT_IDENTIFICATION = observationResult.PATIENT_IDENTIFICATION == null ? new OBSERVATIONPATIENTIDENTIFICATION() : observationResult.PATIENT_IDENTIFICATION;
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID == null ? new EXTERNALPATIENTID() : observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME = observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME;
                    observationResult.OBSERVATION_RESULT = observationResult.OBSERVATION_RESULT == null ? new List<OBSERVATION_RESULT>() : observationResult.OBSERVATION_RESULT;

                    //External Patient Id
                    List<PersonIdentifier> personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patientMessage.Id, identifier.Id);
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = PatientIdentificationSegment.GetExternalPatientId(personIdentifiers);

                    //CCC Number
                    INTERNALPATIENTID internalPatientId = PatientIdentificationSegment.getInternalPatientIdCCC(patientMessage);
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalPatientId);

                    //National ID
                    if (!String.IsNullOrWhiteSpace(patientMessage.NATIONAL_ID) && patientMessage.NATIONAL_ID != "99999999")
                    {
                        INTERNALPATIENTID internalNationalId = PatientIdentificationSegment.getInternalPatientIdNationalId(patientMessage);
                        observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalNationalId);
                    }

                    //Patient Names
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME = PatientIdentificationSegment.GetPatientName(patientMessage);

                    //get vitals observation
                    PatientVitalsMessageManager patientVitalsMessage = new PatientVitalsMessageManager();
                    PatientVitalsMessage patientVitals = patientVitalsMessage.GetPatientVitalsMessageByPatientIdPatientMasterVisitId(patientId, patientMasterVisitId);
                    if (patientVitals != null)
                    {
                        OBSERVATION_RESULT observationHeight = new OBSERVATION_RESULT()
                        {
                            OBSERVATION_IDENTIFIER = "START_HEIGHT",
                            OBSERVATION_SUB_ID = "",
                            CODING_SYSTEM = "",
                            VALUE_TYPE = "NM",
                            OBSERVATION_VALUE = patientVitals.Height.ToString(),
                            UNITS = patientVitals.HeightUnits,
                            OBSERVATION_RESULT_STATUS = "F",
                            OBSERVATION_DATETIME = patientVitals.VisitDate,
                            ABNORMAL_FLAGS = "N"
                        };

                        OBSERVATION_RESULT observationWeight = new OBSERVATION_RESULT()
                        {
                            OBSERVATION_IDENTIFIER = "START_WEIGHT",
                            OBSERVATION_SUB_ID = "",
                            CODING_SYSTEM = "",
                            VALUE_TYPE = "NM",
                            OBSERVATION_VALUE = patientVitals.Weight.ToString(),
                            UNITS = patientVitals.WeightUnits,
                            OBSERVATION_RESULT_STATUS = "F",
                            OBSERVATION_DATETIME = patientVitals.VisitDate,
                            ABNORMAL_FLAGS = "N"
                        };

                        observationResult.OBSERVATION_RESULT.Add(observationHeight);
                        observationResult.OBSERVATION_RESULT.Add(observationWeight);
                    }
                }

                return observationResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
