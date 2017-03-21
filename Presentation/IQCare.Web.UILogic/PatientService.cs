using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Application.Presentation;
using Entities.Administration;
using Entities.FormBuilder;
using Interface.Clinical;
using Interface.FormBuilder;
using Entities.PatientCore;
using Interface.PatientCore;

namespace IQCare.Web.UILogic
{
    public class StaticFormMap
    {
        //public readonly List<StaticFormMap> FormUrl;
        public string ReferenceId { get; private set; }
        public string FormName { get; private set; }
        public string Url { get; private set; }
 
        public static List<StaticFormMap> FormUrl
        {
            get
            {
                CurrentSession session = CurrentSession.Current;
                List<StaticFormMap> map = new List<StaticFormMap>();
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "LABORATORY",
                    FormName = "Laboratory",
                    Url = session.Facility.PaperLess ? "~/Laboratory/LabRequestForm.aspx?key=" + Guid.NewGuid().ToString() : "~/Laboratory/LabRecordEntry.aspx?key=" + Guid.NewGuid().ToString()
                });
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "PHARMACY",
                    FormName = "Pharmacy",
                    Url = "~/Pharmacy/frmPharmacyForm.aspx?key=" + Guid.NewGuid().ToString()
                });


                map.Add(new StaticFormMap()
                {
                    ReferenceId = "SERVICE_REQUEST",
                    FormName = "Service Request",
                    Url = string.Format("~/ClinicalService/ServiceRecordEntry.aspx?key={0}&name=add", Guid.NewGuid().ToString())
                });
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "CONSUMABLES_ISSUANCE",
                    FormName = "Consumables Issuance",
                    Url = "~/Billing/frmBilling_BillingPanel.aspx?mode=clinical&key=" + Guid.NewGuid().ToString()
                });
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "ART_THERAPY",
                    FormName = "ART Therapy",
                    Url = "~/ClinicalForms/frmClinical_ARVTherapy.aspx"
                });
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "ART_HISTORY",
                    FormName = "ART History",
                    Url = "~/ClinicalForms/frmClinical_ARTHistory.aspx"
                });
                map.Add(new StaticFormMap()
                {
                    ReferenceId = "CCC_INITIAL_FOLLOWUP",
                    FormName = "Initial and Follow up Visits",
                    Url = "~/ClinicalForms/frmClinical_InitialFollowupVisit.aspx"

                });
                return map;
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class PatientService
    {
        public readonly Patient CurrentPatient;
        public readonly ServiceArea CurrentServiceArea;
        // public readonly List<EnrollmentService> Enrollment;
        public readonly DataTable Formset;
        public readonly List<StaticFormMap> FormUrl;

        public bool FormsLoaded
        {
            get
            {
                return this.formLoaded;
            }
        }
        protected bool formLoaded = false;
        PatientService()
        {
            CurrentPatient = null;
            CurrentServiceArea = null;
            Formset = null;

            // Enrollment = null;
            FormUrl = StaticFormMap.FormUrl;
        }
       
        public PatientService (int patientId)
        {
            //patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
           
            IPatientHome pHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            CurrentPatient= pHome.GetPatientById(patientId);
        }
        public PatientService(CurrentSession session, int patientId, int moduleId)
        {
            int locationId, userId;

            locationId = session.Facility.Id;
            patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
            userId = session.User.Id;
            IPatientHome pHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            this.CurrentPatient = pHome.GetPatientById(patientId);
            CurrentServiceArea = session.Facility.Modules.Where(m => m.Id == moduleId).FirstOrDefault();

            if (CurrentServiceArea != null && CurrentServiceArea.Clinical)
            {
                Formset = this.GetFormsForPatientAndModule(locationId, moduleId, userId, this.CurrentPatient);
                FormUrl = StaticFormMap.FormUrl;
                this.formLoaded = true;

            }
            else
            {
                this.formLoaded = false;
                Formset = null;
                FormUrl = null;
            }

        }

        public PatientService(int locationId, int userId, Patient p, ServiceArea s)
        {
            this.CurrentPatient = p;
            this.CurrentServiceArea = s;
            this.Formset = this.GetFormsForPatientAndModule(locationId, s.Id, userId, this.CurrentPatient);
            this.FormUrl = StaticFormMap.FormUrl;
        }
        public static PatientService SetCurrentPatient(CurrentSession session, int patientId, int moduleId)
        {

            return new PatientService(session, patientId, moduleId);

        }

        public static DataTable GetModuleForms(int locationId, int moduleId)
        {
            IPatientHome ptmhm = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            DataTable dtForms = ptmhm.GetModuleForms(moduleId, locationId);
            return dtForms;
        }

        /// <summary>
        /// Gets the forms for patient and module.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="patientAge">The patient age.</param>
        /// <param name="patientSex">The patient sex.</param>
        /// <returns></returns>
        DataTable GetFormsForPatientAndModule(int locationId, int moduleId, int userId, Patient patient)
        {
            IPatientHome ptmhm = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            List<StaticFormMap> formMap = StaticFormMap.FormUrl;
            DataTable dtForms = ptmhm.GetModuleForms(moduleId, locationId);
           
            List<FormRule> formRules = ptmhm.GetModuleFormsBusinessRule(moduleId, null, null);
            DataTable dt = this.ApplyBusinessRuleOnFormSet(ref dtForms, ref formRules, patient.Age, patient.Sex);
            //dtForms.DefaultView.ToTable();
            if (moduleId == 1 && patient.Age < 2) //pmtct
            {
                // to do move this to the model configuration, rules per forms and modules
                //remove ccc and anc forms if the patientage < 2 years
                //CCC_INITIAL_FOLLOWUP
                //ART_HISTORY
                //ART_THERAPY
                //ICF
                //ANC_Register_MOH_405
                //Postnatal_Register_MOH_406
                dt.DefaultView.RowFilter = "ReferenceId Not In('CCC_INITIAL_FOLLOWUP','ART_HISTORY','ART_THERAPY','ICF','ANC_Register_MOH_405','Postnatal_Register_MOH_406')";

            }

            DataTable dtFilterd = dt.DefaultView.ToTable();
            dtFilterd.Columns.Add(new DataColumn("Url", Type.GetType("System.String")));
            dtFilterd.Columns["Url"].DefaultValue = "";
            dtFilterd.AcceptChanges();
            foreach (DataRow row in dtFilterd.Rows)
            {

                if (!Convert.ToBoolean(row["Custom"]))
                {
                    StaticFormMap map = formMap.Where(m => m.ReferenceId == row["ReferenceId"].ToString()).FirstOrDefault();
                    if (map != null)
                    {

                        row["Url"] = string.Format("{0}?|{1}", map.Url, row["FeatureId"].ToString());
                    }
                    else
                    {
                        row["Deleted"] = 1;
                    }
                }
                else if (row["Code"].ToString() == "CARE_END")
                {
                    row["Url"] = string.Format("{0}|{1}", "~/Scheduler/frmScheduler_ContactCareTracking.aspx?", row["FeatureId"].ToString());
                }
                else
                {
                    row["Url"] = string.Format("~/ClinicalForms/CustomForm.aspx?|{0}", row["FeatureId"].ToString());
                }
                row.AcceptChanges();
            }
            dtFilterd.DefaultView.RowFilter = "Deleted <> 1";
            this.formLoaded = true;
            return dtFilterd.DefaultView.ToTable();
        }
        /// <summary>
        /// Gets the module for patient enrollment.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="patientAge">The patient age.</param>
        /// <param name="patientSex">The patient sex.</param>
        /// <returns></returns>
        public static List<ServiceArea> GetModuleForPatientEnrollment(List<ServiceArea> facilityServiceAreas, double patientAge, string patientSex)
        {

            if (facilityServiceAreas != null && facilityServiceAreas.Count > 0)
            {
                List<ServiceArea> result = FilterModuleByBusinessRules(facilityServiceAreas, patientAge, patientSex);

                return result;
            }
            return new List<ServiceArea>();
        }
        /// <summary>
        /// Applies the business rule on form set.
        /// </summary>
        /// <param name="theForms">The forms.</param>
        /// <param name="theRules">The rules.</param>
        /// <param name="patientAge">The patient age.</param>
        /// <param name="patientSex">The patient sex.</param>
        DataTable ApplyBusinessRuleOnFormSet(ref DataTable theForms, ref List<FormRule> theRules, double patientAge, string patientSex)
        {
            theForms.Columns.Add(new DataColumn("Deleted", Type.GetType("System.Int32")));
            theForms.Columns["Deleted"].DefaultValue = 0;
            theForms.AcceptChanges();
            foreach (DataRow row in theForms.Rows)
            {
                row["Deleted"] = 0;
                int formId = Convert.ToInt32(row["FormId"]);
                FormRule setOneAge = theRules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS" && r.FormId == formId).FirstOrDefault();
                List<FormRule> setOneFemale = theRules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_FEMALE" && r.FormId == formId).ToList();
                List<FormRule> setOneMale = theRules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_MALE" && r.FormId == formId).ToList();

                bool setOneFlag = true;
                bool setTwoFlag = true;
                if (setOneAge != null)
                {
                    if (setOneAge.MinValue != "0" || setOneAge.MaxValue != "0")
                    {
                        if (patientAge >= Convert.ToDouble(setOneAge.MinValue) && patientAge <= Convert.ToDouble(setOneAge.MaxValue))
                        {
                            //setOneFlag = true;
                        }
                        else
                        {
                            setOneFlag = false;
                        }
                        //rowFilter = string.Format("{0} Age >= {1} And Age <= {2} ", rowFilter, Convert.ToDecimal(setOneAge.MinValue), Convert.ToDecimal(setOneAge.MaxValue));
                    }
                }
                if (setOneFemale != null && setOneFemale.Count > 0 && setOneMale != null && setOneMale.Count > 0)
                {

                }
                else
                {

                    if (setOneFemale.Count > 0 && patientSex == "Male")
                    {
                        setOneFlag = false;
                    }
                    else if (setOneMale.Count > 0 && patientSex == "Female")
                    {
                        setOneFlag = false;
                    }
                }
                FormRule set2Age = theRules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS" && r.FormId == formId).FirstOrDefault();
                List<FormRule> set2Female = theRules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_FEMALE" && r.FormId == formId).ToList();
                List<FormRule> set2Male = theRules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_MALE" && r.FormId == formId).ToList();
                if (set2Age != null)
                {
                    if (patientAge >= Convert.ToDouble(set2Age.MinValue) && patientAge <= Convert.ToDouble(set2Age.MaxValue))
                    {

                    }
                    else
                    {
                        setTwoFlag = false;
                    }
                }
                if (set2Female != null && set2Female.Count > 0 && set2Male != null && set2Male.Count > 0)
                {

                }
                else
                {
                    if (set2Female.Count > 0 && patientSex == "Male")
                    {
                        setTwoFlag = false;
                    }
                    else if (set2Male.Count > 0 && patientSex == "Female")
                    {
                        setTwoFlag = false;
                    }
                }

                if (!setTwoFlag || !setOneFlag)
                {
                    row["Deleted"] = 1;
                    row.AcceptChanges();
                }
                theForms.AcceptChanges();

            }
            theForms.DefaultView.RowFilter = "Deleted <> 1";
            return theForms.DefaultView.ToTable();

        }

        public static DataTable GetPatientEnrollmentDetails(int patientId, int locationId)
        {
            DataTable dt = new DataTable();
            IPatientRegistration PatientManager;
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            dt = PatientManager.GetPatientServiceLines(patientId, locationId);
            return dt;
        }
        /// <summary>
        /// Binds the module by business rules.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="patientAge">The patient age.</param>
        /// <param name="patientSex">The patient sex.</param>
        public static List<ServiceArea> FilterModuleByBusinessRules(List<ServiceArea> dt, double patientAge, string patientSex)
        {
            IModule moduleMgr = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");

            List<ServiceRule> rules = moduleMgr.GetBusinessRule(null);
            List<ServiceArea> list = dt.Where(r => r.EnrolFlag == true).ToList();
            list.RemoveAll(item =>
            {
                int moduleId = item.Id;
                ServiceRule setOneAge = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS" && r.ServiceAreaId == moduleId).FirstOrDefault();
                List<ServiceRule> setOneFemale = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_FEMALE" && r.ServiceAreaId == moduleId).ToList();
                List<ServiceRule> setOneMale = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_MALE" && r.ServiceAreaId == moduleId).ToList();
                bool setOneFlag = true;
                bool setTwoFlag = false;
                if (setOneAge != null)
                {
                    if (setOneAge.MinValue != "0" || setOneAge.MaxValue != "0")
                    {
                        if (patientAge >= Convert.ToDouble(setOneAge.MinValue) && patientAge <= Convert.ToDouble(setOneAge.MaxValue))
                        {

                        }
                        else
                        {
                            setOneFlag = false;
                        }

                    }
                }
                if (setOneFemale != null && setOneFemale.Count > 0 && setOneMale != null && setOneMale.Count > 0)
                {

                }
                else
                {

                    if (setOneFemale.Count > 0 && patientSex == "Male")
                    {
                        setOneFlag = false;
                    }
                    else if (setOneMale.Count > 0 && patientSex == "Female")
                    {
                        setOneFlag = false;
                    }
                }

                ServiceRule set2Age = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS" && r.ServiceAreaId == moduleId).FirstOrDefault();
                List<ServiceRule> set2Female = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_FEMALE" && r.ServiceAreaId == moduleId).ToList();
                List<ServiceRule> set2Male = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_MALE" && r.ServiceAreaId == moduleId).ToList();
                if (set2Age != null)
                {
                    if (patientAge >= Convert.ToDouble(set2Age.MinValue) && patientAge <= Convert.ToDouble(set2Age.MaxValue))
                    {

                    }
                    else
                    {
                        setTwoFlag = false;
                    }
                }
                if (set2Female != null && set2Female.Count > 0 && set2Male != null && set2Male.Count > 0)
                {

                }
                else
                {
                    if (set2Female.Count > 0 && patientSex == "Male")
                    {
                        setTwoFlag = false;
                    }
                    else if (set2Male.Count > 0 && patientSex == "Female")
                    {
                        setTwoFlag = false;
                    }
                }
                return (!setTwoFlag && !setOneFlag);
            });


            return list;


        }

        public static bool ValidatePatientDate(DateTime dateOfBirth, DateTime dateOfReg, int patientId)
        {
            IPatientService ps = (IPatientService)ObjectFactory.CreateInstance("BusinessProcess.PatientCore.PatientCoreServices, BusinessProcess.PatientCore");
            PatientVisit lastVisit = ps.GetPatientLastVisit(patientId);
            if (lastVisit == null)
            {
                return true;
            }
            if (lastVisit.VisitDate < dateOfBirth) return false;
            if (lastVisit.VisitDate < dateOfReg) return false;

            return true;
        }

        /// <summary>
        /// Finds the patient.
        /// </summary>
        /// <param name="facilityId">The facility identifier.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="middlename">The middlename.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="enrollment">The enrollment.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="status">The status.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="registrationDate">The registration date.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="maxRecords">The maximum records.</param>
        /// <returns></returns>
        public static DataTable FindPatient(int facilityId, string lastname, string middlename, string firstname,
            string enrollment,
            string gender,
            string status,
            DateTime? dob,
            DateTime? registrationDate,           
            int moduleId = 999,
            int maxRecords = 100,
             string phoneNumber ="")
        {
            IPatientRegistration pMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");

            string rowFilter = "";
            string rowFilter2 = "";
            string ruleOne = "";
            string ruleOneAge = "";
            string ruleTwoAge = "";
            string rule1Sex = "";
            string rule2Sex = "";
            string ruletwo = "";
            if (moduleId < 999)
            {

                IModule moduleMgr = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                List<ServiceRule> rules = moduleMgr.GetBusinessRule(moduleId);

                if (rules != null && rules.Count > 0)
                {

                    ServiceRule setOneAge = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS").FirstOrDefault();
                    List<ServiceRule> setOneFemale = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_FEMALE").ToList();
                    List<ServiceRule> setOneMale = rules.Where(r => r.RuleSet == 1 && r.RuleReferenceId == "ACTIVE_MALE").ToList();
                    if (setOneAge != null)
                    {
                        if (setOneAge.MinValue != "" || setOneAge.MaxValue != "")
                        {
                            rowFilter = string.Format("{0} Age >= {1} And Age <= {2} ", rowFilter, Convert.ToDecimal(setOneAge.MinValue), Convert.ToDecimal(setOneAge.MaxValue));
                            ruleOne = string.Format("And (convert(varchar,round(cast(datediff(dd,DOB,DateofDeath)/365.25 as decimal(5,2)),2))) between {0} and {1})", setOneAge.MinValue, setOneAge.MaxValue);
                            ruleOneAge = string.Format(" (Age between {0} and {1}) ", setOneAge.MinValue, setOneAge.MaxValue);
                        }
                    }
                    if (setOneFemale != null && setOneFemale.Count > 0 && setOneMale != null && setOneMale.Count > 0)
                    {

                    }
                    else
                    {

                        if (setOneFemale.Count > 0)
                        {
                            if (rowFilter != "") rowFilter = rowFilter + " and ";
                            rowFilter = string.Format("{0} Sex = '{1}' ", rowFilter, "Female");
                            ruleOne += " And (Sex = 17) ";
                            rule1Sex = "(Sex = 'Female')";
                        }
                        else if (setOneMale.Count > 0)
                        {
                            if (rowFilter != "") rowFilter = rowFilter + " and ";
                            rowFilter = string.Format("{0} Sex = '{1}' ", rowFilter, "male");
                            ruleOne += " And (Sex = 16) ";
                            rule1Sex = "(Sex = 'Male')";
                        }
                    }

                    ServiceRule set2Age = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_AGE_RANGE_YEARS").FirstOrDefault();
                    List<ServiceRule> set2Female = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_FEMALE").ToList();
                    List<ServiceRule> set2Male = rules.Where(r => r.RuleSet == 2 && r.RuleReferenceId == "ACTIVE_MALE").ToList();
                    if (set2Age != null)
                    {
                        if (set2Age.MinValue != "" || set2Age.MaxValue != "")
                        {
                            rowFilter2 = string.Format("{0} Age >= {1} And Age <= {2} ", rowFilter2, Convert.ToDecimal(set2Age.MinValue), Convert.ToDecimal(set2Age.MaxValue));
                            ruletwo = string.Format("And (convert(varchar,round(cast(datediff(dd,DOB,DateofDeath)/365.25 as decimal(5,2)),2))) between {0} and {1})", setOneAge.MinValue, setOneAge.MaxValue);
                            ruleTwoAge = string.Format(" (Age between {0} and {1})", set2Age.MinValue, set2Age.MaxValue);

                        }
                        if (set2Female != null && set2Female.Count > 0 && set2Male != null && set2Male.Count > 0)
                        {

                        }
                        else
                        {
                            if (set2Female.Count > 0)
                            {
                                if (rowFilter2 != "") rowFilter2 = rowFilter2 + " and ";
                                rowFilter2 = string.Format("{0} Sex = '{1}' ", rowFilter2, "Female");
                                ruletwo += " And (Sex = 17) ";
                                rule2Sex = " (Sex = 'Female') ";
                            }
                            else if (set2Male.Count > 0)
                            {
                                if (rowFilter2 != "") rowFilter2 = rowFilter2 + " and ";
                                rowFilter2 = string.Format("{0} Sex = '{1}' ", rowFilter2, "male");
                                ruletwo += " And (Sex = 16) ";
                                rule2Sex = " (Sex = 'Male') ";
                            }
                        }

                        string mainRowFilter = rowFilter;
                        if (rowFilter2 != "") mainRowFilter = mainRowFilter + " or (" + rowFilter2 + " )";


                        //dtPatient.DefaultView.RowFilter = mainRowFilter;
                    }
                }
                ruleOne = ruleOneAge.Trim() + (rule1Sex.Trim() != "" ? " And " + rule1Sex.Trim() : "");
                ruletwo = ruleTwoAge.Trim() + (rule2Sex.Trim() != "" ? " And " + rule2Sex.Trim() : "");

                if (ruleOne.Trim() != "" && ruletwo.Trim() != "")
                {
                    rowFilter = string.Format(" Where ( {0} or ( {1}))", ruleOne, ruletwo);
                }
                else if (ruleOne.Trim() != "")
                {
                    rowFilter = " Where " + ruleOne;
                }
                else if (ruletwo.Trim() != "")
                {
                    rowFilter = " Where " + ruletwo;
                }
            }
            DataTable dtPatient = pMgr.GetPatientSearchResults(facilityId, lastname, middlename, firstname, enrollment, gender, status, dob, registrationDate, moduleId, maxRecords, rowFilter,phoneNumber);
            // DataTable dt = dtPatient.DefaultView.ToTable();
            return dtPatient;

        }
    }

}


