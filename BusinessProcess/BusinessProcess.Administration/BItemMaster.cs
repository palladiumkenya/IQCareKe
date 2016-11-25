using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Administration;
using Entities.Administration;
using Application.Common;
namespace BusinessProcess.Administration
{
    public class BItemMaster : ProcessBase, IItemMaster
    {
        #region ItemList
        /// <summary>
        /// Finds the items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ExcludeItemTypeID">The exclude item type identifier.</param>
        /// <param name="PriceDate">The price date.</param>
        /// <param name="WithPriceOnly">The with price only.</param>
        /// <returns></returns>
        public DataTable FindItems(String filter, int? ItemTypeID = null, int? ExcludeItemTypeID = null, DateTime? PriceDate = null, bool? WithPriceOnly = true)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, filter);
                if (ItemTypeID.HasValue)
                    ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, ItemTypeID.Value);
                if (ExcludeItemTypeID.HasValue)
                    ClsUtility.AddExtendedParameters("@ExcludeItemTypeID", SqlDbType.Int, ExcludeItemTypeID.Value);
                if (PriceDate.HasValue)
                    ClsUtility.AddExtendedParameters("@BillingDate", SqlDbType.DateTime, PriceDate.Value.ToString("yyyy-MM-dd"));
                if (WithPriceOnly.HasValue)
                    ClsUtility.AddExtendedParameters("@HasPrice", SqlDbType.Bit, WithPriceOnly.Value);
                ClsObject BillManager = new ClsObject();
                //return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetAllBillableItems", ClsUtility.ObjectEnum.DataTable);
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_FindItemByName", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Clinicals the find items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataTable ClinicalFindItems(string filter)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, filter);
            ClsObject obj = new ClsObject();
            return (DataTable)obj.ReturnObject(ClsUtility.theParams, "pr_Clinical_FindItemByName", ClsUtility.ObjectEnum.DataTable);
        }
        /// <summary>
        /// Gets the name of the item type identifier by.
        /// </summary>
        /// <param name="ItemTypeName">Name of the item type.</param>
        /// <returns></returns>
        public int GetItemTypeIDByName(string ItemTypeName)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, ItemTypeName);

                ClsObject BillManager = new ClsObject();
                //return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetAllBillableItems", ClsUtility.ObjectEnum.DataTable);
                DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_GetItemTypeIDByName", ClsUtility.ObjectEnum.DataTable);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["ItemTypeID"]);
                }
                else
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Gets the items for billable.
        /// </summary>
        /// <param name="billableItemID">The billable item identifier.</param>
        /// <returns></returns>
        public DataTable GetItemsForBillable(int billableItemID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@BillableItemID", SqlDbType.Int, billableItemID);
                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_GetItemsForBillable", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Saves the billables items.
        /// </summary>
        /// <param name="BillableID">The billable identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemList">The item list.</param>
        public void SaveBillablesItems(int BillableID, int userID, DataTable itemList)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BillableID", SqlDbType.Int, BillableID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());

                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow row in itemList.Rows)
                {

                    sbItems.Append("<row>");
                    sbItems.Append("<itemtypeid>" + row["ItemTypeID"].ToString() + "</itemtypeid>");
                    sbItems.Append("<itemid>" + row["ItemID"].ToString() + "</itemid>");
                    sbItems.Append("<rowstatus>" + row["RowStatus"].ToString() + "</rowstatus>");
                    sbItems.Append("</row>");
                }
                sbItems.Append("</root>");
                ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                ClsObject BillablesManager = new ClsObject();
                BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_SaveItemsForBillable", ClsUtility.ObjectEnum.ExecuteNonQuery);

            }
        }


        /// <summary>
        /// Gets the get item types.
        /// </summary>
        /// <value>
        /// The get item types.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetItemTypes
        {
            get
            {
                lock (this)
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemSubs = new ClsObject();
                    return (DataTable)itemSubs.ReturnObject(ClsUtility.theParams, "Select ItemTypeID, ItemName, DeleteFlag From dbo.Mst_ItemType ORder By SRNo, ItemName;", ClsUtility.ObjectEnum.DataTable);
                }
            }
        }
        /// <summary>
        /// Gets the item types.
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Gets the type of the item sub.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Data.DataTable GetTypeSubType(int itemTypeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject itemSubs = new ClsObject();
                ClsUtility.AddExtendedParameters("@ItemTypeId", SqlDbType.Int, itemTypeID);
                return (DataTable)itemSubs.ReturnObject(ClsUtility.theParams, "pr_Admin_GetSubItemTypes", ClsUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Gets the sub types for item.
        /// </summary>
        /// <param name="itemID">The item identifier.</param>
        /// <returns></returns>
        public System.Data.DataTable GetSubTypesForItem(int itemID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject itemSubs = new ClsObject();
                ClsUtility.AddExtendedParameters("@Item_PK", SqlDbType.Int, itemID);
                return (DataTable)itemSubs.ReturnObject(ClsUtility.theParams, "pr_Admin_GetSubTypesForItem", ClsUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Gets the type of the items by.
        /// </summary>
        /// <param name="itemTypeID">The item type identifier.</param>
        /// <param name="isSubType">if set to <c>true</c> [is sub type].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetItemListByType(int itemTypeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject itemManager = new ClsObject();
                ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, itemTypeID);
                return (DataTable)itemManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetItemsByType", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="Dictionary`2">The dictionary`2.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int AddEditItem(string ItemName, int ItemTypeID, int UserID, Dictionary<int, string> ItemSubTypes = null, bool Active = true, int ItemID = -1)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject itemManager = new ClsObject();
                // ClsUtility.AddParameters("@ItemSubTypeID", SqlDbType.Int, ItemSubTypeID.ToString());
                ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, ItemTypeID);
                ClsUtility.AddExtendedParameters("@ItemName", SqlDbType.VarChar, ItemName);
                ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, !Active);
                if (ItemID > 0)
                {
                    ClsUtility.AddParameters("@Item_PK", SqlDbType.Int, ItemID.ToString());
                }
                DataRow returnRow = (DataRow)itemManager.ReturnObject(ClsUtility.theParams, "pr_Admin_InsertUpdateItem", ClsUtility.ObjectEnum.DataRow);
                int itemID = int.Parse(returnRow[0].ToString());
                if (ItemSubTypes == null)
                {
                    return itemID;
                }
                else
                {

                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (KeyValuePair<int, string> item in ItemSubTypes)
                    {

                        sbItems.Append("<parameter>");
                        sbItems.Append("<subtypeid>" + item.Key.ToString() + "</subtypeid>");
                        sbItems.Append("<subtypename>" + item.Value + "</subtypename>");
                        sbItems.Append("</parameter>");
                    }
                    sbItems.Append("</root>");

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@Item_PK", SqlDbType.Int, itemID);
                    ClsUtility.AddExtendedParameters("@SubTypes", SqlDbType.Xml, sbItems.ToString());
                    itemManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_InsertSubTypeForItem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    return itemID;
                }

            }
        }

        /// <summary>
        /// Adds the type of the item sub.
        /// </summary>
        /// <param name="ItemSubTypeID">The item sub type identifier.</param>
        /// <param name="SubTypeName">Name of the sub type.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public int AddEditItemSubType(string SubTypeName, int ItemTypeID, int UserID, int ItemSubTypeID = -1, bool Active = true)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject itemManager = new ClsObject();
                if (ItemSubTypeID > -1)
                {
                    ClsUtility.AddParameters("@ItemSubTypeID", SqlDbType.Int, ItemSubTypeID.ToString());
                }
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, ItemTypeID.ToString());
                ClsUtility.AddParameters("@SubTypeName", SqlDbType.VarChar, SubTypeName);
                ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, !Active);
                DataRow returnRow = (DataRow)itemManager.ReturnObject(ClsUtility.theParams, "pr_Admin_InsertUpdateSubItemType", ClsUtility.ObjectEnum.DataRow);
                return int.Parse(returnRow[0].ToString());
            }
        }
        #endregion


        
    }


    public class BWardMaster : ProcessBase, IWardsMaster
    {

        #region Wards Admission
        /// <summary>
        /// Discharges the admission.
        /// </summary>
        /// <param name="AdmissionID">The admission identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="DischargeDate">The discharge date.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DischargeAdmission(int AdmissionID, int DischargedBy, int UserID, DateTime DischargeDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemManager = new ClsObject();
                    ClsUtility.AddExtendedParameters("@AdmissionID", SqlDbType.Int, AdmissionID);
                    ClsUtility.AddExtendedParameters("@DischargedBy", SqlDbType.Int, DischargedBy);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@DischargeDate", SqlDbType.DateTime, DischargeDate);
                    itemManager.ReturnObject(ClsUtility.theParams, "pr_Wards_DischargePatient", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the ward admission.
        /// </summary>
        /// <param name="WardID">The ward identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<WardAdmission> GetWardAdmission(int LocationID, int? WardID=null, int? AdmissionID = null, int? PatientID = null, bool ExcludeDischarged = true)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemManager = new ClsObject();
                    if (WardID.HasValue)
                        ClsUtility.AddExtendedParameters("@WardID", SqlDbType.Int, WardID.Value);
                    if (AdmissionID.HasValue)
                        ClsUtility.AddExtendedParameters("@AdmissionID", SqlDbType.Int, AdmissionID.Value);
                    if (PatientID.HasValue)
                        ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID.Value);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    ClsUtility.AddExtendedParameters("@ExcludeDischarged", SqlDbType.Bit, ExcludeDischarged);
                    ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    DataTable returnTable = (DataTable)itemManager.ReturnObject(ClsUtility.theParams, "pr_Wards_GetAdmission", ClsUtility.ObjectEnum.DataTable);
                    DateTime? emptyDate = null;
                    int? emptyInt = null;

                    var _admissions = (from _row in returnTable.AsEnumerable()
                                       select new WardAdmission()
                                       {
                                           AdmissionID = Convert.ToInt32(_row["AdmissionID"]),
                                           WardID = Convert.ToInt32(_row["WardID"]),
                                           WardName = _row["WardName"].ToString(),
                                           PatientID = Convert.ToInt32(_row["PatientID"]),
                                           AdmissionDate = Convert.ToDateTime(_row["AdmissionDate"]),
                                           AdmittedBy = Convert.ToInt32(_row["AdmittedBy"]),
                                           AdmissionNumber = _row["AdmissionNumber"]!= DBNull.Value ? _row["AdmissionNumber"].ToString() : "",
                                           BedNumber = _row["BedNumber"].ToString(),
                                           DischargeDate = _row["ActualDoD"] != DBNull.Value ? Convert.ToDateTime(_row["ActualDoD"]) : emptyDate,
                                           ExpectedDischarge = _row["ExpectedDoD"] != DBNull.Value ? Convert.ToDateTime(_row["ExpectedDoD"]) : emptyDate,
                                           PatientName = _row["PatientName"].ToString(),
                                           PatientNumber = _row["PatientNumber"].ToString(),
                                           Discharged = Convert.ToBoolean(_row["Discharged"]),
                                           DischargedBy = _row["DischargedBy"] != DBNull.Value ? Convert.ToInt32(_row["DischargedBy"]) : emptyInt,
                                           ReferredFrom = _row["ReferredFrom"].ToString()
                                       }
                                          ).ToList<WardAdmission>();
                    return _admissions;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the wards.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="WardId">The ward identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<PatientWard> GetWards(int LocationID, int? WardId = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemManager = new ClsObject();
                    // ClsUtility.AddParameters("@ItemSubTypeID", SqlDbType.Int, ItemSubTypeID.ToString());
                    if (WardId.HasValue)
                        ClsUtility.AddExtendedParameters("@WardID", SqlDbType.Int, WardId.Value);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    DataTable returnTable = (DataTable)itemManager.ReturnObject(ClsUtility.theParams, "pr_Wards_Get", ClsUtility.ObjectEnum.DataTable);
                    var _patientWards = (from _row in returnTable.AsEnumerable()
                                         select new PatientWard()
                                         {
                                             WardName = _row["WardName"].ToString(),
                                             WardID = Convert.ToInt32(_row["WardID"]),
                                             Capacity = Convert.ToInt32(_row["Capacity"]),
                                             PatientCategory = _row["PatientCategory"].ToString(),
                                             Active = Convert.ToBoolean(_row["Active"]),
                                             LocationID = Convert.ToInt32(_row["LocationID"]),
                                             Occupancy = Convert.ToInt32(_row["Occupancy"])
                                         }
                          ).ToList<PatientWard>(); ;
                    return _patientWards;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the admission.
        /// </summary>
        /// <param name="WardID">The ward identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="AdmissionDate">The admission date.</param>
        /// <param name="ExpectedDischargeDate">The expected discharge date.</param>
        /// <param name="AdmissionID">The admission identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        ///  //(int WardID, int UserID, int PatientID, DateTime AdmissionDate, DateTime? ExpectedDischargeDate, int? AdmissionID = null)
        public string SaveAdmission(WardAdmission admission, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemManager = new ClsObject();
                    ClsUtility.AddExtendedParameters("@WardID", SqlDbType.Int, admission.WardID);
                    ClsUtility.AddExtendedParameters("@AdmittedBy", SqlDbType.Int, admission.AdmittedBy);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, admission.PatientID);
                    ClsUtility.AddExtendedParameters("@AdmissionDate", SqlDbType.DateTime, admission.AdmissionDate);
                  //  ClsUtility.AddExtendedParameters("@AdmissionNumber", SqlDbType.VarChar, admission.AdmissionNumber);
                    ClsUtility.AddExtendedParameters("@ReferredFrom", SqlDbType.VarChar, admission.ReferredFrom);
                    ClsUtility.AddExtendedParameters("@BedNumber", SqlDbType.VarChar, admission.BedNumber);
                    ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, admission.Active);
                    if (admission.AdmissionID.HasValue)
                        ClsUtility.AddExtendedParameters("@AdmissionID", SqlDbType.Int, admission.AdmissionID.Value);
                    if (admission.ExpectedDischarge.HasValue)
                        ClsUtility.AddExtendedParameters("@ExpectedDischargeDate", SqlDbType.DateTime, admission.ExpectedDischarge.Value);
                    DataTable dt = (DataTable)itemManager.ReturnObject(ClsUtility.theParams, "pr_Wards_SaveAdmission", ClsUtility.ObjectEnum.DataTable);
                    return dt.Rows[0][0].ToString();
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Saves the ward.
        /// </summary>
        /// <param name="Ward">The ward.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveWard(PatientWard Ward, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject itemManager = new ClsObject();
                    if (Ward.WardID.HasValue)
                        ClsUtility.AddExtendedParameters("@WardID", SqlDbType.Int, Ward.WardID.Value);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddParameters("@WardName", SqlDbType.VarChar, Ward.WardName);
                    ClsUtility.AddExtendedParameters("@Capacity", SqlDbType.Int, Ward.Capacity);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, Ward.LocationID);
                    ClsUtility.AddParameters("@PatientCategory", SqlDbType.VarChar, Ward.PatientCategory);
                    ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, Ward.Active);
                    itemManager.ReturnObject(ClsUtility.theParams, "pr_Wards_Save", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion
    }
}
