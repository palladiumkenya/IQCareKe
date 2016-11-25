using System;
using System.Collections.Generic;
using System.Data;
using Entities.Administration;

namespace Interface.Administration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IItemMaster
    {

        /// <summary>
        /// Finds the items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ExcludeItemTypeID">The exclude item type identifier.</param>
        /// <param name="PriceDate">The price date.</param>
        /// <param name="WithPriceOnly">The with price only.</param>
        /// <returns></returns>
        DataTable FindItems(String filter, int? ItemTypeID = null, int? ExcludeItemTypeID = null, DateTime? PriceDate = null, bool? WithPriceOnly = true);
        /// <summary>
        /// Clinicals the find items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        DataTable ClinicalFindItems(string filter);
        /// <summary>
        /// Gets the get item types.
        /// </summary>
        /// <value>
        /// The get item types.
        /// </value>
        DataTable GetItemTypes { get; }
        /// <summary>
        /// Gets the type of the item sub.
        /// </summary>
        /// <returns></returns>
        DataTable GetTypeSubType(int itemTypeID);
        /// <summary>
        /// Gets the sub types for item.
        /// </summary>
        /// <param name="itemID">The item identifier.</param>
        /// <returns></returns>
        DataTable GetSubTypesForItem(int itemID);
        /// <summary>
        /// Gets the type of the items by.
        /// </summary>
        /// <param name="itemTypeID">The item type identifier.</param>
        /// <param name="isSubType">if set to <c>true</c> [is sub type].</param>
        /// <returns></returns>
        DataTable GetItemListByType(int itemTypeID);
        /// <summary>
        /// Adds the type of the item sub.
        /// </summary>
        /// <param name="ItemSubTypeID">The item sub type identifier.</param>
        /// <param name="SubTypeName">Name of the sub type.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        int AddEditItemSubType(string SubTypeName, int ItemTypeID, int UserID, int ItemSubTypeID = -1, bool Active = true);
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="ItemSubTypes">The item sub types.</param>
        /// <returns></returns>
        int AddEditItem(string ItemName, int ItemTypeID, int UserID, Dictionary<int, string> ItemSubTypes = null, bool Active = true, int ItemID = -1);
        /// <summary>
        /// Gets the name of the item type identifier by.
        /// </summary>
        /// <param name="ItemName">Name of the item.</param>
        /// <returns></returns>
        int GetItemTypeIDByName(string ItemName);

        #region Billables
        /// <summary>
        /// Gets the items for billable.
        /// </summary>
        /// <param name="billableItemID">The billable item identifier.</param>
        /// <returns></returns>
        DataTable GetItemsForBillable(int billableItemID);
        /// <summary>
        /// Saves Billable items lsit
        /// </summary>
        /// <param name="BillableID">Bill item Identifier</param>
        /// <param name="userID">user identifier</param>
        /// <param name="itemList">item list </param>
        void SaveBillablesItems(int BillableID, int userID, DataTable itemList);

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IWardsMaster
    {
        #region Wards
        
        /// <summary>
        /// Saves the ward.
        /// </summary>
        /// <param name="ward">The ward.</param>
        /// <param name="UserID">The user identifier.</param>
        void SaveWard(PatientWard Ward, int UserID);
        /// <summary>
        /// Gets the wards.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="WardId">The ward identifier.</param>
        /// <returns></returns>
        List<PatientWard> GetWards(int LocationID, int? WardId=null);
        /// <summary>
        /// Gets the ward admission.
        /// </summary>
        /// <param name="WardID">The ward identifier.</param>
        /// <returns></returns>
        List<WardAdmission> GetWardAdmission(int LocationID, int? WardID=null, int? AdmissionID = null, int? PatientID = null, bool ExcludeDischarged = true);
       /// <summary>
       /// Makes an admission.
       /// </summary>
       /// <param name="WardID">The ward identifier.</param>
       /// <param name="UserID">The user identifier.</param>
       /// <param name="PatientID">The patient identifier.</param>
       /// <param name="AdmissionDate">The admission date.</param>
       /// <param name="ExpectedDischargeDate">The expected discharge date.</param>
       /// <param name="AdmissionID">The admission identifier.</param>
       string SaveAdmission(WardAdmission admission, int UserID);
        /// <summary>
        /// Discharges the admission.
        /// </summary>
        /// <param name="AdmissionID">The admission identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="DischargeDate">The discharge date.</param>
        void DischargeAdmission(int AdmissionID, int DischargedBy, int UserID, DateTime DischargeDate);

        #endregion
       
    }
    
}
