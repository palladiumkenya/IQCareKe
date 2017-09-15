using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Common;
using Entities.FormBuilder;
using Entities.PatientCore;
using Interface.Clinical;
using DataAccess.Context;

namespace BusinessProcess.Clinical
{
    /// <summary>
    ///
    /// </summary>
    public class BPatientHome : ProcessBase,IPatientHome
    {
        #region "Constuctor"
        /// <summary>
        /// Initializes a new instance of the <see cref="BPatientHome"/> class.
        /// </summary>
        public BPatientHome()
        {
        }
        #endregion

        /// <summary>
        /// Gets the patient details.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="systemId">The system identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientDetails(int patientId, int systemId, int moduleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@SystemId", SqlDbType.Int, systemId);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
             //   ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient history.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientHistory(int patientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                // ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientHistory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient visit detail.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientVisitDetail(int patientId)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);

                //ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientVisitDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Res the activate patient.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="ModId">The mod identifier.</param>
        /// <returns></returns>
        public DataSet ReActivatePatient(int patientId, int modId)
        {
            lock (this)
            {
                ClsObject ReActivatePtnMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@Mod", SqlDbType.Int, modId);
                return (DataSet)ReActivatePtnMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_ReActivatePatient_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Gets the pharmacy identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="VisitId">The visit identifier.</param>
        /// <returns></returns>
        public DataTable GetPharmacyID(int patientId, int locationId, int visitId  )
        {
            lock (this)
            {
                ClsObject PharmacyMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                ClsUtility.AddExtendedParameters("@VisitId", SqlDbType.Int, visitId);

                return (DataTable)PharmacyMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPharmacyId_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Gets the name of the technical areaand form.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetTechnicalAreaandFormName(int moduleId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaandFormName_Future", ClsUtility.ObjectEnum.DataSet);
            }
        
        }

        /// <summary>
        /// Gets the technical area indicators.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetTechnicalAreaIndicators(int moduleId, int patientId)
        { 
            try
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaIndicators_Future", ClsUtility.ObjectEnum.DataSet);

            }

            catch //(Exception ex)
            {
                //throw ex;
                return null;
            }

            
        }

        /// <summary>
        /// Gets the technical area identifier future.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="patientId">The PTN_PK.</param>
        /// <returns></returns>
        public DataSet GetTechnicalAreaIdentifierFuture(int moduleId, int patientId)
        {
            try
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                ClsUtility.AddExtendedParameters("@Ptn_pk", SqlDbType.Int, patientId);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaIdentifier_Future", ClsUtility.ObjectEnum.DataSet);

            }

            catch //(Exception ex)
            {
                //throw ex;
                return null;
            }


        }

        /// <summary>
        /// Gets the patient debit note summary.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientDebitNoteSummary(int patientId)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);

                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteSummary_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patient debit note open items.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public DataTable GetPatientDebitNoteOpenItems(int patientId, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@Start", SqlDbType.DateTime, start);
                ClsUtility.AddExtendedParameters("@End", SqlDbType.DateTime, end);

                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteOpenItems_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the patient debit note details.
        /// </summary>
        /// <param name="billId">The bill identifier.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientDebitNoteDetails(int billId,int patientId)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@billid", SqlDbType.Int, billId);
                ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, patientId);
              //  ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }


        /// <summary>
        /// Creates the debit note.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public int CreateDebitNote(int patientId,int locationId, int userId, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@locationID", SqlDbType.Int, locationId);
                ClsUtility.AddExtendedParameters("@userID", SqlDbType.Int, userId);
                ClsUtility.AddExtendedParameters("@Start", SqlDbType.DateTime, start.ToString());
                ClsUtility.AddExtendedParameters("@End", SqlDbType.DateTime, end.ToString());

                DataRow row = (DataRow)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_CreateDebitNote_Futures", ClsUtility.ObjectEnum.DataRow);
                int billid = Convert.ToInt32(row["BillId"]);
                return billid;
            }
        }

        /**************************************/
        //John Macharia
        //5th Sep 2012
        /*************************************/
        /// <summary>
        /// Gets the patient summary information.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetPatientSummaryInformation(int patientId, int moduleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
               // ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientSummaryInfo_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //John End
        /// <summary>
        /// Gets the patient wait list.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientWaitList(int patientId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);


