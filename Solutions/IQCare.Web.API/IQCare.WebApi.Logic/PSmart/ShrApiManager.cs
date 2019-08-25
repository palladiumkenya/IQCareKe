using Application.Common;
using Application.Presentation;
using Entities.Administration;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Entities.CCC.PSmart;
using Entities.PSmart;
using Interface.Clinical;
using Interface.FormBuilder;
using Interface.PatientCore;
using Interface.WebApi;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO.PSmart;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;

namespace IQCare.WebApi.Logic.PSmart
{
    public class ShrApiManager
    {

        private PsmartShrManager _psmartShrManager;
        private readonly IShrApiManager _shrApiManager = (IShrApiManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BShrApi, BusinessProcess.WebApi");
        private readonly IPatientService _patientCoreService = (IPatientService)ObjectFactory.CreateInstance("BusinessProcess.PatientCore.PatientCoreServices, BusinessProcess.PatientCore");
        private IPatientQueue _queueService = (IPatientQueue)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientQueue, BusinessProcess.Clinical");
        private IModule _moduleService = (IModule)ObjectFactory.CreateInstance(
                "BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
        IHivTestTrackerManager hivTestTrackerManager = (IHivTestTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BHivTestTrackerManager, BusinessProcess.WebApi");

        private MstPatientManager _mstPatientManager;
        private PSmartPatientProgramStartManager _patientProgramStart;
        //private PatientVisitManager _patientVisitManager;
        private HivTestTrackerManager _hivTestTrackerManager;
        private ImmunizationTrackerManager _immunizationTracker;
        private Utility _utility;
        private PsmartMotherDetailsViewManager _motherDetailsViewManager;
        private PatientIdentifierManager _patientIdentifierManager;
        private PatientLookupManager _patientLookupManager;
        PatientManager _patientManager;
        PersonManager personManager;
        //   private int ptnpk=0;
        public ShrApiManager()
        {
            _psmartShrManager = new PsmartShrManager();
            _mstPatientManager = new MstPatientManager();
            //  _patientVisitManager=new PatientVisitManager();
            _patientProgramStart = new PSmartPatientProgramStartManager();
            _hivTestTrackerManager = new HivTestTrackerManager();
            _immunizationTracker = new ImmunizationTrackerManager();
            _utility = new Utility();
            _motherDetailsViewManager = new PsmartMotherDetailsViewManager();
            _patientIdentifierManager = new PatientIdentifierManager();
            _patientLookupManager = new PatientLookupManager();
            _patientManager = new PatientManager();
            personManager = new PersonManager();
        }

        private readonly JavaScriptSerializer _jsonSerialiser = new JavaScriptSerializer();
        public List<HivTestTracker> GetPersonsHivTests(int personId)
        {

            return hivTestTrackerManager.GetPersonHIVTest(personId);

        }
        public void TrackHIVTest(PatientEntity patient, List<DTO.PSmart.HIVTEST> hivTests)
        {
            List<HivTestTracker> hivTracker = this.GetPersonsHivTests(patient.PersonId);

            foreach (var hivtest in hivTests.Where(tx => !string.IsNullOrWhiteSpace(tx.RESULT)))
            {
                if (!hivTracker.Exists(xx => xx.PersonId == patient.PersonId
                 && xx.MFLCode == hivtest.FACILITY
                 && xx.Strategy == hivtest.STRATEGY
                 && xx.Result == hivtest.RESULT
                 && xx.ResultDate == DateTime.ParseExact(hivtest.DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                  && xx.ProviderName.Trim().ToLower() == xx.ProviderName.Trim().ToLower()))
                {
                    DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    hivTestTrackerManager.AddHivTestTracker(patient.PersonId, hivtest.FACILITY,
                                        "ANTIBODY", hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
                                        Convert.ToInt32(patient.ptn_pk), resultDate, hivtest.RESULT, hivtest.STRATEGY,
                                        hivtest.TYPE);
                }
            }
        }
        private static DateTime GetDateValueFromYYYYMMDD(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateValue))
                return dateValue;

            return new DateTime(1900, 1, 1);
        }


        public string ProcessIncomingShr(string _shrMessage)
        {
            TransactionLog log = new TransactionLog();
            log.TransactionDate = DateTime.Now;
            log.TransactionType = 1;
            log.UserId = 1;
            log.Request = _shrMessage;
            log.TranLogId = Guid.NewGuid();
            log.LogMessage = "Received";
           //int logId = _shrApiManager.LogPSmartRequest(log);
            //log.Id = logId;
            try
            {
                //save in transcation log
              

                string hts_number = "";
                string hts_number_issued_at = "";
                bool hasErrors = false;
                bool hasWarning = false;
                List<string> Errors = new List<string>();
                List<string> _warnings = new List<string>();
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
                Module htsModule = _moduleService.GetMstModueByName("HTS");
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
                string _card_issued_at = "";

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
                        _card_issued_at = internalIdentifier.Find(xx => xx.IDENTIFIER_TYPE == "CARD_SERIAL_NUMBER").ASSIGNING_FACILITY;
                    }
                    if (!internalIdentifier.Exists(xx => xx.IDENTIFIER_TYPE == "HTS_NUMBER"))
                    {

                        _warnings.Add("Missing HTS_NUMBER. Cannot be processed");
                        hts_number = "";
                        hasWarning = true;
                    }
                    else
                    {
                        hts_number = internalIdentifier.Find(xx => xx.IDENTIFIER_TYPE == "HTS_NUMBER").ID;
                        hts_number_issued_at = internalIdentifier.Find(xx => xx.IDENTIFIER_TYPE == "HTS_NUMBER").ASSIGNING_FACILITY;
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
                    log.LogMessage = String.Join(" | ", Errors.ToArray());
                   // _shrApiManager.UpdateRequestLog(log);
                    return $"Erros Encounters {String.Join(" | ", Errors.ToArray())}";
                }
               else
                {
                    if (hasWarning)
                    {
                        log.LogMessage = "Valid Message but with warnings. Ready to process. Warnings : " + String.Join(" | ", _warnings.ToArray());
                    }
                    else
                    {
                        log.LogMessage = "Valid Message. Ready to process";
                    }
                  //  _shrApiManager.UpdateRequestLog(log);
                  
                    // Module module = _moduleService.GetMstModueByName("HTC Module"); TODO enanble for OLD HTS ONLY:208 THE DEFAILT ID FOR NEW HTS MODULE 
                    //find if there is client record for the serial number if yes 
                    //var patient = _patientCoreService.GetPatient(_cardserialnumber);TODO:Finds patient in mst_patient
                    var patient = _patientIdentifierManager.GetPatientByCardSerialNumber(_cardserialnumber);
                    if(patient == null)
                    {
                        string firstName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME.Trim();
                        string middleName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME.Trim();
                        string lastName = entity.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        DateTime registrationDate = DateTime.Now; //.ToString("")
                        string dob = entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH;
                        string dobPrecision = (entity.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION == "ESTIMATED")  ? "1"  : "0";
                        string phone = entity.PATIENT_IDENTIFICATION.PHONE_NUMBER;
                        string gender = entity.PATIENT_IDENTIFICATION.SEX;
                        string landmark = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK;
                        string maritalStatus = entity.PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string village =  entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE;
                        string ward = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;
                        string subcounty = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;

                        string address = entity.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;
                        string HEID = "";
                        int personId = _mstPatientManager.AddMstPatient(firstName, middleName, lastName, registrationDate, GetDateValueFromYYYYMMDD( dob),
                            dobPrecision, phone, gender, landmark, maritalStatus, hts_number, htsModule.Id.ToString(),
                            _cardserialnumber,village, ward, subcounty, HEID, address, _card_issued_at, hts_number_issued_at);

                        //    var   patientlookup = _patientLookupManager.GetPatientByPersonId(personId);
                        patient = _patientManager.GetPatientEntityByPersonId(personId);
                        if (patient == null)
                        {
                            throw new Exception("Patient/Client details cannnot be processed");

                        }
                    }
                   
                    if (patient != null)
                    {
                        //(check in) , 
                        // Module modules = _moduleService.GetModuleByName("HTC Module");
                      //  WaitingQueue queue = _queueService.GetQueueByName("HTS Services (PSmart)");
                        
                        //to do create a psmart user or adjust the request from interactor to come with the logged in user
                     //   _queueService.QueuePatient(patient.Id, queue.QueueId, QueueStatus.Pending, QueuePriority.Normal,           htsModule.Id, 1);
                        //(update demographics

                        //
                        /* //HIV tracking table
                         *  PersonId, Date Tested, MFL, result, test type (Screening...), catergory (pcr, antibody), provider, strategy)
                         * //Immunizationtracking table
                         *  PersonId, Dategiven, antigen, provider, lot number, expiry date, next visit
                         * 
                         * */
                        //string PersonId = "";
                        
                        string _mother_cccNumbers = "";
                        string _mother_ccc_issued_at = "";
                        if (patientIdentification.MOTHER_DETAILS.MOTHER_IDENTIFIER.Exists(xx => xx.IDENTIFIER_TYPE == "CCC_NUMBER"))
                        {
                            DTO.PSmart.MOTHERIDENTIFIER mCCC = patientIdentification.MOTHER_DETAILS.MOTHER_IDENTIFIER.Find(x => x.IDENTIFIER_TYPE == "CCC_NUMBER");

                            _mother_cccNumbers = mCCC.ID;
                            _mother_ccc_issued_at = mCCC.ASSIGNING_FACILITY;
                        }

                        //    foreach (var motherDetail in patientIdentification.MOTHER_DETAILS.MOTHER_IDENTIFIER)
                        //{
                        //    if (motherDetail.IDENTIFIER_TYPE == "CCC_NUMBER")
                        //    {
                        //        _mother_cccNumbers = motherDetail.ID.ToString();
                        //    }
                        //}

                        if ((patient.ptn_pk.HasValue))
                        {
                            if (hivTests != null)
                            {

                                this.TrackHIVTest(patient, hivTests);
                                //HivTestTracker track = _hivTestTrackerManager.
                                //foreach (var hivtest in hivTests)
                                //{

                                //    //DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd",
                                //    //    CultureInfo.InvariantCulture);
                                //    // resultDate = resultDate.ToString("yyyy-MM-dd");

                                //    //_hivTestTrackerManager.AddHivTestTracker(Convert.ToInt32(patient.PersonId),
                                //    //    hivtest.FACILITY,
                                //    //    hivtest.STRATEGY, hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
                                //    //    Convert.ToInt32(patient.Id), resultDate, hivtest.RESULT, hivtest.STRATEGY,
                                //    //    hivtest.TYPE);
                                //}
                            }

                            if (immunizations != null)
                            {
                                this.ProcessImmunizationSegment(immunizations, patient);
                                //foreach (var immunization in immunizations)

                                //{
                                //    DateTime administeredDate = DateTime.ParseExact(immunization.DATE_ADMINISTERED,
                                //        "yyyyMMd", CultureInfo.InvariantCulture);
                                //    ProcessImmunizationSegment(administeredDate, immunization.NAME,
                                //        Convert.ToInt32(patient.Id), Convert.ToInt32(patient.ptn_pk.Value));
                                //}
                            }
                           // if()
                           if(!string.IsNullOrWhiteSpace( _mother_cccNumbers))
                            {
                             var motherEntity =   _patientIdentifierManager.GetPatientEntityByIdentifier("CCC_NUMBER", _mother_cccNumbers);
                                if(motherEntity== null)
                                {
                                    //new person
                                }
                                else
                                {
                                    //check the relationship
                                }
                            }
                            if (!string.IsNullOrEmpty(patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME))
                            {
                                string mfirstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME.Trim();
                                string mmiddleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME.Trim();
                                string mlastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME.Trim();
                                PatientLookupManager lookupManager = new PatientLookupManager();
                                
                                var mothers = lookupManager.GetPatientRelationshipView(patient.Id);
                                if(mothers== null || !mothers.Exists(m => m.RelativeFirstName == mfirstName && m.RelativeLastName == mlastName && m.Relationship== "Mother"))
                                {

                                    _mstPatientManager.ProcessMotherNames(mfirstName, mmiddleName, mlastName, _mother_cccNumbers, patient.ptn_pk.Value);
                                  //  int motherPersonId = personManager.AddPersonUiLogic(mfirstName, mmiddleName, mlastName, 0, 1);
                                  //  PersonRelationshipManager relationshipManager = new PersonRelationshipManager();
                                  //  LookupLogic lookupLogic = new LookupLogic();
                                  //LookupItemView item =  lookupLogic.GetItemIdByGroupAndItemName("Relationship", "Mother").FirstOrDefault(); ;
                                  //  if (item != null)
                                  //  {
                                  //      relationshipManager.AddPersonRelationship(patient.PersonId, motherPersonId, item.ItemId);
                                  //  }
                                }
                                //MotherDetailsView motherDetailsView = new MotherDetailsView()
                                //{
                                //    Ptn_pk = Convert.ToInt32(patient.ptn_pk.Value),
                                //    firstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME,
                                //    middleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME,
                                //    lastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME,
                                //};
                                //var pk = _motherDetailsViewManager.FindExistingMother(motherDetailsView);
                                //if (pk != null)
                                //{
                                //}
                                //else
                                //{
                                //    string mfirstName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.FIRST_NAME;
                                //    string mmiddleName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.MIDDLE_NAME;
                                //    string mlastName = patientIdentification.MOTHER_DETAILS.MOTHER_NAME.LAST_NAME;


                                //    _mstPatientManager.ProcessMotherNames(mfirstName, mmiddleName, mlastName,
                                //        _mother_cccNumbers, Convert.ToInt32(patient.Id));
                                //}

                            }


                        }
                    }
                   /* else
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

                                    if (hts_number == "missing")
                                    {
                                        HTSID = DateTime.Now.Ticks.ToString();
                                    }
                                    else { HTSID = x.ID; }
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
                        /*

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




                        int personId = _mstPatientManager.AddMstPatient(firstName, middleName, lastName, registrationDate, dob,
                            dobPrecision, phone, gender, landmark, maritalStatus, HTSID, 208.ToString(),
                            CARD_SERIAL_NO,
                            village, ward, subcounty, HEID, address, _card_issued_at);
                        */
                     //    var   patientlookup = _patientLookupManager.GetPatientByPersonId(personId);
                     /* var patientLookup =  _patientManager.CheckPersonEnrolled(personId);
                        if (patientLookup != null)
                        {
                            patient = patientLookup[0];
                            if (hivTests != null)
                            {
                                this.TrackHIVTest(patient, hivTests);
                                //foreach (var hivtest in hivTests)
                                //{
                                //    // DateTime resultDate=   DateTime.Parse(hivtest.DATE, CultureInfo.CreateSpecificCulture("fr-FR"));
                                //    DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd",
                                //        CultureInfo.InvariantCulture);
                                //    // resultDate = resultDate.ToString("yyyy-MM-dd");

                                //    _hivTestTrackerManager.AddHivTestTracker(Convert.ToInt32(msg), hivtest.FACILITY,
                                //        hivtest.STRATEGY, hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
                                //        Convert.ToInt32(msg), resultDate, hivtest.RESULT, hivtest.STRATEGY,
                                //        hivtest.TYPE);
                                //}
                            }

                            if (immunizations != null)
                            {
                                this.ProcessImmunizationSegment(immunizations, patient);
                                //foreach (var immunization in immunizations)

                                //{
                                //    DateTime administeredDate = DateTime.ParseExact(immunization.DATE_ADMINISTERED, "yyyyMMdd", CultureInfo.InvariantCulture);
                                //    ProcessImmunizationSegment(administeredDate, immunization.NAME,
                                //        Convert.ToInt32(msg), Convert.ToInt32(msg));
                                //}
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
                    }*/

                    return "Client Processed Successfully!";
                }

            }
            catch (Exception ex) {
               
                log.LogMessage += ex.Message;
               // _shrApiManager.UpdateRequestLog(log);
                return $"error occured {ex.Message}"; }
            finally
            {
                _shrApiManager.LogPSmartRequest(log);
            }
        }
        //void ProcessHivTestSegment(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy, string testCategory)
        //{
        //    _hivTestTrackerManager.AddHivTestTracker(personId, facilityMflCode, diagnosisMode, providerName, providerId, ptnpk, resultDate, testResult, strategy, testCategory);
        //}
        void ProcessImmunizationSegment(List<DTO.PSmart.IMMUNIZATION> immunizations, PatientEntity patient)
        {
            foreach (var immunization in immunizations)

            {
                DateTime administeredDate = DateTime.ParseExact(immunization.DATE_ADMINISTERED, "yyyyMMdd", CultureInfo.InvariantCulture);
                _immunizationTracker.AddImmunizationTracker(administeredDate, immunization.NAME, patient.PersonId, patient.ptn_pk.Value);
                //ProcessImmunizationSegment(administeredDate, immunization.NAME,
                //    Convert.ToInt32(msg), Convert.ToInt32(msg));
            }
        }
        //void ProcessImmunizationSegment(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk)
        //{
        //    _immunizationTracker.AddImmunizationTracker(dateAdministered, antigenAdministered, personId, ptnPk);
        //}
    }
}