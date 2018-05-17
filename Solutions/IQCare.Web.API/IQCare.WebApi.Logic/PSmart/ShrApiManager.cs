using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using Application.Common;
using Entities.Administration;

using Entities.CCC.PSmart;
using Entities.PSmart;
using Entities.Queue;
using Interface.Clinical;
using Interface.FormBuilder;
using Interface.PatientCore;
using Interface.WebApi;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO.PSmart;

namespace IQCare.WebApi.Logic.PSmart
{
    public class ShrApiManager
    {

        private PsmartShrManager _psmartShrManager;
        private readonly IShrApiManager _shrApiManager = (IShrApiManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BShrApi, BusinessProcess.WebApi");
        private readonly IPatientService _patientCoreService = (IPatientService)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.PatientCore.PatientCoreServices, BusinessProcess.PatientCore");
        private IPatientQueue _queueService = (IPatientQueue)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientQueue, BusinessProcess.Clinical");
        private IModule _moduleService =(IModule) Application.Presentation.ObjectFactory.CreateInstance(
                "BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");

        private MstPatientManager _mstPatientManager;
        private PSmartPatientProgramStartManager _patientProgramStart;
        //private PatientVisitManager _patientVisitManager;
        private HivTestTrackerManager _hivTestTrackerManager;
        private ImmunizationTrackerManager _immunizationTracker;
        private Utility _utility;
        private PsmartMotherDetailsViewManager _motherDetailsViewManager;
        private PatientIdentifierManager _patientIdentifierManager;

        private int ptnpk=0;
        public ShrApiManager()
        {
            _psmartShrManager = new PsmartShrManager();
            _mstPatientManager=new MstPatientManager();
          //  _patientVisitManager=new PatientVisitManager();
            _patientProgramStart=new PSmartPatientProgramStartManager();
            _hivTestTrackerManager=new HivTestTrackerManager();
            _immunizationTracker=new ImmunizationTrackerManager();
            _utility=new Utility();
            _motherDetailsViewManager=new PsmartMotherDetailsViewManager();
            _patientIdentifierManager=new PatientIdentifierManager();
        }

        private readonly JavaScriptSerializer _jsonSerialiser = new JavaScriptSerializer();

        private static DateTime GetDateValueFromYYYYMMDD(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateValue))
                return dateValue;

            return new DateTime(1900, 1, 1);
        }