                return (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_WaitingList_GetPatientsWaitingList", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Saves the patient wait list.
        /// </summary>
        /// <param name="WaitingList">The waiting list.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        public void SavePatientWaitList(DataTable WaitingList, int ModuleID, int UserID,int PatientID)
        {
            
            System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
            foreach (DataRow row in WaitingList.Rows)
            {

                sbItems.Append("<row>");
                sbItems.Append(string.Format("<WaitingListID>{0}</WaitingListID>", row["WaitingListID"]));
                sbItems.Append(string.Format("<ListID>{0}</ListID>", row["ListID"]));
                sbItems.Append(string.Format("<Priority>{0}</Priority>", row["Priority"]));
                sbItems.Append(string.Format("<RowStatus>{0}</RowStatus>", row["RowStatus"]));
               

                sbItems.Append("</row>");
            }
            sbItems.Append("</root>");
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID);
                ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, ModuleID);
                ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                ClsObject BillManager = new ClsObject();
               BillManager.ReturnObject(ClsUtility.theParams, "pr_WaitingList_SavePatientsWaitingList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                

            }
        }



        /// <summary>
        /// Gets the patient revisits.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataTable GetPatientRevisits(int patientId, int locationId,bool GetLastOnly =false)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, patientId);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                    ClsUtility.AddExtendedParameters("@GetLastVisit", SqlDbType.Bit, GetLastOnly);
                    ClsObject clsObject = new ClsObject();
                    DataTable dt = (DataTable) clsObject.ReturnObject(ClsUtility.theParams, "pr_Record_GetPatientRevisit", ClsUtility.ObjectEnum.DataTable);
                    return dt;

                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the patient revisit.
        /// </summary>
        /// <param name="visitDate">The visit date.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SavePatientRevisit(DateTime visitDate, int hoursBetweenRevisit, int moduleId, int userId, int patientId, int locationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, patientId);
                    ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, moduleId);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                    ClsUtility.AddExtendedParameters("@VisitDate", SqlDbType.DateTime, visitDate);
                    ClsUtility.AddExtendedParameters("@RevistHrsAllowance", SqlDbType.Int, hoursBetweenRevisit);
                    ClsObject clsObject = new ClsObject();
                    clsObject.ReturnObject(ClsUtility.theParams, "pr_Record_CreatePatientRevisit", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                catch
                {
                    throw;
                }
            }
        }





        public DataTable GetModuleForms(int moduleId,  int locationId)
        {
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            ClsObject clsObject = new ClsObject();
            DataTable dt = (DataTable)clsObject.ReturnObject(ClsUtility.theParams, "ServiceArea_GetForms", ClsUtility.ObjectEnum.DataTable);
            return dt;
        }

        public DataTable GetModuleReport(int moduleId, int locationId)
        {
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            ClsObject clsObject = new ClsObject();
            DataTable dt = (DataTable)clsObject.ReturnObject(ClsUtility.theParams, "ServiceArea_GetPatientReports", ClsUtility.ObjectEnum.DataTable);
            return dt;
        }


        public List<FormRule> GetModuleFormsBusinessRule(int? moduleId = null, int? featureId = null, int? formId = null)
        {
            ClsUtility.Init_Hashtable();
            if (moduleId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId.Value);
            }
            if (featureId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@FeatureId", SqlDbType.Int, featureId.Value);
            }
            if (formId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@FormId", SqlDbType.Int, formId.Value);
            }
            ClsObject clsObject = new ClsObject();
            DataTable dt = (DataTable)clsObject.ReturnObject(ClsUtility.theParams, "Form_GetBusinessRule", ClsUtility.ObjectEnum.DataTable);
            var result = (from row in dt.AsEnumerable()
                          select new FormRule()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              MinValue = Convert.ToString(row["MinValue"]),
                              MaxValue = Convert.ToString(row["MaxValue"]),
                              RuleSet = Convert.ToInt32(row["RuleSet"]),
                              FormId = Convert.ToInt32(row["FormId"]),                              
                              Form = new FormObject(){
                                  Id=Convert.ToInt32(row["FormId"]),                                 
                                  Name=Convert.ToString(row["FormName"]),
                                  FeatureId = Convert.ToInt32(row["FeatureId"]),
                                  ReferenceName= Convert.ToString(row["FormReferenceId"]),
                                  Custom= Convert.ToBoolean(row["Custom"]),
                                  FormTypeId= Convert.ToInt32(row["FeatureTypeId"]),
                                  FormTypeReferenceName=Convert.ToString(row["FormTypeReferenceId"])
                              },
                              BusinessRule = new BusinessRule()
                              {
                                  RuleId = Convert.ToInt32(row["BusRuleId"]),
                                  RuleName = Convert.ToString(row["BusRuleName"]),
                                  ReferenceId = Convert.ToString(row["BusRuleReferenceId"]),
                                  DeleteFlag = Convert.ToBoolean(row["BusRuleDeleteFlag"])
                              }
                          }
                          ).ToList<FormRule>();
            return result;
          

        }


        /// <summary>
        /// Gets the patient by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Patient GetPatientById(int Id)
        {
            PatientRepository repo = new PatientRepository();
            Patient p = repo.Get(Id);
           return p;
        }
    }
    
}
