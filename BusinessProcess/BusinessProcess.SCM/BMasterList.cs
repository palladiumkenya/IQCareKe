using System;
using System.Collections;
using System.Data;
using System.Text;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using System.Collections.Generic;

namespace BusinessProcess.SCM
{

    /// <summary>
    /// 
    /// </summary>
    public class BMasterList : ProcessBase, IMasterList
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BMasterList"/> class.
        /// </summary>
        public BMasterList()
        {
        }

        #endregion

        /// <summary>
        /// Gets the program list.
        /// </summary>
        /// <returns></returns>
        public DataSet GetProgramList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Programlist = new ClsObject();
                return
                    (DataSet)
                    Programlist.ReturnObject(ClsUtility.theParams, "pr_SCM_GetProgramList_Futures",
                                             ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the name of the batch.
        /// </summary>
        /// <param name="BatchName">Name of the batch.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="itemID">The item identifier.</param>
        /// <param name="expiryDatetime">The expiry datetime.</param>
        /// <returns></returns>
        public DataSet SaveBatchName(string BatchName, int UserId,string itemID,string expiryDatetime)
        {
            lock (this)
            {
                ClsObject BatchNameMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BatchName", SqlDbType.VarChar, BatchName.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@ItemID", SqlDbType.VarChar, itemID.ToString());
                ClsUtility.AddParameters("@ExpiryDatetime", SqlDbType.VarChar, expiryDatetime.ToString());

                return
                    (DataSet)
                    BatchNameMgr.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveBatchFromOpnStock_Futures",
                                              ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the program list.
        /// </summary>
        /// <param name="dtProgramList">The dt program list.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveProgramList(DataTable dtProgramList, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtProgramList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.VarChar, dtProgramList.Rows[i]["ProgramId"].ToString());
                    ClsUtility.AddParameters("@ProgramName", SqlDbType.VarChar,
                                             dtProgramList.Rows[i]["ProgramName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtProgramList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@FiscalYearMonth", SqlDbType.Int,
                                             dtProgramList.Rows[i]["FiscalYearMonth"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ProgramList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveProgramMaster_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the update item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="CategoryID">The category identifier.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveUpdateItemList(DataTable dtItemList, string CategoryID, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;

                foreach (DataRow theDR in dtItemList.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["id"].ToString());
                    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDR["Status"].ToString());
                    ClsUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    ClsUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                    ClsUtility.AddParameters("@TableName", SqlDbType.Int, TableName.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the supplier list.
        /// </summary>
        /// <returns></returns>
        public DataSet GetSupplierList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject SupplierList = new ClsObject();
                return
                    (DataSet)
                    SupplierList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetSupplierList_Futures",
                                              ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the supplier list.
        /// </summary>
        /// <param name="dtSupplierList">The dt supplier list.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveSupplierList(DataTable dtSupplierList, int UserID)
        {
            lock (this)
            {
                ClsObject SupplierList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtSupplierList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtSupplierList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@SupplierId", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierId"].ToString());
                    ClsUtility.AddParameters("@SupplierName", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtSupplierList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@Address", SqlDbType.VarChar, dtSupplierList.Rows[i]["Address"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        SupplierList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveSupplierMaster_Futures",
                                                  ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetItemType_Futures]",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the type of the sub item.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubItemType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetSubItemType_Futures]",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the type of the drug.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <returns></returns>
        public DataSet GetDrugType(int itemTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsObject DrugList = new ClsObject();
                return
                    (DataSet)
                    DrugList.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetDrugType_Futures]",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }


        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        public DataSet GetItemList(int itemTypeId, int Subtypeid, int programId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@programId", SqlDbType.Int, programId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemList_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the item list supplier.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="SupplierId">The supplier identifier.</param>
        /// <returns></returns>
        public DataSet GetItemListSupplier(int itemTypeId, int Subtypeid, int SupplierId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, SupplierId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListSupplier_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the item list store_ filtered.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="Subtypeid">The subtypeid.</param>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        public DataSet GetItemListStore_Filtered(int itemTypeId, int Subtypeid, int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListStoreFiltered_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the item list store.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        public DataSet GetItemListStore(int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListStore_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the common item list.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public DataTable GetCommonItemList(String categoryId, String tableName, int locationId)
        {
            lock (this)
            {
                ClsObject ObjCommonItemList = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CategoryId", SqlDbType.VarChar, categoryId.ToString());
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, tableName.ToString());
                ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                return
                    (DataTable)
                    ObjCommonItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetCommonItemList_Futures",
                                                   ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Saves the sub item list.
        /// </summary>
        /// <param name="dtSubitemList">The dt subitem list.</param>
        /// <param name="itemID">The item identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveSubItemList(ArrayList dtSubitemList, int itemID, int UserID)
        {
            lock (this)
            {
                ClsObject subItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                theRowAffected =
                    (int)
                    subItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteItemdrugType_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i <= dtSubitemList.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, dtSubitemList[i].ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        subItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemdrugType_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="itematypeID">The itematype identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ProgramID">The program identifier.</param>
        /// <returns></returns>
        public int SaveItemList(DataTable dtItemList, int itematypeID, int UserID, int ProgramID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                //ClsUtility.Init_Hashtable();
                //ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                //ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                //theRowAffected =
                //    (int)
                //    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteItemList_Futures",
                //                             ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dtItemList.Rows.Count; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    //  ClsUtility.AddParameters("@DrugGeneric", SqlDbType.Int, dtItemList.Rows[i]["DrugGeneric"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Checked", SqlDbType.Int, dtItemList.Rows[i]["Checked"].ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemList_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return Rec;
            }
        }

        /// <summary>
        /// Saves the supplier item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="itematypeID">The itematype identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="supplierID">The supplier identifier.</param>
        /// <returns></returns>
        public int SaveSupplierItemList(DataTable dtItemList, int itematypeID, int UserID, int supplierID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletesupplierItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveSupplierItemList_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the store item list_ filtered.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="itemtypeID">The itemtype identifier.</param>
        /// <returns></returns>
        public int SaveStoreItemList_Filtered(DataTable dtItemList, int UserID, int StoreID,int itemtypeID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the store item list.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns></returns>
        public int SaveStoreItemList(DataTable dtItemList, int UserID, int StoreID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the donor list.
        /// </summary>
        /// <returns></returns>
        public DataSet GetDonorList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DonorList = new ClsObject();
                return
                    (DataSet)
                    DonorList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDonorList_Futures",
                                           ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the donor list.
        /// </summary>
        /// <param name="dtDonorList">The dt donor list.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveDonorList(DataTable dtDonorList, int UserID)
        {
            lock (this)
            {
                ClsObject DonorList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtDonorList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtDonorList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@DonorId", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorId"].ToString());
                    ClsUtility.AddParameters("@DonorName", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtDonorList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@Donorshortname", SqlDbType.VarChar,
                                             dtDonorList.Rows[i]["Donorshortname"].ToString());
                    ClsUtility.AddParameters("@Srno", SqlDbType.Int, dtDonorList.Rows[i]["Srno"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        DonorList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDonorMaster_Futures",
                                               ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the program donor LNK.
        /// </summary>
        /// <returns></returns>
        public DataSet GetProgramDonorLnk()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ProgramDonorLnk = new ClsObject();
                return
                    (DataSet)
                    ProgramDonorLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDonorProgramLinking_Futures",
                                                 ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the item master listing.
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemMasterListing()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemMasterManager = new ClsObject();
                return
                    (DataSet)
                    ItemMasterManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetItemListing_Futures",
                                                   ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the update store.
        /// </summary>
        /// <param name="dtItemList">The dt item list.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int SaveUpdateStore(DataTable dtItemList,  int userId, int locationId)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                foreach (DataRow theDR in dtItemList.Rows)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, (theDR["Id"]!=DBNull.Value? Convert.ToInt32(theDR["Id"]):0));
                    ClsUtility.AddParameters("@StoreId", SqlDbType.VarChar, theDR["StoreId"].ToString());
                    ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    ClsUtility.AddParameters("@StoreCategory", SqlDbType.VarChar, theDR["StoreCategory"].ToString());
                    ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, Convert.ToBoolean(theDR["DeleteFlag"]));
                    ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, (theDR["Status"].ToString() == "Active"));
                    ClsUtility.AddParameters("@CentralStore", SqlDbType.Int, theDR["CentralStore"].ToString());
                    ClsUtility.AddParameters("@DispensingStore", SqlDbType.VarChar, theDR["DispensingStore"].ToString());
                    ClsUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(ClsUtility.theParams, "SCM_ManageItemStore",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the program donor LNK.
        /// </summary>
        /// <param name="dtProgramDonorLnk">The dt program donor LNK.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveProgramDonorLnk(DataTable dtProgramDonorLnk, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramDonorLnk = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramDonorLnk.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DonorId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["DonorId"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["ProgramId"].ToString());
                    ClsUtility.AddParameters("@FundingStartDate", SqlDbType.DateTime, dtProgramDonorLnk.Rows[i]["FundingStartDate"].ToString());
                    ClsUtility.AddParameters("@FundingEndDate", SqlDbType.DateTime, dtProgramDonorLnk.Rows[i]["FundingEndDate"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    if (Rec == 1)
                        ClsUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    theRowAffected = (int)ProgramDonorLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveProgramDonorlnk_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        /// <summary>
        /// Gets the store detail.
        /// </summary>
        /// <returns></returns>
        public DataSet GetStoreDetail()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject StoreMasterManager = new ClsObject();
                return
                    (DataSet)
                    StoreMasterManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStoreDetails_Futures",
                                                    ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <param name="theItemId">The item identifier.</param>
        /// <returns></returns>
        public DataSet GetItemDetails(int theItemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theItemId.ToString());
                ClsObject ItemManager = new ClsObject();
                return
                    (DataSet)
                    ItemManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemDetails_Futures",
                                             ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the update store linking.
        /// </summary>
        /// <param name="dtStoreList">The dt store list.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveUpdateStoreLinking(DataTable dtStoreList, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                StringBuilder theSB = new StringBuilder();
                theSB.Append("Delete from " + TableName + " ");
                foreach (DataRow theDR in dtStoreList.Rows)
                {
                    theSB.Append("Insert into " + TableName +
                                 "(SourceStore, DestinationStore, UserId, CreateDate, UpdateDate) ");
                    theSB.Append("values (" + theDR["SourceStore"].ToString() + "," + theDR["DestinationStore"].ToString() +
                                 "," + UserID + ", getdate(), getdate())");
                }
                ClsUtility.AddParameters("@Str", SqlDbType.VarChar, theSB.ToString());
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                theRowAffected =
                    (int)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                          ClsUtility.ObjectEnum.ExecuteNonQuery);

                return theRowAffected;
            }
        }

        /// <summary>
        /// Saves the update item master.
        /// </summary>
        /// <param name="theHash">The hash.</param>
        /// <returns></returns>
        public int SaveUpdateItemMaster(Hashtable theHash)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theHash["Drug_Pk"].ToString());
                ClsUtility.AddParameters("@ItemCode", SqlDbType.VarChar, theHash["ItemCode"].ToString());
                ClsUtility.AddParameters("@FDACode", SqlDbType.VarChar, theHash["FDACode"].ToString());
                ClsUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theHash["DispensingUnit"].ToString());
                ClsUtility.AddParameters("@PurchaseUnit", SqlDbType.Int, theHash["PurchaseUnit"].ToString());
                ClsUtility.AddParameters("@PurchaseUnitQty", SqlDbType.Int, theHash["PurchaseUnitQty"].ToString());
                ClsUtility.AddParameters("@PurchaseUnitPrice", SqlDbType.Decimal, theHash["PurchaseUnitPrice"].ToString());
                ClsUtility.AddParameters("@Manufacturer", SqlDbType.Int, theHash["Manufacturer"].ToString());
                ClsUtility.AddParameters("@DispensingUnitPrice", SqlDbType.Decimal,
                                         theHash["DispensingUnitPrice"].ToString());
                ClsUtility.AddParameters("@DispensingMargin", SqlDbType.Decimal, theHash["DispensingMargin"].ToString());
                ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theHash["SellingPrice"].ToString());
                ClsUtility.AddParameters("@EffectiveDate", SqlDbType.DateTime, theHash["EffectiveDate"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, theHash["Status"].ToString());
                ClsUtility.AddParameters("@MinStock", SqlDbType.Int, theHash["MinQty"].ToString());
                ClsUtility.AddParameters("@MaxStock", SqlDbType.Int, theHash["MaxQty"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHash["UserId"].ToString());
                ClsObject theItemManager = new ClsObject();
                return
                    (Int32)
                    theItemManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMaster_Futures",
                                                ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        /// <summary>
        /// Gets the store user link.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <returns></returns>
        public DataSet GetStoreUserLink(int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    UserList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStoreUserLinking_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the store by user.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public DataTable GetStoreByUser(int UserId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return
                    (DataTable)
                    UserList.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStoreNameByUserID_Futures",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Saves the update store user linking.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="Users">The users.</param>
        /// <returns></returns>
        public int SaveUpdateStoreUserLinking(int StoreId, List<int> Users)
        {
            ClsObject obj = new ClsObject();
           // int Rec = 0;

            int theRowAffected = 0;

            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@StoreId", SqlDbType.Int, StoreId);
            if (Users.Count > 0)
            {
                System.Text.StringBuilder sbUsers = new System.Text.StringBuilder("<root>");
                foreach (int userId in Users)
                {

                    sbUsers.Append("<user>");
                    sbUsers.Append("<id>" + userId.ToString() + "</id>");
                    sbUsers.Append("</user>");
                }
                sbUsers.Append("</root>");
                ClsUtility.AddExtendedParameters("@UserList", SqlDbType.Xml, sbUsers.ToString());
            }
          theRowAffected =   (int) obj.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveStoreUserlnk_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
          return theRowAffected;
        }
        /// <summary>
        /// Saves the update store user linking.
        /// </summary>
        /// <param name="dtStoreUserList">The dt store user list.</param>
        /// <returns></returns>
        //public int SaveUpdateStoreUserLinking(DataTable dtStoreUserList)
        //{
        //    lock (this)
        //    {
        //        ClsObject StoreUserLnk = new ClsObject();
        //        int Rec = 0;

        //        int theRowAffected = 0;

        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[0]["StoreID"].ToString());
        //        theRowAffected =
        //            (int)
        //            StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreUserlnk_Futures",
        //                                      ClsUtility.ObjectEnum.ExecuteNonQuery);

        //         for (int i = 0; i <= dtStoreUserList.Rows.Count - 1; i++)
        //        {
        //            Rec = Rec + 1;
        //            ClsUtility.Init_Hashtable();
        //            ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[i]["StoreID"].ToString());
        //            ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtStoreUserList.Rows[i]["USerId"].ToString());
        //            theRowAffected =
        //                (int)
        //                StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveStoreUserlnk_Futures",
        //                                          ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        }
        //        return theRowAffected;
        //    }
        //}
        /// <summary>
        /// Gets the billables.
        /// </summary>
        /// <returns></returns>
        public DataTable GetBillables()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetBillables]",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }
        
        /// <summary>
        /// Gets the item by store identifier.
        /// </summary>
        /// <param name="storeId">The store identifier.</param>
        /// <returns></returns>
        public DataTable GetItemByStoreId(int storeId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
             ClsUtility.AddExtendedParameters("@StoreId", SqlDbType.Int, storeId);
             return (DataTable)obj.ReturnObject(ClsUtility.theParams, "SCM_GetItemsByStoreId", ClsUtility.ObjectEnum.DataTable);

        }

        public DataTable GetItemsBySupplier(int supplierId, int? itemTypeId = default(int?))
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@SupplierId", SqlDbType.Int, supplierId);
            if (itemTypeId.HasValue)
            { 
                ClsUtility.AddExtendedParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.Value);
            }
            return (DataTable)obj.ReturnObject(ClsUtility.theParams, "SCM_GetItemBySupplier", ClsUtility.ObjectEnum.DataTable);
        }
    }
}


