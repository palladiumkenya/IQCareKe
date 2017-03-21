using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Interface.SCM
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMasterList
    {
        /// <summary>
        /// Gets the program list.
        /// </summary>
        /// <returns></returns>
        DataSet GetProgramList();
        /// <summary>
        /// Saves the program list.
        /// </summary>
        /// <param name="dtProgramList">The dt program list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveProgramList(DataTable dtProgramList, int userId);
        /// <summary>
        /// Gets the supplier list.
        /// </summary>
        /// <returns></returns>
        DataSet GetSupplierList();
        /// <summary>
        /// Saves the supplier list.
        /// </summary>
        /// <param name="dtSupplierList">The dt supplier list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveSupplierList(DataTable dtSupplierList, int userId);
        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <returns></returns>
        DataTable GetItemType();
        /// <summary>
        /// Gets the type of the drug.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <returns></returns>
        DataSet GetDrugType(int itemTypeId);
        /// <summary>
        /// Gets the type of the sub item.
        /// </summary>
        /// <returns></returns>
        DataTable GetSubItemType();
        // DataTable GetItemList(int itemTypeId, int Subtypeid);
        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        DataSet GetItemList(int itemTypeId, int Subtypeid, int programId);
        /// <summary>
        /// Gets the common item list.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        DataTable GetCommonItemList(String categoryId, String tableName, int locationId);
        /// <summary>
        /// Saves the sub item list.
        /// </summary>
        /// <param name="tdrugType">Type of the tdrug.</param>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="userId">The userId.</param>
        /// <returns></returns>
        int SaveSubItemList(ArrayList tdrugType, int ItemID, int userId);
        /// <summary>
        /// Saves the item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="itematypeID">The itematype identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="ProgramID">The program identifier.</param>
        /// <returns></returns>
        int SaveItemList(DataTable dtItemList, int itematypeID, int userId, int ProgramID);
        /// <summary>
        /// Saves the supplier item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="itematypeID">The itematype identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="supplierID">The supplier identifier.</param>
        /// <returns></returns>
        int SaveSupplierItemList(DataTable dtItemList, int itematypeID, int userId, int supplierID);
        /// <summary>
        /// Saves the update item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="CategoryID">The category identifier.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveUpdateItemList(DataTable dtItemList, string CategoryID, string TableName, int userId);
        /// <summary>
        /// Gets the donor list.
        /// </summary>
        /// <returns></returns>
        DataSet GetDonorList();
        /// <summary>
        /// Saves the donor list.
        /// </summary>
        /// <param name="dtDonorList">The dt donor list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveDonorList(DataTable dtDonorList, int userId);
        /// <summary>
        /// Gets the program donor LNK.
        /// </summary>
        /// <returns></returns>
        DataSet GetProgramDonorLnk();
        /// <summary>
        /// Gets the item master listing.
        /// </summary>
        /// <returns></returns>
        DataSet GetItemMasterListing();
        /// <summary>
        /// Saves the update store.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="CategoryID">The category identifier.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveUpdateStore(DataTable dtItemList,  int userId, int locationId);
        /// <summary>
        /// Saves the program donor LNK.
        /// </summary>
        /// <param name="dtProgramDonorLnk">The dt program donor LNK.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveProgramDonorLnk(DataTable dtProgramDonorLnk, int userId);
        //DataTable GetItemListSupplier(int itemTypeId, int Subtypeid);
        /// <summary>
        /// Gets the item list supplier.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="SupplierId">The supplier identifier.</param>
        /// <returns></returns>
        DataSet GetItemListSupplier(int itemTypeId, int Subtypeid, int SupplierId);
        /// <summary>
        /// Gets the store detail.
        /// </summary>
        /// <returns></returns>
        DataSet GetStoreDetail();
        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <param name="theItemId">The item identifier.</param>
        /// <returns></returns>
        DataSet GetItemDetails(int theItemId);
        /// <summary>
        /// Saves the update store linking.
        /// </summary>
        /// <param name="dtStoreList">The dt store list.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveUpdateStoreLinking(DataTable dtStoreList, string TableName, int userId);
        /// <summary>
        /// Saves the update item master.
        /// </summary>
        /// <param name="theHash">The hash.</param>
        /// <returns></returns>
        int SaveUpdateItemMaster(Hashtable theHash);
        /// <summary>
        /// Gets the store user link.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        DataSet GetStoreUserLink(int StoreId);
        //int SaveUpdateStoreUserLinking(DataTable dtStoreList);
        /// <summary>
        /// Saves the update store user linking.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="Users">The users.</param>
        /// <returns></returns>
        int SaveUpdateStoreUserLinking(int StoreId, List<int> Users);
        /// <summary>
        /// Gets the store by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        DataTable GetStoreByUser(int userId);
        /// <summary>
        /// Gets the item by store identifier.
        /// </summary>
        /// <param name="storeId">The store identifier.</param>
        /// <returns></returns>
        DataTable GetItemByStoreId(int storeId);
        /// <summary>
        /// Gets the item list store.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        DataSet GetItemListStore(int StoreId);
        /// <summary>
        /// Saves the store item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns></returns>
        int SaveStoreItemList(DataTable dtItemList, int userId, int StoreID);
        /// <summary>
        /// Saves the name of the batch.
        /// </summary>
        /// <param name="BatchName">Name of the batch.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="itemID">The item identifier.</param>
        /// <param name="expiryDatetime">The expiry datetime.</param>
        /// <returns></returns>
        DataSet SaveBatchName(string BatchName, int userId, string itemID, string expiryDatetime);
        /// <summary>
        /// Gets the item list store_ filtered.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        DataSet GetItemListStore_Filtered(int itemTypeId, int Subtypeid, int StoreId);
        /// <summary>
        /// Saves the store item list_ filtered.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="itemtypeID">The itemtype identifier.</param>
        /// <returns></returns>
        int SaveStoreItemList_Filtered(DataTable dtItemList, int userId, int storeId, int itemtypeId);

        DataTable GetItemsBySupplier(int supplierId, int? itemTypeId = default(int?));

    }
}