        public string ProcessIncomingShr(string _shrMessage)
        {
            try
            {
                //save in transcation log
                TransactionLog log = new TransactionLog();
                log.TransactionDate = DateTime.Now;
                log.TransactionType = 1;
                log.UserId = 1;
                log.Request = _shrMessage;
                log.TranLogId = new Guid();

                string HTSId = "";
                bool hasErrors = false;
                List<string> Errors = new List<string>();
                //process
                //to do use the validator object to validate instead of ifs
                DtoShr entity = new JavaScriptSerializer().Deserialize<DtoShr>(_shrMessage);


                DTO.PSmart.CARDDETAILS cardDetails = entity.CARD_DETAILS;
                DTO.PSmart.PATIENTIDENTIFICATION patientIdentification = entity.PATIENT_IDENTIFICATION;
                List<DTO.PSmart.NEXTOFKIN> nextOfKin = entity.NEXT_OF_KIN;
                List<DTO.PSmart.IMMUNIZATION> immunizations = entity.IMMUNIZATION;
                List<DTO.PSmart.HIVTEST> hivTests = entity.HIV_TEST;
                DTO.PSmart.PATIENTNAME clientName = patientIdentification.PATIENT_NAME;
                List<DTO.PSmart.INTERNALPATIENTID> internalIdentifier = patientIdentification.INTERNAL_PATIENT_ID;

                //DTO.PSmart.CARDDETAILS carddetails= new DTO.PSmart.CARDDETAILS()
                //{
                // //   CardSerialNumber = entity.CARD_DETAILS.CardSerialNumber,
                //    LAST_UPDATED = entity.CARD_DETAILS.LAST_UPDATED,
                //    LAST_UPDATED_FACILITY = entity.CARD_DETAILS.LAST_UPDATED_FACILITY,
                //    PatientId = entity.CARD_DETAILS.PatientId
                //};

                //PATIENTIDENTIFICATION patientIdentification =new PATIENTIDENTIFICATION()
                //{
                //    CardSerialNumber = entity.
              //  };



                string _cardserialnumber = "";
                //if (string.IsNullOrEmpty(entity.VERSION))
                //{
                //    Errors.Add("Missing a required segment: VERSION");
                //    hasErrors = true;
                //}
                if (internalIdentifier == null)
                {
                    Errors.Add("Missing a required segment: INTERNAL_PATIENT_ID");
                    hasErrors = true;
                }
                else
                {
                    if (!internalIdentifier.Exists(xx => xx.IDENTIFIER_TYPE == "CARD_SERIAL_NUMBER"))
                    {

                        Errors.Add("Missing CARD_SERIAL_NUMBER. Cannot be processed");
                        hasErrors = true;

                    }
                    else
                    {
                        _cardserialnumber = internalIdentifier.Find(xx => xx.IDENTIFIER_TYPE == "CARD_SERIAL_NUMBER").ID;
                    }
                    if (!internalIdentifier.Exists(xx => xx.IDENTIFIER_TYPE == "HTS_NUMBER"))
                    {

                        Errors.Add("Missing HTS_NUMBER. Cannot be processed");
                        HTSId = "missing";
                        //hasErrors = true;
                    }
                }
                if (clientName == null || clientName.FIRST_NAME == string.Empty || clientName.LAST_NAME == string.Empty)
                {
                    Errors.Add("Missing a required segment: PATIENT_NAME");
                    hasErrors = true;
                }
                if (string.IsNullOrEmpty(patientIdentification.DATE_OF_BIRTH))
                {
                    Errors.Add("Missing a required segment: DATE_OF_BIRTH");
                    hasErrors = true;
                }
                else if (GetDateValueFromYYYYMMDD(patientIdentification.DATE_OF_BIRTH) > DateTime.Today)
                {
                    Errors.Add("Error: Future DATE_OF_BIRTH");
                    hasErrors = true;
                }
               
                if (patientIdentification.SEX == string.Empty)
                {
                    Errors.Add("Missing a required segment: SEX");
                    hasErrors = true;
                }
                if (cardDetails == null)
                {
                    Errors.Add("Missing a required segment: CARD_DETAILS");
                    hasErrors = true;
                }
                else if (cardDetails.LAST_UPDATED_FACILITY.Trim() == string.Empty)
                {
                    Errors.Add("Missing a required element: LAST_UPDATED_FACILITY");
                    hasErrors = true;
                }
                if (hasErrors)
                {
                    log.LogMessage= String.Join(" | ", Errors.ToArray());
                    _shrApiManager.LogPSmartRequest(log);
                    return $"Erros Encounters {String.Join(" | ", Errors.ToArray())}";
                }
                else
                {

                    log.LogMessage = "Valid Message. Ready to process";
                   // Module module = _moduleService.GetMstModueByName("HTC Module"); TODO enanble for OLD HTS ONLY:208 THE DEFAILT ID FOR NEW HTS MODULE 
                    //find if there is client record for the serial number if yes 
                    //var patient = _patientCoreService.GetPatient(_cardserialnumber);TODO:Finds patient in mst_patient
                    var patient = _patientIdentifierManager.GetPatientByCardSerialNumber(_cardserialnumber);

                    if (patient != null)
                    {
                        //(check in) , 
                       // Module modules = _moduleService.GetModuleByName("HTC Module");
                        WaitingQueue queue = _queueService.GetQueueByName("HTS Services (PSmart)");
                        //to do create a psmart user or adjust the request from interactor to come with the logged in user
                        _queueService.QueuePatient(patient.PatientId, queue.QueueId, QueueStatus.Pending, QueuePriority.Normal,
                            208, 1);
                        //(update demographics

                        //
                        /* //HIV tracking table
                         *  PersonId, Date Tested, MFL, result, test type (Screening...), catergory (pcr, antibody), provider, strategy)
                         * //Immunizationtracking table
                         *  PersonId, Dategiven, antigen, provider, lot number, expiry date, next visit
                         * 
                         * */
                        string PersonId = "";
                        string cccNumbers = "";

                        foreach (var motherDetail in patientIdentification.MOTHER_DETAILS.MOTHER_IDENTIFIER)
                        {
                            if (motherDetail.IDENTIFIER_TYPE == "CCC_NUMBER")
                            {
                                cccNumbers = motherDetail.ID.ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(patient.PatientId.ToString()))
                        {
                            if (hivTests != null)
                            {
                                foreach (var hivtest in hivTests)
                                {
                                    // DateTime resultDate=   DateTime.Parse(hivtest.DATE, CultureInfo.CreateSpecificCulture("fr-FR"));
                                    DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd",
                                        CultureInfo.InvariantCulture);
                                    // resultDate = resultDate.ToString("yyyy-MM-dd");

                                    _hivTestTrackerManager.AddHivTestTracker(Convert.ToInt32(patient.PatientId),
                                        hivtest.FACILITY,
                                        hivtest.STRATEGY, hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
                                        Convert.ToInt32(patient.Id), resultDate, hivtest.RESULT, hivtest.STRATEGY,
                                        hivtest.TYPE);
                                }
                            }

                            if (immunizations != null)
                            {
                                foreach (var immunization in immunizations)

                                {
                                    DateTime administeredDate = DateTime.ParseExact(immunization.DATE_ADMINISTERED,
                                        "yyyyMMd", CultureInfo.InvariantCulture);
                                    ProcessImmunizationSegment(administeredDate, immunization.NAME,
                                        Convert.ToInt32(patient.Id), Convert.ToInt32(patient.PatientId));
                                }
                            }

                            if (!string.IsNullOrEmpty(patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME))
                            {
                                MotherDetailsView motherDetailsView = new MotherDetailsView()
                                {
                                    Ptn_pk = Convert.ToInt32(patient.PatientId),
                                    firstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME,
                                    middleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME,
                                    lastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME,
                                };
                                var pk = _motherDetailsViewManager.FindExistingMother(motherDetailsView);
                                if (pk != null)
                                {
                                }
                                else
                                {
                                    string mfirstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME;
                                    string mmiddleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME;
                                    string mlastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME;


                                    _mstPatientManager.ProcessMotherNames(mfirstName, mmiddleName, mlastName,
                                        cccNumbers, Convert.ToInt32(patient.Id));
                                }

                            }


                        }
                    }
                    else
                    {
                        //else (register) in bluecard
                        //mst_patient, ord_visit, lnk_patientprogramstart,
                        //to do register in new tables
                        //insert demographics:

                       // Module module = _moduleService.GetMstModueByName("HTC Module");
                        string HTSID = "";
                        string CARD_SERIAL_NO = "";
                        string HEID = "";
                        var msg = "";
                        string cccNumber = "";

                        foreach (var x in entity.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                        {
                            switch (x.IDENTIFIER_TYPE)
                            {
                                case "HTS_NUMBER":
                                   
                                    if (HTSId == "missing")
                                    {
                                        HTSID = DateTime.Now.Ticks.ToString();}else { HTSID = x.ID; }
                                    break;
                                case "CARD_SERIAL_NUMBER":
                                    CARD_SERIAL_NO = x.ID;
                                    break;
                                //case "HEI_NUMBER":
                                //    HEID = x.ID;
                                //    break;
                                default: break;
                            }
                        }

                        foreach (var motherDetail in patientIdentification.MOTHER_DETAILS.MOTHER_IDENTIFIER)
                        {
                            if (motherDetail.IDENTIFIER_TYPE == "CCC_NUMBER")
                            {
                                cccNumber = motherDetail.ID.ToString();
                            }
                        }

                        string firstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        DateTime registrationDate = DateTime.Now; //.ToString("")
                        string dob = entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH;
                        string dobPrecision = (entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION == "ESTIMATED")
                            ? "1"
                            : "0";
                        string phone = entity.PATIENT_IDENTIFICATION.PHONE_NUMBER;
                        string gender = entity.PATIENT_IDENTIFICATION.SEX;
                        string landmark = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .NEAREST_LANDMARK;
                        string maritalStatus = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string village =
                            entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE;
                        string ward = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;
                        string subcounty = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;

                        string address = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;




                        msg = _mstPatientManager.AddMstPatient(firstName, middleName, lastName, registrationDate, dob,
                            dobPrecision, phone, gender, landmark, maritalStatus, HTSID, 208.ToString(),
                            CARD_SERIAL_NO,
                            village, ward, subcounty, HEID, address).ToString();

                        if (!string.IsNullOrEmpty(msg))
                        {
                            if (hivTests != null)
                            {
                                foreach (var hivtest in hivTests)
                                {
                                    // DateTime resultDate=   DateTime.Parse(hivtest.DATE, CultureInfo.CreateSpecificCulture("fr-FR"));
                                    DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd",
                                        CultureInfo.InvariantCulture);
                                    // resultDate = resultDate.ToString("yyyy-MM-dd");

                                    _hivTestTrackerManager.AddHivTestTracker(Convert.ToInt32(msg), hivtest.FACILITY,
                                        hivtest.STRATEGY, hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
                                        Convert.ToInt32(msg), resultDate, hivtest.RESULT, hivtest.STRATEGY,
                                        hivtest.TYPE);
                                }
                            }

                            if (immunizations != null)
                            {
                                foreach (var immunization in immunizations)

                                {
                                    DateTime administeredDate = DateTime.ParseExact(immunization.DATE_ADMINISTERED,"yyyyMMdd", CultureInfo.InvariantCulture);
                                    ProcessImmunizationSegment(administeredDate, immunization.NAME,
                                        Convert.ToInt32(msg), Convert.ToInt32(msg));
                                }
                            }

                            if (!string.IsNullOrEmpty(patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME))
                            {
                                MotherDetailsView motherDetailsView = new MotherDetailsView()
                                {
                                    Ptn_pk = Convert.ToInt32(msg),
                                    firstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME,
                                    middleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME,
                                    lastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME,
                                };
                                var pk = _motherDetailsViewManager.FindExistingMother(motherDetailsView);
                                if (!string.IsNullOrEmpty(pk))
                                {
                                }
                                else
                                {
                                    firstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME;
                                    middleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME;
                                    lastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME;

                                    _mstPatientManager.ProcessMotherNames(firstName, middleName, lastName, cccNumber,
                                        Convert.ToInt32(msg));
                                }

                            }
                            // if (hivTests != null) { ProcessHivTestSegment(Convert.ToInt32(msg), hivTests); }
                            //if(immunizations!=null) { ProcessImmunizationSegment(immunization. Convert.ToInt32(msg), immunizations); }

                        }
                    }

                    return "Client Processed Successfully!";
                }
               
            }
            catch (Exception ex) { return $"error occured {ex.Message}"; }
        }
        void ProcessHivTestSegment(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy, string testCategory)
        {
             _hivTestTrackerManager.AddHivTestTracker(personId, facilityMflCode,diagnosisMode,providerName,providerId,ptnpk,resultDate,testResult,strategy,testCategory);
        }
        void ProcessImmunizationSegment(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk)
        {
            _immunizationTracker.AddImmunizationTracker(dateAdministered, antigenAdministered, personId, ptnPk);
        }
    }
}