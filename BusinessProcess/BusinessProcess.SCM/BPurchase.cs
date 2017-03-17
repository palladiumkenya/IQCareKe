using System;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;

namespace BusinessProcess.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public class BPurchase : ProcessBase, IPurchase
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BPurchase"/> class.
        /// </summary>
        public BPurchase()
        {
        }

        #endregion

        /// <summary>
        /// Saves the purchase order.
        /// </summary>
        /// <param name="DtMasterPO">The dt master po.</param>
        /// <param name="dtPOItems">The dt po items.</param>
        /// <param name="isUpdate">if set to <c>true</c> [is update].</param>
        /// <returns></returns>
        public int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int orderId = 0;
                DataRow theDR;

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
                ClsUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
                ClsUtility.AddParameters("@OrderDate", SqlDbType.DateTime, DtMasterPO.Rows[0]["OrderDate"].ToString());
                ClsUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
                ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
                ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
                ClsUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
                if (DtMasterPO.Rows[0]["PONumber"] != null && DtMasterPO.Rows[0]["PONumber"] != DBNull.Value)
                {
                    ClsUtility.AddParameters("@PONumber", SqlDbType.VarChar, DtMasterPO.Rows[0]["PONumber"].ToString());
                }
                if (isUpdate)
                {
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
                    ClsUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

                    if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "5");
                    }
                    else
                    {
                        if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                        }
                        else
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                        }
                       
                    }
                }
                else
                {
                    if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                    }
                }

                theDR =
                    (DataRow)
                    PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Futures",
                                          ClsUtility.ObjectEnum.DataRow);
                orderId = System.Convert.ToInt32(theDR[0].ToString());

                if (isUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@POId", SqlDbType.Int, orderId);
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < dtPOItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@POId", SqlDbType.Int, orderId.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtPOItems.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, dtPOItems.Rows[i]["ItemTypeId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal, dtPOItems.Rows[i]["priceperunit"].ToString());
                    //  ClsUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

                    ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
                    ClsUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, dtPOItems.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UnitQuantity", SqlDbType.Int, dtPOItems.Rows[i]["UnitQuantity"].ToString());
                    if(dtPOItems.Rows[i]["SupplierId"] != DBNull.Value)
                    {
                        ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, dtPOItems.Rows[i]["SupplierId"].ToString());
                    }
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return orderId;
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
        /// Gets the purcase order item.
        /// </summary>
        /// <param name="isPO">The is po.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns></returns>
        public DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject GetPurcahseItem = new ClsObject();
                ClsUtility.AddParameters("@isPO", SqlDbType.Int, isPO.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());

                return
                    (DataSet)
                    GetPurcahseItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurcaseOrderItem",
                                                 ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Gets the purchase order details by poid.
        /// </summary>
        /// <param name="POId">The po identifier.</param>
        /// <returns></returns>
        public DataSet GetPurchaseOrderDetailsByPoid(int POId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderDetailsByPoid_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        /// <summary>
        /// Gets the purchase order details.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="DestinStoreID">The destin store identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DestinStoreID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

                    return
                        (DataTable)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetails_Futures",
                                                  ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        /// <summary>
        /// Gets the purchase order details for GRN.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="IsSourceStore">if set to <c>true</c> [is source store].</param>
        /// <returns></returns>
        public DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID, bool IsSourceStore = false)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddExtendedParameters("@IsSourceStore", SqlDbType.Bit, IsSourceStore);


                    return
                        (DataTable)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetailsForGRN_Futures",
                                                  ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        /// <summary>
        /// Gets the purchase order details by poid GRN.
        /// </summary>
        /// <param name="POId">The po identifier.</param>
        /// <returns></returns>
        public DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderGRNByPoid_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        /// <summary>
        /// Gets the open stock.
        /// </summary>
        /// <returns></returns>
        public DataSet GetOpenStock()
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    return
                        (DataSet)
                        OpeningStock.ReturnObject(ClsUtility.theParams, "pr_SCM_GetOpeningStock_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
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
        }
        /// <summary>
        /// Saves the update opening stock.
        /// </summary>
        /// <param name="theDTOPStock">The dtop stock.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="TransactionDate">The transaction date.</param>
        /// <returns></returns>
        public int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int theRowAffected = 0;
                for (int i = 0; i < theDTOPStock.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDTOPStock.Rows[i]["BatchId"].ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar,
                                             theDTOPStock.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@TransactionDate", SqlDbType.DateTime, TransactionDate.ToString());
                    theRowAffected =
                        (int)
                        StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveOpeningStock_Futures",
                                                  ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }

        }
        /// <summary>
        /// Saves the update stock adjustment.
        /// </summary>
        /// <param name="theDTAdjustStock">The dt adjust stock.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="AdjustmentDate">The adjustment date.</param>
        /// <param name="AdjustmentPreparedBy">The adjustment prepared by.</param>
        /// <param name="AdjustmentAuthorisedBy">The adjustment authorised by.</param>
        /// <param name="Updatestock">The updatestock.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId,
                                            string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
                                            int Updatestock, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreAdjust = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures", ClsUtility.ObjectEnum.DataRow);

             
                    for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                        {

                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                            ClsUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                            ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                            ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDTAdjustStock.Rows[i]["BatchId"].ToString());
                            ClsUtility.AddExtendedParameters("@ExpiryDate", SqlDbType.DateTime, Convert.ToDateTime(theDTAdjustStock.Rows[i]["ExpiryDate"].ToString()));
                            ClsUtility.AddParameters("@StoreId", SqlDbType.Int,theDTAdjustStock.Rows[i]["StoreId"].ToString());
                            ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            ClsUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,  theDTAdjustStock.Rows[i]["UnitId"].ToString());
                            ClsUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
                            theRowAffected =
                                (int)
                                ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockTransAdjust_Futures",ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                    }
          
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
        /// Saves the goodreceived notes.
        /// </summary>
        /// <param name="DtMasterGRN">The dt master GRN.</param>
        /// <param name="dtGRNItems">The dt GRN items.</param>
        /// <param name="IsPOorIST">The is p oor ist.</param>
        /// <returns></returns>
        public int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int GrnId = 0;
                DataRow theDR;

                if (DtMasterGRN.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
                    {
                        if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
                            ClsUtility.AddParameters("@LocationID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["LocationID"].ToString());
                            ClsUtility.AddParameters("@RecievedStoreID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
                            ClsUtility.AddParameters("@Freight", SqlDbType.VarChar,
                                                     DtMasterGRN.Rows[0]["Freight"].ToString());
                            ClsUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
                            theDR =
                                (DataRow)
                                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNMaster_Futures",
                                                      ClsUtility.ObjectEnum.DataRow);
                            GrnId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }

                if (GrnId == 0)
                {
                    GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
                }
                for (int i = 0; i < dtGRNItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
                    {
                        if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
                        {

                            ClsUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
                            ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
                            //ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
                            ClsUtility.AddParameters("@batchName", SqlDbType.VarChar,
                                                     dtGRNItems.Rows[i]["batchName"].ToString());
                            ClsUtility.AddParameters("@RecievedQuantity", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

                            ClsUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int,
                                (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

                            ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
                                                (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
                            ClsUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
                            ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
                            ClsUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
                                                     (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
                            ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                     dtGRNItems.Rows[i]["ExpiryDate"].ToString());

                            ClsUtility.AddParameters("@UserID", SqlDbType.Int,
                                 (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
                            ClsUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
                            ClsUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
                            ClsUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
                            ClsUtility.AddParameters("@destinationStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["DestinStoreID"].ToString());
                            ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["SourceStoreID"].ToString());
                            //ClsUtility.AddParameters("@InKindFlag", SqlDbType.Int,
                            //                         dtGRNItems.Rows[i]["InKindFlag"].ToString());


                            theRowAffected =
                                (int)
                                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNItems_Futures",
                                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GrnId;
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
        /// Gets the dispose stock.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="AsofDate">The asof date.</param>
        /// <returns></returns>
        public DataSet GetDisposeStock(int StoreId, DateTime AsofDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@AsofDate", SqlDbType.DateTime, AsofDate.ToString());

                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDisposeStock_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        /// <summary>
        /// Saves the dispose items.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="AsofDate">The asof date.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="theDT">The dt.</param>
        /// <returns></returns>
        public int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreDispose = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@DisposeDate", SqlDbType.VarChar, AsofDate.ToString());
                ClsUtility.AddParameters("@DisposePreparedBy", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DisposeAuthorisedBy", SqlDbType.Int, UserId.ToString());
                DataRow theDR = (DataRow)ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures", ClsUtility.ObjectEnum.DataRow);
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (Convert.ToInt32(!DBNull.Value.Equals(theDT.Rows[i]["Dispose"])) == 1)
                    {
                        ClsUtility.AddParameters("@DisposeId", SqlDbType.Int, theDR["DisposeId"].ToString());
                        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDT.Rows[i]["ItemId"].ToString());
                        ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDT.Rows[i]["BatchId"].ToString());
                        ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                 theDT.Rows[i]["ExpiryDate"].ToString());
                        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDT.Rows[i]["StoreId"].ToString());
                        ClsUtility.AddParameters("@Quantity", SqlDbType.Int, "-" + theDT.Rows[i]["Quantity"].ToString());
                        ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserId.ToString());
                        theRowAffected =
                            (int)
                            ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures",
                                                         ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        /// Gets the stockfor adjustment.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="AdjustmentDate">The adjustment date.</param>
        /// <returns></returns>
        public DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject GetStockItem = new ClsObject();
                    ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                    return
                        (DataSet)
                        GetStockItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStockforAdjustment_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
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
        }



    }

}
