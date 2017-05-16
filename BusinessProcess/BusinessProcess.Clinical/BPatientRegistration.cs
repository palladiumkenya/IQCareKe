using System;
using System.Collections;
using System.Data;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Clinical;
using Entities.Administration;
using System.Linq;
using System.Collections.Generic;
using Entities.FormBuilder;
//using DataAccess.Common;

/// <summary>
///
/// </summary>
namespace BusinessProcess.Clinical
{
    /// <summary>
    ///
    /// </summary>
    public class BPatientRegistration : ProcessBase, IPatientRegistration
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BPatientRegistration"/> class.
        /// </summary>
        public BPatientRegistration()
        {
        }

        #endregion "Constructor"

        /// <summary>
        /// Changes the waiting list status.
        /// </summary>
        /// <param name="WaitingListID">The waiting list identifier.</param>
        /// <param name="RowStatus">The row status.</param>
        /// <param name="UserID">The user identifier.</param>
        public void ChangeWaitingListStatus(int WaitingListID, int RowStatus, int UserID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@WaitingListID", SqlDbType.Int, WaitingListID.ToString());
                ClsUtility.AddParameters("@RowStatus", SqlDbType.Int, RowStatus.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsObject WaitingListManager = new ClsObject();
                WaitingListManager.ReturnObject(ClsUtility.theParams, "pr_WaitingListChangePatientStatus", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        /// <summary>
        /// Checks the duplicate identifiervaule.
        /// </summary>
        /// <param name="Columnname">The columnname.</param>
        /// <param name="Columnvalue">The columnvalue.</param>
        /// <returns></returns>
        public DataTable CheckDuplicateIdentifiervaule(string Columnname, string Columnvalue)
        {
            try
            {
                lock (this)
                {
                    ClsObject PatientHistory = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Columnname", SqlDbType.Int, Columnname.ToString());
                    ClsUtility.AddParameters("@Columnvalue", SqlDbType.Int, Columnvalue.ToString());
                    return (DataTable)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckDuplicateIdentifiervaule_Future", ClsUtility.ObjectEnum.DataTable);
                }
            }
            catch //Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Checks the identity.
        /// </summary>
        /// <param name="ExposedInfantId">The exposed infant identifier.</param>
        /// <returns></returns>
        public DataSet CheckIdentity(string ExposedInfantId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckIdentityInfant", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        /// <param name="dob">The dob.</param>
        /// <param name="regdate">The regdate.</param>
        /// <returns></returns>
        public DataSet GetAge(DateTime dob, DateTime regdate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@dob", SqlDbType.DateTime, dob.ToString());
                ClsUtility.AddParameters("@regdate", SqlDbType.DateTime, regdate.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_GetDataDiff", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets all drop downs.
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetPatientEnrollmentDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the duplicate patient search results.
        /// </summary>
        /// <param name="lastname">The lastname.</param>
        /// <param name="middlename">The middlename.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="address">The address.</param>
        /// <param name="phone">The phone.</param>
        /// <returns></returns>
        public DataSet GetDuplicatePatientSearchResults(string lastname, string middlename, string firstname, string address, string phone)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                ClsUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                ClsUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, address.ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, phone.ToString());
                //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetDuplicatePatientSearchresults_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the enrolment.
        /// </summary>
        /// <param name="CountryID">The country identifier.</param>
        /// <param name="PossitionID">The possition identifier.</param>
        /// <param name="SatelliteID">The satellite identifier.</param>
        /// <param name="PatientClinicID">The patient clinic identifier.</param>
        /// <param name="enrolmentid">The enrolmentid.</param>
        /// <param name="deleteflag">The deleteflag.</param>
        /// <returns></returns>
        public DataSet GetEnrolment(string CountryID, string PossitionID, string SatelliteID, string PatientClinicID, string enrolmentid, int deleteflag)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Countryid", SqlDbType.Int, CountryID.ToString());
                ClsUtility.AddParameters("@Posid", SqlDbType.Int, PossitionID.ToString());
                ClsUtility.AddParameters("@Satelliteid", SqlDbType.Int, SatelliteID.ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                ClsUtility.AddParameters("@enrolmentid", SqlDbType.VarChar, enrolmentid.ToString());
                ClsUtility.AddParameters("@deleteflag", SqlDbType.Int, deleteflag.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SelectEnrollment", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the maximum automatic populate identifier.
        /// </summary>
        /// <param name="columnname">The columnname.</param>
        /// <returns></returns>
        public DataSet GetMaxAutoPopulateIdentifier(string columnname)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@columnname", SqlDbType.VarChar, columnname);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetMaxAutopopulatIdentifier", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the module names.
        /// </summary>
        /// <param name="FacilityID">The facility identifier.</param>
        /// <returns></returns>
        public DataSet GetModuleNames(int FacilityID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@facilityid", SqlDbType.Int, FacilityID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectModulesByFacilityID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient enroll.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientEnroll(string patientid, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetEnrollment_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient record.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientRecord(int PatientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject RecordMgr = new ClsObject();
                return (DataTable)RecordMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRecord_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patient registration.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="VisitType">Type of the visit.</param>
        /// <returns></returns>
        public DataSet GetPatientRegistration(int patientid, int VisitType)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRegistration_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient search results.
        /// </summary>
        /// <param name="FId">The f identifier.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="middlename">The middlename.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="enrollment">The enrollment.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="status">The status.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="registrationDate">The registration date.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="MaxRecords">The maximum records.</param>
        /// <returns></returns>
        public DataTable GetPatientSearchResults(
            int FId,
            string lastname,
            string middlename,
            string firstname,
            string enrollment,
            string gender,
            string status,
            DateTime? dob,
            DateTime? registrationDate,
            int ModuleId = 999,
            int MaxRecords = 100,
            string ruleFilter="",
             string phoneNumber ="")
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                if (FId > 0)
                {
                    ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FId.ToString());
                }

                if (!string.IsNullOrEmpty(lastname.Trim()))
                    ClsUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.Trim().ToString());
                if (!string.IsNullOrEmpty(middlename.Trim()))
                    ClsUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.Trim().ToString());
                if (!string.IsNullOrEmpty(firstname.Trim()))
                    ClsUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.Trim().ToString());
                if (!string.IsNullOrEmpty(enrollment.Trim()))
                    ClsUtility.AddParameters("@enrollmentid", SqlDbType.VarChar, enrollment.Trim().ToString());
                if (!string.IsNullOrEmpty(gender.Trim()))
                    ClsUtility.AddParameters("@Sex", SqlDbType.Int, gender.ToString());
                if (dob.HasValue)
                    ClsUtility.AddParameters("@dob", SqlDbType.DateTime, dob.Value.ToString("yyyy-MM-dd"));
                if (registrationDate.HasValue)
                    ClsUtility.AddParameters("@RegistrationDate", SqlDbType.DateTime, registrationDate.Value.ToString("yyyy-MM-dd"));
                if (!string.IsNullOrEmpty(status.Trim()))
                    ClsUtility.AddParameters("@status", SqlDbType.Int, status.ToString());
                if (ModuleId > 0)
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                if (!string.IsNullOrEmpty(phoneNumber))
                    ClsUtility.AddParameters("@PhoneNumber", SqlDbType.VarChar, phoneNumber.Trim());
               // ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddExtendedParameters("@top", SqlDbType.Int, MaxRecords);
                ClsUtility.AddExtendedParameters("@RuleFilter", SqlDbType.VarChar, ruleFilter);
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(ClsUtility.theParams, "dbo.Pr_Clinical_GetPatientSearchresults", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patient search results.
        /// </summary>
        /// <param name="FId">The f identifier.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="middlename">The middlename.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="enrollment">The enrollment.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="status">The status.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientSearchResults(int FId, string lastname, string middlename, string firstname, string enrollment, string gender, DateTime dob, string status, int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FId.ToString());
                ClsUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                ClsUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                ClsUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                ClsUtility.AddParameters("@enrollmentid", SqlDbType.VarChar, enrollment.ToString());
                //ClsUtility.AddParameters("@hospitalno", SqlDbType.VarChar, hospitalno.ToString());
                ClsUtility.AddParameters("@gender", SqlDbType.VarChar, gender.ToString());
                //ClsUtility.AddParameters("@dobexact", SqlDbType.Int, dobexact.ToString());
                //ClsUtility.AddParameters("@dobestimate", SqlDbType.Int, dobestimate.ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.DateTime, dob.ToString());
                ClsUtility.AddParameters("@status", SqlDbType.VarChar, status.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                //  ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientSearchresults_COnstella", ClsUtility.ObjectEnum.DataSet);
                //return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientSearchresults_Naveen", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient service lines.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientServiceLines(int PatientID, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject FieldMgr = new ClsObject();
                return (DataTable)FieldMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatient_EnrolledServiceLines", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patients on waiting list.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientsOnWaitingList(int ListID, int ModuleID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ListID", SqlDbType.Int, ListID.ToString());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataTable)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_WaitingList_GetPatientsOnWaitingList", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patient technical area details.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="ModuleName">Name of the module.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientTechnicalAreaDetails(int patientid, string ModuleName, int ModuleID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Modulename", SqlDbType.VarChar, ModuleName.ToString().TrimEnd());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientTechnicalAreaDetails_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the visit date_ ielab.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet GetVisitDate_IELAB(int patientid, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIELAB_VisitDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Thes the visit iddt.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <returns></returns>
        public DataTable theVisitIDDT(string patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.VarChar, patientid.ToString());
                ClsObject VisitIDMgr = new ClsObject();
                return (DataTable)VisitIDMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetVisitIDEnrolment_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        #region "AidsRelief"

        /// <summary>
        /// Inserts the update patient registration.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="dtCaretype">The dt caretype.</param>
        /// <param name="dtARTsponsor">The dt ar tsponsor.</param>
        /// <param name="dt">The dt.</param>
        /// <param name="dtBarrier">The dt barrier.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <param name="dataquality">The dataquality.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <returns></returns>
        public DataSet InsertUpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                int Rowsaffected;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                ClsUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                ClsUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                ClsUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                //ClsUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                //ClsUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                ClsUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                ClsUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                ClsUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                ClsUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                ClsUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOthers", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                ClsUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                ClsUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                ClsUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                ClsUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                ClsUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                ClsUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                ClsUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                ClsUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                ClsUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                ClsUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                ClsUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                ClsUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                ClsUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet DSRowsReturned = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_InsertUpdateEnrollmentHIVCare_Constella", ClsUtility.ObjectEnum.DataSet);
                VisitID = Convert.ToInt32(DSRowsReturned.Tables[0].Rows[0]["VisitID"].ToString());

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                DataTable DTtempcaretype = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteCareType_Constella", ClsUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //returning ARTSponsor;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempartsponsor = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteARTSponsor_Constella", ClsUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Disclosure
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                Rowsaffected = (int)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());
                    int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //returning Barrier Manager
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempbarrier = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteBarrier_Constella", ClsUtility.ObjectEnum.DataTable);
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", VisitID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return DSRowsReturned;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the new registration.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="dtCaretype">The dt caretype.</param>
        /// <param name="dtARTsponsor">The dt ar tsponsor.</param>
        /// <param name="dt">The dt.</param>
        /// <param name="dtBarrier">The dt barrier.</param>
        /// <param name="dataquality">The dataquality.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <returns></returns>
        public DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                ClsUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                ClsUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                ClsUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                ClsUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                ClsUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                ClsUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                ClsUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                ClsUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                ClsUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOthers", SqlDbType.Int, "");
                ClsUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                ClsUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                ClsUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                ClsUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                ClsUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                ClsUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                ClsUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                ClsUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                ClsUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                ClsUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                ClsUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                ClsUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                ClsUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                ClsUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollment_Constella", ClsUtility.ObjectEnum.DataTable);

                //returning CareType;
                //////DataMgr.CommitTransaction(this.Transaction);
                //////DataMgr.ReleaseConnection(this.Connection);

                ////////returning CareType;
                //////this.Connection = DataMgr.GetConnection();
                //////this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //ClsObject CareManager = new ClsObject();
                int intflag = 0;
                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ClsObject ARTSponsorMgr = new ClsObject();
                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //HIV Disclosure Section
                //ClsObject DiscloseManager = new ClsObject();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //ClsObject BarrierManager = new ClsObject();
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", dtp.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", dtp.Rows[0][1].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the new registration.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="dataquality">The dataquality.</param>
        /// <returns></returns>
        public DataTable SaveNewRegistration(Hashtable ht, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                //ClsUtility.AddParameters("@PatientImage", SqlDbType.VarChar, ht["PatientImage"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SavePatientRegistration_Constella", ClsUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the update technical area.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <returns></returns>
        public DataTable SaveUpdateTechnicalArea(Hashtable ht, int VisitID)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@flag", SqlDbType.VarChar, ht["flag"].ToString());
                ClsUtility.AddParameters("@Action", SqlDbType.VarChar, ht["Action"].ToString());
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                ClsUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                ClsUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                ClsUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString());
                ClsUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                ClsUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, ht["VisitType"].ToString());

                theDS = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateTechnicalArea_Constella", ClsUtility.ObjectEnum.DataSet);
                return theDS.Tables[0];
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Updates the patient registration.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="Ptn_Pk">The PTN_ pk.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <param name="dataquality">The dataquality.</param>
        /// <returns></returns>
        public int UpdatePatientRegistration(Hashtable ht, int Ptn_Pk, int VisitID, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                int RowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdatePatientRegistration_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                ////////DataMgr.CommitTransaction(this.Transaction);
                ////////DataMgr.ReleaseConnection(this.Connection);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion "AidsRelief"

        #region "CTC"

        /// <summary>
        /// Gets the drug generic CTC.
        /// </summary>
        /// <returns></returns>
        public DataSet GetDrugGenericCTC()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CTCDrugManager = new ClsObject();
                return (DataSet)CTCDrugManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetDrugGenericPatientRegistrationCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient details CTC.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="CountryID">The country identifier.</param>
        /// <param name="PosID">The position identifier.</param>
        /// <param name="SatelliteID">The satellite identifier.</param>
        /// <param name="PatientClinicID">The patient clinic identifier.</param>
        /// <param name="Existflag">The existflag.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientDetailsCTC(string patientId, string CountryID, string PosID, string SatelliteID, string PatientClinicID, int Existflag, int VisitID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, patientId);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID);
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID);
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                ClsUtility.AddParameters("@ExistFlag", SqlDbType.Int, Existflag.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@password", SqlDbType.Int, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);

                // DataMgr.CommitTransaction(this.Transaction);
                //  DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the patient registration CTC.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="Flag">The flag.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <returns></returns>
        public DataTable SavePatientRegistrationCTC(Hashtable ht, int Flag, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;
                //int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@PatientEnrolID", SqlDbType.VarChar, ht["EnrolmentID"].ToString());
                ClsUtility.AddParameters("@FileRefID", SqlDbType.VarChar, ht["FileReferenceID"].ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@RegDate", SqlDbType.Int, ht["RegDate"].ToString());
                ClsUtility.AddParameters("@Gender", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@Maristatus", SqlDbType.VarChar, ht["Maristatus"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@ConDetails", SqlDbType.VarChar, ht["ConDetails"].ToString());
                ClsUtility.AddParameters("@Region", SqlDbType.VarChar, ht["Region"].ToString());
                ClsUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                ClsUtility.AddParameters("@Division", SqlDbType.VarChar, ht["Division"].ToString());
                ClsUtility.AddParameters("@Ward", SqlDbType.VarChar, ht["Ward"].ToString());
                ClsUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                ClsUtility.AddParameters("@TCLLeader", SqlDbType.VarChar, ht["TCLLeader"].ToString());
                ClsUtility.AddParameters("@TCLContact", SqlDbType.VarChar, ht["TCLContact"].ToString());
                ClsUtility.AddParameters("@HHead", SqlDbType.VarChar, ht["HHead"].ToString());
                ClsUtility.AddParameters("@Hcontact", SqlDbType.VarChar, ht["Hcontact"].ToString());
                ClsUtility.AddParameters("@SupportName", SqlDbType.VarChar, ht["SupportName"].ToString());
                ClsUtility.AddParameters("@TsAddress", SqlDbType.VarChar, ht["TsAddress"].ToString());
                ClsUtility.AddParameters("@TsPhone", SqlDbType.VarChar, ht["TsPhone"].ToString());
                ClsUtility.AddParameters("@ComSOrganisation", SqlDbType.VarChar, ht["ComSOrganisation"].ToString());
                ClsUtility.AddParameters("@FirstHIVPosTestDate", SqlDbType.VarChar, ht["PosHivTest"].ToString());
                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, ht["ConfirmHivPositive"].ToString());
                ClsUtility.AddParameters("@DrugAllery", SqlDbType.VarChar, ht["DrugAllery"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromOther", SqlDbType.VarChar, ht["ReferredFromOther"].ToString());
                ClsUtility.AddParameters("@PriorExposure", SqlDbType.VarChar, ht["PriorExposure"].ToString());
                ClsUtility.AddParameters("@ArtStartDate", SqlDbType.VarChar, ht["ArtStartDate"].ToString());
                ClsUtility.AddParameters("@WhyEligible", SqlDbType.VarChar, ht["WhyEligible"].ToString());
                ClsUtility.AddParameters("@InitialRegCode", SqlDbType.VarChar, ht["InitialRegimenCode"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["InitialRegimenAbb"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                ClsUtility.AddParameters("@FunStatus", SqlDbType.VarChar, ht["FunStatus"].ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                ClsUtility.AddParameters("@CD4", SqlDbType.VarChar, ht["CD4"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["CD4Percent"].ToString());
                ClsUtility.AddParameters("@TLC", SqlDbType.VarChar, ht["TLC"].ToString());
                ClsUtility.AddParameters("@TLCPercent", SqlDbType.VarChar, ht["TLCPercent"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataTable dtp = new DataTable();
                DataSet objDs = new DataSet();
                if (ht["Update"].ToString() == "1")
                {
                    ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                else
                {
                    ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", objDs.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", objDs.Tables[1].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Thes the dropdown.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="Flag">The flag.</param>
        /// <returns></returns>
        public DataSet theDropdown(string ID, string Flag)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.VarChar, ID);
                ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, Flag);
                ClsObject DDMgr = new ClsObject();
                return (DataSet)DDMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_DropDownCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion "CTC"

        #region "PMTCT"

        /// <summary>
        /// Deletes the infant information.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int DeleteInfantInfo(int PatientId, int UserID)
        {
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteInfantInfo_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Gets the child detail.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet GetChildDetail(int patientId, int locationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@patientid", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetChildDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient registration PMTCT.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientRegistrationPMTCT(int PatientId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                //ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());

                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, "11");
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetRegistrationPMTCT_Constella", ClsUtility.ObjectEnum.DataSet);

                //  DataMgr.CommitTransaction(this.Transaction);
                //DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        //    return null;
        //}
        /// <summary>
        /// Saves the infant information.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="VisitId">The visit identifier.</param>
        /// <param name="ParentId">The parent identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public DataSet SaveInfantInfo(int patientId, int locationId, int visitId, int parentId, int userId)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Ptnpk", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@VisitPk", SqlDbType.Int, visitId);
                ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                ClsUtility.AddExtendedParameters("@ParentID", SqlDbType.Int, parentId);
                ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveInfantInfo_Futures", ClsUtility.ObjectEnum.DataSet);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the patient registration PMTCT.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <returns></returns>
        public DataTable SavePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManagerPMTCT = new ClsObject();
                PatientManagerPMTCT.Connection = this.Connection;
                PatientManagerPMTCT.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, Convert.ToInt32(ht["PatientID"]));
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LName"].ToString());
                ClsUtility.AddExtendedParameters("@RegDate", SqlDbType.DateTime, ht["RegDate"].ToString());
                ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, Convert.ToInt32(ht["Gender"].ToString()));
                ClsUtility.AddExtendedParameters("@DOB", SqlDbType.DateTime, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@MStatus", SqlDbType.VarChar, ht["MStatus"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["TransferIn"].ToString());
                ClsUtility.AddParameters("@RefFrom", SqlDbType.VarChar, ht["RefFrom"].ToString());
                ClsUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                ClsUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                ClsUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                ClsUtility.AddParameters("@HEIIDNumber", SqlDbType.VarChar, ht["HEIIDNumber"].ToString());
                ClsUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                ClsUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());

                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, Convert.ToInt32(ht["LocationID"]));
                ClsUtility.AddExtendedParameters("@DataQuality", SqlDbType.Int, Convert.ToInt32(ht["DataQuality"]));
                ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, Convert.ToInt32(ht["UserID"]));
                //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddExtendedParameters("@visittype", SqlDbType.Int, Convert.ToInt32(ht["VisitType"]));

                if (ht["PatientID"].ToString() == "0" || ht["PatientID"].ToString() == "0") //add mode only
                {
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                    ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString()); //only in insert mode,should not be updated in update mode
                    ClsUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                    ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                }

                theDS = (DataSet)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollmentFrmPMTCT_Constella", ClsUtility.ObjectEnum.DataSet);

                DataTable dtp = new DataTable();
                dtp = theDS.Tables[0];
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", theDS.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;

                //            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                //            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                //            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                //            theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                //            theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                //            ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                //            int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //        }

                //        DataMgr.CommitTransaction(this.Transaction);
                //        DataMgr.ReleaseConnection(this.Connection);
                //        return dtp;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Validates the specified argument.
        /// </summary>
        /// <param name="Argument">The argument.</param>
        /// <param name="Flag">The flag.</param>
        /// <returns></returns>
        public DataTable Validate(string Argument, string Flag)
        {
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrValidatePMTCT = new ClsObject();
                MgrValidatePMTCT.Connection = this.Connection;
                MgrValidatePMTCT.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Argument", SqlDbType.VarChar, Argument.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                return theDT = (DataTable)MgrValidatePMTCT.ReturnObject(ClsUtility.theParams, "pr_Clinical_ValidateEnrollmentPMTCT_Constella", ClsUtility.ObjectEnum.DataTable);
                //DataMgr.CommitTransaction(this.Transaction);
                // DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        

        #endregion "PMTCT"

        #region "ExposedInfant"

        /// <summary>
        /// Deletes the exposed infant by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public int DeleteExposedInfantById(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsObject VisitManager = new ClsObject();
                int theRowAffected = 0;
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteExposedInfantById", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the exposed infant by parent identifier.
        /// </summary>
        /// <param name="Ptn_Pk">The PTN_ pk.</param>
        /// <returns></returns>
        public DataSet GetExposedInfantByParentId(int Ptn_Pk)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExposedInfantByParentId", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the exposed infant.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="Ptn_Pk">The PTN_ pk.</param>
        /// <param name="ExposedInfantId">The exposed infant identifier.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="DOB">The dob.</param>
        /// <param name="FeedingPractice3mos">The feeding practice3mos.</param>
        /// <param name="CTX2mos">The ct x2mos.</param>
        /// <param name="HIVTestType">Type of the hiv test.</param>
        /// <param name="HIVResult">The hiv result.</param>
        /// <param name="FinalStatus">The final status.</param>
        /// <param name="DeathDate">The death date.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName.ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName.ToString());
                ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                ClsUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
                ClsUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
                ClsUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
                ClsUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
                ClsUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID.ToString());
                if (DeathDate != null)
                {
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, DeathDate == null ? null : DeathDate.ToString());
                }
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion "ExposedInfant"

        #region "Technical Areas - Added Naveen -28-Oct-2010"

        /// <summary>
        /// Gets the field names.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetFieldNames(int ModuleID, int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@moduleId", SqlDbType.Int, ModuleID.ToString());
                ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetModuleFieldNames_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion "Technical Areas - Added Naveen -28-Oct-2010"

        #region "Dynamic Registration"

        /// <summary>
        /// Common_s the get save updatefor custom registrion.
        /// </summary>
        /// <param name="Insert">The insert.</param>
        /// <returns></returns>
        public DataSet Common_GetSaveUpdateforCustomRegistrion(string Insert)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet GetValue = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomPatientRegistration_Constella", ClsUtility.ObjectEnum.DataSet);
                //if (RowsAffected == 0)
                //{
                //    MsgBuilder theBL = new MsgBuilder();
                //    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                //    AppException.Create("#C1", theBL);
                //}
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GetValue;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Gets the field name_and_ label.
        /// </summary>
        /// <param name="FeatureID">The feature identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetFieldName_and_Label(int FeatureID, int PatientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                // ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public List<FormSection> GetFormSection(int featureId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FeatureId", SqlDbType.Int, featureId);
            ClsObject mgr = new ClsObject();


            DataTable dt = (DataTable)mgr.ReturnObject(ClsUtility.theParams, "FormBuilder_GetSection", ClsUtility.ObjectEnum.DataTable);
            var x = (from row in dt.AsEnumerable()
                     select new FormSection()
                     {
                         Id = Convert.ToInt32(row["SectionId"]),
                         Name = Convert.ToString(row["SectionName"]),
                         Description = Convert.ToString(row["SectionInfo"]),
                         DeleteFlag = false,
                         Active = true,
                         Rank = Convert.ToDouble(row["Seq"]),
                         GridView = Convert.ToBoolean(row["IsGridView"]),
                         FeatureId = Convert.ToInt32(row["FeatureId"])

                     });
            mgr = null;
            ClsUtility.Init_Hashtable();
            return x.ToList();

        }
        public List<FormSection> GetCustomRegistrationField(int featureId, int patientId)
        {
            //List<FormSection> sections = this.GetFormSection(featureId);

            DataSet ds = this.GetFieldName_and_Label(featureId, patientId);
            string[] columnNames = { "FeatureId", "SectionId", "SectionName", "SeqSection", "IsGridView", "SectionInfo" };
            DataTable dt = ds.Tables[1];
            DataTable dtSections = ds.Tables[1].DefaultView.ToTable(true, columnNames);
            int? nullInt = null;
            dtSections.DefaultView.Sort = "SeqSection Asc";
            dtSections = dtSections.DefaultView.ToTable();
            var x = (from section in dtSections.AsEnumerable()
                     select new FormSection()
                     {
                         Id = Convert.ToInt32(section["SectionId"]),
                         Name = Convert.ToString(section["SectionName"]),
                         Description = Convert.ToString(section["SectionInfo"]),
                         DeleteFlag = false,
                         Active = true,
                         Rank = Convert.ToDouble(section["SeqSection"]),
                         GridView = Convert.ToBoolean(section["IsGridView"]),
                         FeatureId = Convert.ToInt32(section["FeatureId"]),
                         FieldSet = (from row in dt.AsEnumerable()
                                     where Convert.ToInt32(row["SectionId"]) == Convert.ToInt32(section["SectionId"])
                                     select new FormField()
                                     {
                                         FeatureId = Convert.ToInt32(row["FeatureId"]),
                                         FieldLabel = row["FieldLabel"].ToString(),
                                         Rank = Convert.ToDouble(row["seq"]),
                                         SectionId = Convert.ToInt32(row["SectionId"]),
                                         FieldId = Convert.ToInt32(row["FieldId"]),
                                         PersistField = row["FieldName"].ToString(),
                                         PersistTable = row["PDFTableName"].ToString(),
                                         Field = ((Convert.ToBoolean(row["Predefined"]) == false) ?
                                            (IFormField)(new CustomFormField()
                                            {
                                                Active = true,
                                                Id = Convert.ToInt32(row["FieldId"]),
                                                Name = row["FieldName"].ToString(),
                                                Registration = true,
                                                CategoryId = row["CodeId"] == DBNull.Value ? nullInt : Convert.ToInt32(row["CodeId"]),
                                                CareEnd = false,
                                                FieldType = new Entities.Common.FieldControlType() { Id = Convert.ToInt32(row["ControlId"]), Active = true, DeleteFlag = false },
                                                BindTable = row["BindSource"].ToString()
                                            }) : (IFormField)(new PredefinedFormField()
                                            {

                                                Active = true,
                                                Id = Convert.ToInt32(row["FieldId"]),
                                                Name = row["FieldName"].ToString(),
                                                Registration = true,
                                                CategoryId = row["CodeId"] == DBNull.Value ? nullInt : Convert.ToInt32(row["CodeId"]),
                                                CareEnd = false,
                                                FieldType = new Entities.Common.FieldControlType() { Id = Convert.ToInt32(row["ControlId"]), Active = true, DeleteFlag = false },
                                                BindTable = row["BindSource"].ToString()

                                            })
                                            )

                                     }
                                                 ).ToList()
                     }
                        ).ToList();

            return x;

        }
        #endregion "Dynamic Registration"

        public ServiceArea GetServiceAreaById(int id, int locationId)
        {
            DataSet ds = this.GetModuleNames(locationId);

            var s = (from row in ds.Tables[0].AsEnumerable()
                     where Convert.ToInt32(row["ModuleId"]) == id
                     select new ServiceArea()
                     {
                         Id = Convert.ToInt32(row["ModuleId"]),
                         Name = row["ModuleName"].ToString(),
                         DisplayName = row["DisplayName"].ToString(),
                         EnrolFlag = Convert.ToBoolean(row["CanEnroll"]),
                         PublishFlag = true,
                         DeleteFlag = false,
                         Active = true,
                         Clinical = !Convert.ToBoolean(row["CanEnroll"])
                     }
                     ).FirstOrDefault();
            return s;

        }

        public void BlueCardToGreenCardSyncronise(int ptn_Pk)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, ptn_Pk);         
            ClsObject obClsObject = new ClsObject();
            obClsObject.ReturnObject(ClsUtility.theParams, "SP_Bluecard_ToGreenCard", ClsUtility.ObjectEnum.ExecuteNonQuery);
        }
    }
}