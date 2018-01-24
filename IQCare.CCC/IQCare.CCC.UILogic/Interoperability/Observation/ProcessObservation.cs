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

                    var personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patientMessage.Id, identifier.Id);
                    string whoStageString  = lookupLogic.GetLookupItemNameByMasterNameItemId(whoStage.WHOStage, "WHOStage");

                    //Initialize default values
                    observationResult.PATIENT_IDENTIFICATION = observationResult.PATIENT_IDENTIFICATION == null ? new OBSERVATIONPATIENTIDENTIFICATION() : observationResult.PATIENT_IDENTIFICATION;
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID == null ? new EXTERNALPATIENTID() : observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID;
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME = observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME;
                    observationResult.OBSERVATION_RESULT = observationResult.OBSERVATION_RESULT == null ? new List<OBSERVATION_RESULT>() : observationResult.OBSERVATION_RESULT;

                    //External Patient Id
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID = personIdentifiers.Count > 0 ? personIdentifiers[0].IdentifierValue : String.Empty;
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = "MPI";
                    observationResult.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE = "GODS_NUMBER";

                    //CCC Number
                    INTERNALPATIENTID internalPatientId = new INTERNALPATIENTID();
                    internalPatientId.ID = patientMessage.IdentifierValue;
                    internalPatientId.IDENTIFIER_TYPE = "CCC_NUMBER";
                    internalPatientId.ASSIGNING_AUTHORITY = "CCC";

                    //National ID
                    observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalPatientId);
                    if (!String.IsNullOrWhiteSpace(patientMessage.NATIONAL_ID) && patientMessage.NATIONAL_ID != "99999999")
                    {
                        INTERNALPATIENTID internalNationalId = new INTERNALPATIENTID();
                        internalNationalId.ID = patientMessage.NATIONAL_ID;
                        internalNationalId.IDENTIFIER_TYPE = "NATIONAL_ID";
                        internalNationalId.ASSIGNING_AUTHORITY = "GOK";

                        observationResult.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalNationalId);
                    }
                    //Patient Names
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = !string.IsNullOrWhiteSpace(patientMessage.FIRST_NAME) ? patientMessage.FIRST_NAME : "";
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = !string.IsNullOrWhiteSpace(patientMessage.MIDDLE_NAME) ? patientMessage.MIDDLE_NAME : "";
                    observationResult.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = !string.IsNullOrWhiteSpace(patientMessage.LAST_NAME) ? patientMessage.LAST_NAME : "";

                    //string observationDate  = DateTime.ParseExact(visit.VisitDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMddHmmss");
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
    }
}
