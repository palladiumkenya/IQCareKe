using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Billing;
using Interface.Billing;
using Entities.Common;

namespace BusinessProcess.Billing
{
    public class BBilling : ProcessBase, IBilling
    {
        #region Price

        private BBilling _bMGr;

        public BBilling()
        {
            _bMGr = this;
        }

        /// <summary>
        /// Gets the item price.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="billingDate">The billing date.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public double GetItemPrice(int itemId, int itemTypeId, DateTime billingDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@ItemID", SqlDbType.Int, itemId);

                    if (billingDate.TimeOfDay.TotalSeconds == 0)
                    {
                        billingDate = billingDate.AddDays(1).AddMinutes(-1);
                    }
                    ClsUtility.AddExtendedParameters("@BillingDate", SqlDbType.DateTime, billingDate);
                    ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, itemTypeId);
                    ClsObject BillManager = new ClsObject();
                    DataTable theDt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemPriceOnDate", ClsUtility.ObjectEnum.DataTable);
                    if (theDt.Rows.Count > 0)
                    {
                        double itemPrice = 0.0D;
                        double.TryParse(theDt.Rows[0]["SellingPrice"].ToString(), out itemPrice);
                        return itemPrice;
                    }
                    return 0.0D;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the item price.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="priceDate">The price date.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Price GetItemPrice(int itemId, int itemTypeId, DateTime? priceDate = null)
        {
            try
            {
                Price _priceOnDate = null;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ItemID", SqlDbType.Int, itemId);
                DateTime BillingDate;
                if (priceDate.HasValue)
                {
                    BillingDate = priceDate.Value;
                }
                else { BillingDate = DateTime.Now; }
                if (BillingDate.TimeOfDay.TotalSeconds == 0)
                {
                    priceDate = BillingDate.AddDays(1).AddMinutes(-1);
                }
                ClsUtility.AddExtendedParameters("@BillingDate", SqlDbType.DateTime, BillingDate);
                ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, itemTypeId);
                ClsObject BillManager = new ClsObject();
                DataTable theDt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemPriceOnDate", ClsUtility.ObjectEnum.DataTable);
                if (theDt.Rows.Count > 0)
                {
                    decimal itemPrice = 0.0M;
                    decimal.TryParse(theDt.Rows[0]["SellingPrice"].ToString(), out itemPrice);
                    _priceOnDate = new Price()
                    {
                        IsCurrent = Convert.ToBoolean(theDt.Rows[0]["IsCurrentPrice"]),
                        Age = int.Parse(theDt.Rows[0]["PriceAge"].ToString()),
                        Id = int.Parse(theDt.Rows[0]["PriceId"].ToString()),
                        Amount = itemPrice,
                        IsBundled = Convert.ToBoolean(theDt.Rows[0]["PharmacyPriceType"]),
                        EffectiveDate = Convert.ToDateTime(theDt.Rows[0]["EffectiveDate"]),

                    };
                    try { _priceOnDate.VersionStamp = Convert.ToUInt64(theDt.Rows[0]["VersionStamp"].ToString()); }
                    catch { }
                }
                return _priceOnDate;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        public DataTable GetPriceList(int itemType)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, itemType.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPriceList", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemTypeId">The item type identifier.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="withPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public ResultSet<SaleItem> GetPriceList(int? itemTypeId, DateTime? priceDate = null, string itemName = "", bool withPriceOnly = false, Pager page = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    // Dictionary<int, List<SaleItem>> resultSets = new Dictionary<int, List<SaleItem>>();
                    if (itemTypeId.HasValue)
                        ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, itemTypeId.Value);
                    if (priceDate.HasValue)
                        ClsUtility.AddExtendedParameters("@PriceDate", SqlDbType.DateTime, priceDate.Value);
                    if (itemName.Trim() != "")
                        ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, itemName);
                    ClsUtility.AddExtendedParameters("@WithPriceOnly", SqlDbType.Bit, withPriceOnly);
                    if (page != null)
                    {
                        ClsUtility.AddExtendedParameters("@Paged", SqlDbType.Bit, true);
                        ClsUtility.AddExtendedParameters("@PageIndex", SqlDbType.Int, page.PageIndex);
                        ClsUtility.AddExtendedParameters("@PageCount", SqlDbType.Int, page.PageCount);
                    }
                    else
                    {
                        ClsUtility.AddExtendedParameters("@Paged", SqlDbType.Bit, false);
                    }
                    ClsObject BillManager = new ClsObject();
                    DataSet ds = (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPriceList", ClsUtility.ObjectEnum.DataSet);

                    DateTime? emptyDate = null;
                    UInt64? emptyU64Int = null;
                    decimal? emptyDecimal = null;
                    bool? emptyBool = false;

                    DataTable dt = ds.Tables[0];
                    int pages = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                    var _priceList = (from row in dt.AsEnumerable()
                                      select new SaleItem
                                      {
                                          ItemId = Convert.ToInt32(row["ItemID"]),
                                          ItemName = row["ItemName"].ToString(),
                                          ItemTypeId = Convert.ToInt32(row["ItemTypeID"]),
                                          ItemTypeName = row["ItemTypeName"].ToString(),
                                          PriceDate = row["PriceDate"] != DBNull.Value ? Convert.ToDateTime(row["PriceDate"]) : emptyDate,
                                          PricedPerItem = row["PharmacyPriceType"] != DBNull.Value ? !Convert.ToBoolean(row["PharmacyPriceType"]) : emptyBool,
                                          SellingPrice = row["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(row["SellingPrice"]) : emptyDecimal,
                                          Active = Convert.ToBoolean(row["Active"]),
                                          VersionStamp = row["VersionStamp"] != DBNull.Value ? Convert.ToUInt64(row["VersionStamp"]) : emptyU64Int
                                      }
                                       ).ToList<SaleItem>();
                    ResultSet<SaleItem> resultSet = new ResultSet<SaleItem>() { Count = pages, Items = _priceList };
                    //  resultSets.Add(pages, _priceList);
                    return resultSet;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the price list XML.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public String GetPriceListXML(string facilityName, string userName, DateTime? PriceDate = null)
        {
            try
            {
                ResultSet<SaleItem> resultSet = this.GetPriceList(null, PriceDate, "", true, null);
                XDocument docX = new XDocument(
                       new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement("Report",
                           new XElement("Summary",
                               new XElement("Facility_Name", facilityName),
                               new XElement("User_Details", userName),
                               new XElement("Report_Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                               new XElement("Price_Date", PriceDate.HasValue ? PriceDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"))
                               ),
                           new XElement("Data",
                               new XElement("ItemTypes",
                                   (from it in resultSet.Items
                                    group it by new
                                    {
                                        it.ItemTypeId,
                                        it.ItemTypeName
                                    } into gcs
                                    select new XElement("ItemType",
                                        new XElement("ItemTypeID", gcs.Key.ItemTypeId),
                                        new XElement("ItemTypeName", gcs.Key.ItemTypeName),
                                        new XElement("ItemCount", gcs.Count())
                                     )
                                   )
                             ),
                             new XElement("PriceList",
                                 (
                                   from item in resultSet.Items
                                   select new XElement("Item",
                                      new XElement("ItemID", item.ItemId),
                                      new XElement("ItemName", item.ItemName),
                                      new XElement("ItemTypeID", item.ItemTypeId),
                                      new XElement("ItemTypeName", item.ItemTypeName),
                                      new XElement("SellingPrice", item.SellingPrice),
                                      new XElement("PriceDate", item.PriceDate),
                                      new XElement("PricedPerItem", item.PricedPerItem)
                                   )
                                   )
                                )
                           )
                         )
                   );
                return docX.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="dtPriceList">The dt price list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int SavePriceList(DataTable dtPriceList, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsObject ItemList = new ClsObject();
                    int theRowAffected = 0;
                    foreach (DataRow theDR in dtPriceList.Rows)
                    {
                        if (theDR["Item Selling Price"].ToString() == "") continue;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddExtendedParameters("@itemID", SqlDbType.Int, int.Parse(theDR["ID"].ToString()));
                        ClsUtility.AddExtendedParameters("@itemType", SqlDbType.Int, int.Parse(theDR["BillingTypeID"].ToString()));
                        ClsUtility.AddExtendedParameters("@itemSellingPrice", SqlDbType.Decimal, Convert.ToDecimal(theDR["Item Selling Price"]));
                        ClsUtility.AddParameters("@effectiveDate", SqlDbType.Int, theDR["Effective Date"].ToString());
                        ClsUtility.AddParameters("@PharmacyPriceType", SqlDbType.Int, theDR["PharmacyPriceType"].ToString());
                        ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(userId.ToString()));
                        theRowAffected = (int)ItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_SavePriceList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="itemList">The item list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int SavePriceList(List<SaleItem> itemList, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsObject obj = new ClsObject();
                    int theRowAffected = 0;
                    // int itemsCount = itemList.Count;
                    foreach (SaleItem saleItem in itemList)
                    {
                        if (saleItem.SellingPrice.HasValue)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddExtendedParameters("@itemID", SqlDbType.Int, saleItem.ItemId);
                            ClsUtility.AddExtendedParameters("@itemType", SqlDbType.Int, saleItem.ItemTypeId);
                            ClsUtility.AddExtendedParameters("@itemSellingPrice", SqlDbType.Decimal, saleItem.SellingPrice);
                            if (!saleItem.PriceDate.HasValue) saleItem.PriceDate = DateTime.Now;
                            ClsUtility.AddParameters("@effectiveDate", SqlDbType.DateTime, saleItem.PriceDate.Value.ToString("dd-MMM-yyyy"));
                            ClsUtility.AddExtendedParameters("@PharmacyPriceType", SqlDbType.Bit, !saleItem.PricedPerItem);
                            if (saleItem.VersionStamp.HasValue)
                            {
                                ClsUtility.AddExtendedParameters("@VersionStamp", SqlDbType.BigInt, Convert.ToInt64(saleItem.VersionStamp));
                            }
                            ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, saleItem.Active);
                            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(userId.ToString()));
                            int x = Convert.ToInt32(((DataRow)obj.ReturnObject(
                                 ClsUtility.theParams,
                                 "pr_Billing_SavePriceList",
                                 ClsUtility.ObjectEnum.DataRow))[0]);
                            theRowAffected += x;
                        }
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion Price

        /// <summary>
        /// Gets the bill details.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillDetails(int BillID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    // ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable patientBills = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillDetails", ClsUtility.ObjectEnum.DataTable);
                    return patientBills;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the items in a bill.
        /// </summary>
        /// <param name="billId">The bill identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillItems(int billId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    //ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, billId.ToString());
                    // ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, DBNull.Value);
                    ClsObject BillManager = new ClsObject();
                    DataTable billItems = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetPatientsBilItems", ClsUtility.ObjectEnum.DataTable);
                    return billItems;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the bill transactions.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillTransactions(int billId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, billId.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable billTran = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillTransactions", ClsUtility.ObjectEnum.DataTable);
                    return billTran;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the outstanding bill.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable getOutstandingBill(int patientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetOutstandingBill", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the patient bill by status.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="billStatus">The bill status.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientBillByStatus(int patientId, int locationId, BillStatus billStatus)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    if (billStatus == BillStatus.All)
                    {
                        //ClsUtility.AddExtendedParameters("@BillStatus", SqlDbType.Int, DBNull.Value);
                    }
                    else ClsUtility.AddExtendedParameters("@BillStatus", SqlDbType.Int, (int)billStatus);
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable patientBills = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillDetails", ClsUtility.ObjectEnum.DataTable);
                    return patientBills;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the patient un billed items.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientUnBilledItems(int patientId, int locationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    // ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, DBNull.Value);
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetPatientsBilItems", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the open bills.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="PaymentStatus">The payment status.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientWithUnpaidItems(int locationId, DateTime? dateFrom, DateTime? dateTo)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    //if(patientId.HasValue)
                    //    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.Value.ToString());
                    if (dateFrom.HasValue)
                        ClsUtility.AddExtendedParameters("@DateFrom", SqlDbType.DateTime, dateFrom.Value);
                    if (dateTo.HasValue)
                        ClsUtility.AddExtendedParameters("@DateTo", SqlDbType.DateTime, dateTo.Value);
                    // ClsUtility.AddParameters("@PaymentStatus", SqlDbType.Int, "0");
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_getOpenBills", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the billables items.
        /// </summary>
        /// <param name="billableId">The billable identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemList">The item list.</param>
        public void SaveBillablesItems(int billableId, int userID, DataTable itemList)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillableID", SqlDbType.Int, billableId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());

                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (DataRow row in itemList.Rows)
                    {
                        sbItems.Append("<row>");
                        sbItems.Append("<BillingTypeID>" + row["BillTypeID"].ToString() + "</BillingTypeID>");
                        sbItems.Append("<ItemID>" + row["ID"].ToString() + "</ItemID>");

                        sbItems.Append("</row>");
                    }
                    sbItems.Append("</root>");
                    ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                    ClsObject BillablesManager = new ClsObject();
                    BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_SaveBillablesItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the patient bill payments.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="paymentInfo">The payment information.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Receipt SavePatientBillPayments(int patientId, BillPaymentInfo paymentInfo, int userId)
        {
            lock (this)
            {
                ClsObject objItemList = null;
                Receipt receipt = null;
                List<BillItem> itemsPaid = paymentInfo.ItemsToPay;
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                if (itemsPaid != null && itemsPaid.Count > 0)
                {
                    foreach (BillItem item in itemsPaid)
                    {
                        sbItems.Append("<parameter>");
                        sbItems.Append("<billitemid>" + item.BillItemId.ToString() + "</billitemid>");
                        sbItems.Append("</parameter>");
                    }
                }
                sbItems.Append("</root>");
                // int theTransactionID = 0;
                // DataRow billItemRow = paymentInfo.Rows[0];
                DataTable dt = null;
                try
                {
                    if (paymentInfo.Deposit)
                    {
                        int locationID = paymentInfo.LocationId;
                        this.ExecuteDepositTransaction(
                                patientId,
                                locationID,
                                userId,
                                paymentInfo.Amount,
                                DepositTransactionType.Settlement,
                                objItemList
                              );
                    }

                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    objItemList = new ClsObject();
                    objItemList.Connection = this.Connection;
                    objItemList.Transaction = this.Transaction;
                    //  DataTable dtdeposit = null;

                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, paymentInfo.BillId);
                    ClsUtility.AddExtendedParameters("@TransactionType", SqlDbType.Int, paymentInfo.PaymentMode.Id);
                    if (null != receipt)
                    {
                        string depositReference = receipt.ReceiptNumber;
                        //dtdeposit.Rows[0]["TransactionReference"].ToString();
                        ClsUtility.AddExtendedParameters("@RefNumber", SqlDbType.VarChar, depositReference);
                    }
                    else
                    {
                        ClsUtility.AddExtendedParameters("@RefNumber", SqlDbType.VarChar, paymentInfo.ReferenceNumber);
                    }
                    if (paymentInfo.Narrative != "")
                    {
                        ClsUtility.AddExtendedParameters("@Narrative", SqlDbType.VarChar, paymentInfo.Narrative);
                    }
                    ClsUtility.AddExtendedParameters("@Amount", SqlDbType.Decimal, paymentInfo.Amount);
                    ClsUtility.AddExtendedParameters("@AmountPayable", SqlDbType.Decimal, paymentInfo.AmountPayable);
                    if (paymentInfo.ChosenDiscountPlan != null)
                    {
                        ClsUtility.AddExtendedParameters("@DiscountRate", SqlDbType.Decimal, paymentInfo.ChosenDiscountPlan.Rate);
                    }
                    ClsUtility.AddExtendedParameters("@TenderedAmount", SqlDbType.Decimal, paymentInfo.TenderedAmount);
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
                    ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, patientId);
                    ClsUtility.AddParameters("@BillStatus", SqlDbType.VarChar, "Paid");
                    if (itemsPaid != null && itemsPaid.Count > 0)
                        ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                    dt = (DataTable)objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_SaveBillPayment", ClsUtility.ObjectEnum.DataTable);
                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    if (null == receipt && null != dt)
                    {
                        int transactionId = Convert.ToInt32(dt.Rows[0]["TransactionID"]);
                        string receiptRef = dt.Rows[0]["TransactionReference"].ToString();
                        receipt = ((IBilling)this).GenerateReceipt(transactionId, userId, ReceiptType.BillPayment);
                    }

                    return receipt;
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

        private void LogDiscountTransactions(DiscountPlan plan, BillPaymentInfo paymentInfo, int transactionId, int patientId, int userId)
        {
            try
            {
                // paymentInfo.BillID
            }
            catch { }
        }

        #region Reports:

       /// <summary>
        /// Gets the billing report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="queryName">Name of the query.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataSet GetBillingReport(DateTime fromDate, DateTime toDate, int locationId, string queryName, bool hasPatientData = false)
        {
            // returns dataset with two tables.
            //Table 0 reportdata, Table 1 facilityData
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@fromDate", SqlDbType.Date, fromDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@toDate", SqlDbType.Date, toDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationId.ToString());
                    if (hasPatientData)
                    {
                        ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    }
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, queryName, ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the cashiers transaction summary.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public DataSet getCashiersTransactionSummary(int userId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@userID", SqlDbType.Int, userId.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_cashiersTransactionsSummary", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetInvoice(int billId, int locationId, int patientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, billId.ToString());
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsObject BillManager = new ClsObject();
                    DataSet theDS = (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetInvoice", ClsUtility.ObjectEnum.DataSet);
                    return theDS;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the receipt.
        /// </summary>
        /// <param name="Transactionid">The transactionid.</param>
        /// <param name="receiptType">Type of the receipt.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Receipt GetReceipt(string receiptNumber)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ReceiptNumber", SqlDbType.VarChar, receiptNumber);
          //  ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
            Receipt receipt = null;
            string commandText = "pr_Billing_GetReciept";

            ClsObject BillManager = new ClsObject();

            DataTable dt = (DataTable)BillManager.ReturnObject(
                    ClsUtility.theParams,
                    commandText,
                    ClsUtility.ObjectEnum.DataTable);
            if (null != dt && dt.Rows.Count == 1)
            {
                receipt = new Receipt()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    PatientId = Convert.ToInt32(dt.Rows[0]["Ptn_PK"]),
                    ReceiptDate = Convert.ToDateTime(dt.Rows[0]["ReceiptDate"]),
                    ReceiptNumber = (dt.Rows[0]["ReceiptNumber"].ToString()),
                    ReceiptType = (ReceiptType)Convert.ToInt32(dt.Rows[0]["ReceiptType"]),
                    ReceiptData = (dt.Rows[0]["ReceiptData"].ToString()),
                    PrintCount = Convert.ToInt32(dt.Rows[0]["PrintCount"])
                };
            }
            BillManager = null;
            dt.Dispose();
            return receipt;
        }

        /// <summary>
        /// Generates the receipt.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="receiptType">Type of the receipt.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Receipt IBilling.GenerateReceipt(int transactionId, int userId, ReceiptType receiptType)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@TransactionID", SqlDbType.Int, transactionId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            //ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
            Receipt receipt = null;
            string commandText = "pr_Billing_GeneratePaymentReceipt";
            switch (receiptType)
            {
                case ReceiptType.BillPayment:
                    commandText = "pr_Billing_GeneratePaymentReceipt";
                    break;

                case ReceiptType.BillPaymentReversal:
                    commandText = "pr_Billing_GeneratePaymentReversalReceipt";
                    break;

                case ReceiptType.DepositRefund:
                    commandText = "pr_Billing_GenerateDepositReceipt";
                    break;

                case ReceiptType.NewDeposit:
                    commandText = "pr_Billing_GenerateDepositReceipt";
                    break;

                default:
                    commandText = "pr_Billing_GeneratePaymentReceipt";
                    break;
            }
            ClsObject BillManager = new ClsObject();

            DataTable dt = (DataTable)BillManager.ReturnObject(
                    ClsUtility.theParams,
                    commandText,
                    ClsUtility.ObjectEnum.DataTable);
            if (null != dt && dt.Rows.Count == 1)
            {
                receipt = new Receipt()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    PatientId = Convert.ToInt32(dt.Rows[0]["Ptn_PK"]),
                    ReceiptDate = Convert.ToDateTime(dt.Rows[0]["ReceiptDate"]),
                    ReceiptNumber = (dt.Rows[0]["ReceiptNumber"].ToString()),
                    ReceiptType = (ReceiptType)Convert.ToInt32(dt.Rows[0]["ReceiptType"]),
                    ReceiptData = (dt.Rows[0]["ReceiptData"].ToString()),
                    PrintCount = Convert.ToInt32(dt.Rows[0]["PrintCount"]),
                    TransactionId = Convert.ToInt32(dt.Rows[0]["TransactionId"])
                };
            }
            BillManager = null;
            dt.Dispose();
            return receipt;
        }

        #endregion Reports:

        #region PaidItems

        /// <summary>
        /// Gets the paid drugs.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPaidDrugs(int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());

                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPaidDrugs", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the paid labs.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPaidLabs(int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());

                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPaidLabs", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Determines whether [is visit type paid] [the specified visittype identifier].
        /// </summary>
        /// <param name="visittypeID">The visittype identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable isVisitTypePaid(int visittypeID, int patientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsUtility.AddParameters("@itemID", SqlDbType.Int, visittypeID.ToString());
                    ClsUtility.AddParameters("@itemTypeName", SqlDbType.VarChar, "VisitType");
                    ClsObject BillManager = new ClsObject();
                    DataTable theDT = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemPayStatus", ClsUtility.ObjectEnum.DataTable);
                    /*  if (theDR[0].ToString() == "1")
                          return true;
                      else
                          return false;*/
                    return theDT;
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion PaidItems

        #region Reversal

        /// <summary>
        /// Approves the reject bill reversal.
        /// </summary>
        /// <param name="BillRervesalID">The bill rervesal identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <param name="ApproverID">The approver identifier.</param>
        /// <param name="ApprovalReason">The approval reason.</param>
        /// <param name="ApprovalDate">The approval date.</param>
        public Receipt ApproveRejectTransactionReversal(int billRervesalId, int transactionId, bool approved, int approverId, string approvalReason, DateTime approvalDate, bool refundCash = false)
        {
            try
            {
                Receipt receipt = null;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ReversalID", SqlDbType.Int, billRervesalId.ToString());
                ClsUtility.AddParameters("@TransactionID", SqlDbType.Int, transactionId.ToString());
                ClsUtility.AddParameters("@ApprovedBy", SqlDbType.Int, approverId.ToString());
                ClsUtility.AddParameters("@ApprovedStatus", SqlDbType.VarChar, (approved ? "APPROVED" : "REJECTED"));
                ClsUtility.AddParameters("@ApprovalReason", SqlDbType.VarChar, approvalReason);
                ClsUtility.AddExtendedParameters("@ApprovalDate", SqlDbType.DateTime, approvalDate);
                ClsUtility.AddExtendedParameters("@RefundCash", SqlDbType.Bit, refundCash);
                ClsObject BillManager = new ClsObject();
                DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_ApproveRejectReversal", ClsUtility.ObjectEnum.DataTable);
                if (refundCash && approved && null != dt)
                {
                    int _transactionId = Convert.ToInt32(dt.Rows[0]["TransactionID"]);
                    string receiptRef = dt.Rows[0]["TransactionReference"].ToString();
                    if (_transactionId > 0 && receiptRef != "")
                    {
                        receipt = ((IBilling)this).GenerateReceipt(_transactionId, approverId, ReceiptType.BillPaymentReversal);
                    }
                }
                return receipt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the reversal requests.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="reversalReference">The reversal reference.</param>
        /// <param name="filterOption">The filter option.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetReversalRequests(int locationId, string reversalReference = "", string filterOption = "PENDING", int? patientId = null)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                if (string.IsNullOrEmpty(reversalReference))
                    ClsUtility.AddParameters("@Reference", SqlDbType.VarChar, reversalReference);
                if (filterOption == "APPROVED")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "1");
                else if (filterOption == "PENDING")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "0");
                else if (filterOption == "REJECTED")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "2");
                if (patientId.HasValue)
                    ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, patientId.Value);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetReversals", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Itemses to be reversed.
        /// </summary>
        /// <param name="BillRervesalID">The bill rervesal identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable ItemsToBeReversed(int billRervesalId)
        {
            //lock (this)
            //{
            //    ClsUtility.Init_Hashtable();
            //    ClsUtility.AddParameters("@ReversalID", SqlDbType.Int, BillRervesalID.ToString());
            //    ClsObject BillManager = new ClsObject();
            //    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_ItemsForReversal", ClsUtility.ObjectEnum.DataTable);

            //}
            return new DataTable();
        }

        /// <summary>
        /// Refunds the cash.
        /// </summary>
        /// <param name="reversalId">The reversal identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public Receipt RefundCash(int reversalId, int userId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@ReversalId", SqlDbType.Int, reversalId.ToString());
            ClsUtility.AddParameters("@RefundedBy", SqlDbType.Int, userId.ToString());
            ClsObject BillManager = new ClsObject();
            DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RefundPayment", ClsUtility.ObjectEnum.DataTable);
            Receipt receipt = null;
            if (null != dt)
            {
                int transactionId = Convert.ToInt32(dt.Rows[0]["TransactionID"]);
                string receiptRef = dt.Rows[0]["TransactionReference"].ToString();
                if (transactionId > 0 && receiptRef != "")
                {
                    receipt = ((IBilling)this).GenerateReceipt(transactionId, userId, ReceiptType.BillPaymentReversal);
                }
            }

            BillManager = null;
            return receipt;
        }

        /// <summary>
        /// Requests the bill reversal.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="reversalReason">The reversal reason.</param>
        /// <param name="requestDate">The request date.</param>
        /// <param name="itemToReverse">The item to reverse.</param>
        public void RequestTransactionReversal(int transactionId, int userId, string reversalReason, DateTime requestDate, List<int> itemToReverse = null)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TransactionID", SqlDbType.Int, transactionId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@ReversalReason", SqlDbType.VarChar, reversalReason);
                ClsUtility.AddParameters("@RequestDate", SqlDbType.DateTime, requestDate.ToString("yyyy-MM-dd"));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (int item in itemToReverse)
                {
                    sbItems.Append("<parameter>");
                    sbItems.Append("<billitemid>" + item.ToString() + "</billitemid>");
                    sbItems.Append("</parameter>");
                }
                sbItems.Append("</root>");
                ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                ClsObject BillManager = new ClsObject();
                BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RequestForReversal", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        #endregion Reversal

        #region MakeBill

        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="billId">The bill identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CancelBill(int billId, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, billId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsObject BillManager = new ClsObject();
                    BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_CancelBill", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Generates the bill.
        /// </summary>
        /// <param name="billItems">The bill items.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GenerateBill(DataTable billItems, int patientId, int locationId, int userId)
        {
            try
            {
                decimal billAmount = Convert.ToDecimal(billItems.Compute("Sum(amount)", ""));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow item in billItems.Rows)
                {
                    sbItems.Append("<parameter>");
                    sbItems.Append("<billitemid>" + item["billitemid"].ToString() + "</billitemid>");
                    sbItems.Append("</parameter>");
                }
                sbItems.Append("</root>");
                lock (this)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddExtendedParameters("@BillAmount", SqlDbType.Decimal, billAmount);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataRow row = (DataRow)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GenerateBill", ClsUtility.ObjectEnum.DataRow);
                    return row[0].ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="billItemId">The bill item identifier.</param>
        /// <param name="billId">The bill identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RemoveItemFromBill(int billItemId, int billId, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillItemID", SqlDbType.Int, billItemId.ToString());
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, billId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsObject BillManager = new ClsObject();
                    BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RemoveItemFromBill", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveItemFromBill(List<BillItem> items, int userId)
        {
            lock (this)
            {
                foreach (BillItem item in items)
                {
                    try
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@BillItemID", SqlDbType.Int, item.BillItemId.ToString());
                        ClsUtility.AddParameters("@BillID", SqlDbType.Int, item.BillId.ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                        ClsObject BillManager = new ClsObject();
                        BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RemoveItemFromBill", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Saves the patient payable items.
        /// </summary>
        /// <param name="billItems">The bill items.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int SavePatientPayableItems(List<BillItem> billItems, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsObject objItemList = new ClsObject();

                    int theRowAffected = 0;
                    foreach (BillItem item in billItems)
                    {
                        if (!item.Active && item.BillItemId.HasValue) // we are deleting
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@billItemID", SqlDbType.Int, item.BillItemId.Value.ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                            theRowAffected += (int)objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_DeleteBillItem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                        else
                        {
                            ClsUtility.Init_Hashtable();
                            if (item.BillId.HasValue)
                                ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, item.BillId.Value);
                            ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, item.PatientId);
                            if (item.ModuleId.HasValue)
                                ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, item.ModuleId.Value);

                            ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, item.LocationId);
                            if (item.BillItemId.HasValue)
                                ClsUtility.AddExtendedParameters("@billItemID", SqlDbType.Int, item.BillItemId.Value);
                            ClsUtility.AddExtendedParameters("@billItemDate", SqlDbType.DateTime, item.BillItemDate);

                            ClsUtility.AddExtendedParameters("@PaymentStatus", SqlDbType.Int, item.Paid ? 1 : 0);
                            ClsUtility.AddExtendedParameters("@ItemId", SqlDbType.Int, item.ItemId);
                            ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, item.ItemName);
                            ClsUtility.AddExtendedParameters("@ItemType", SqlDbType.Int, item.ItemTypeId);
                            ClsUtility.AddExtendedParameters("@Quantity", SqlDbType.Int, item.Quantity);
                            ClsUtility.AddExtendedParameters("@SellingPrice", SqlDbType.Decimal, item.Amount);

                            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(userId.ToString()));
                            //if (billItemRow["serviceStatus"] != DBNull.Value || billItemRow["serviceStatus"].ToString() != "")
                            ClsUtility.AddExtendedParameters("@ServiceStatus", SqlDbType.Int, item.Received ? 1 : 0);
                            if (item.ItemSourceReference.HasValue)
                                ClsUtility.AddParameters("@ItemSourceReferenceID", SqlDbType.Int, item.ItemSourceReference.Value.ToString());
                            if (!item.Discount.HasValue)
                            {
                                item.Discount = item.CalculatedDiscount;
                            }  // float.TryParse(billItemRow["Discount"].ToString(), out result);
                            ClsUtility.AddExtendedParameters("@Discount", SqlDbType.Decimal, item.Discount);
                            theRowAffected +=
                                  (int)
                                  objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_SaveBillItem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the patient payable items.
        /// </summary>
        /// <param name="billitems">The billitems.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int SavePatientPayableItems(DataTable billitems, int userId)
        {
            lock (this)
            {
                try
                {
                    ClsObject objItemList = new ClsObject();

                    int theRowAffected = 0;

                    DataView view = billitems.DefaultView;
                    view.RowFilter = "RowStatus In ('Deleted','Added','Updated')";
                    DataTable newTable = view.ToTable();

                    foreach (DataRow billItemRow in newTable.Rows)
                    {
                        if (billItemRow["RowStatus"].ToString() == "Deleted")
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@billItemID", SqlDbType.Int, billItemRow["billItemID"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                            theRowAffected += (int)objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_DeleteBillItem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                        else
                        {
                            float result = 0.00F;
                            ClsUtility.Init_Hashtable();
                            if (billItemRow["BillID"] != DBNull.Value || billItemRow["BillID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, int.Parse(billItemRow["BillID"].ToString()));
                            ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, int.Parse(billItemRow["patientId"].ToString()));
                            if (billItemRow["ModuleID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, int.Parse(billItemRow["ModuleID"].ToString()));
                            if (billItemRow["LocationID"] != DBNull.Value)
                                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, int.Parse(billItemRow["LocationID"].ToString()));
                            if (billItemRow["billItemID"] != DBNull.Value || billItemRow["billItemID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@billItemID", SqlDbType.Int, int.Parse(billItemRow["billItemID"].ToString()));
                            ClsUtility.AddParameters("@billItemDate", SqlDbType.DateTime, Convert.ToDateTime(billItemRow["billItemDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                            // ClsUtility.AddParameters("@PaymentType", SqlDbType.Int, billItemRow["PaymentType"].ToString());
                            if (billItemRow["PaymentStatus"] != DBNull.Value || billItemRow["PaymentStatus"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@PaymentStatus", SqlDbType.Int, int.Parse(billItemRow["PaymentStatus"].ToString()));
                            ClsUtility.AddExtendedParameters("@ItemId", SqlDbType.Int, int.Parse(billItemRow["itemId"].ToString()));
                            ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, billItemRow["itemName"].ToString());
                            ClsUtility.AddExtendedParameters("@ItemType", SqlDbType.Int, int.Parse(billItemRow["itemType"].ToString()));
                            ClsUtility.AddExtendedParameters("@Quantity", SqlDbType.Int, int.Parse(billItemRow["Quantity"].ToString()));
                            ClsUtility.AddExtendedParameters("@SellingPrice", SqlDbType.Decimal, decimal.Parse(billItemRow["SellingPrice"].ToString()));
                            float.TryParse(billItemRow["Discount"].ToString(), out result);
                            ClsUtility.AddExtendedParameters("@Discount", SqlDbType.Decimal, result);
                            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(userId.ToString()));
                            if (billItemRow["serviceStatus"] != DBNull.Value || billItemRow["serviceStatus"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@ServiceStatus", SqlDbType.Int, int.Parse(billItemRow["serviceStatus"].ToString()));
                            if (billItemRow["ItemSourceReferenceID"] != DBNull.Value || billItemRow["ItemSourceReferenceID"].ToString() != "")
                                ClsUtility.AddParameters("@ItemSourceReferenceID", SqlDbType.Int, billItemRow["ItemSourceReferenceID"].ToString());

                            theRowAffected +=
                                  (int)
                                  objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_SaveBillItem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion MakeBill

        #region Deposits

        /// <summary>
        /// Executes the deposit transaction.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transationType">Type of the transation.</param>
        public Receipt ExecuteDepositTransaction(int patientId, int locationId, int userId, decimal amount, DepositTransactionType transactionType, object clsObject = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsUtility.AddExtendedParameters("@Amount", SqlDbType.Decimal, amount);
                    ClsUtility.AddExtendedParameters("@TransactionType", SqlDbType.Int, (int)transactionType);
                    ClsObject BillManager = null;
                    if (clsObject == null)
                        BillManager = new ClsObject();
                    else BillManager = (ClsObject)clsObject;

                    Receipt receipt = null;
                    DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_ExecDepositTransaction", ClsUtility.ObjectEnum.DataTable);
                    if (null != dt && dt.Rows.Count == 1)
                    {
                        int transactionId = Convert.ToInt32(dt.Rows[0]["TransactionID"]);
                        string receiptRef = dt.Rows[0]["TransactionReference"].ToString();

                        if (transactionType == DepositTransactionType.Settlement)
                        {
                            //donot create a receipt now. to be created at payment
                            //  receipt = ((IBilling)this).GenerateReceipt(transactionId, userID, ReceiptType.BillPayment);
                        }
                        else if (transactionType == DepositTransactionType.MakeDeposit)
                        {
                            receipt = ((IBilling)this).GenerateReceipt(transactionId, userId, ReceiptType.NewDeposit);
                        }
                        else
                        {
                            receipt = ((IBilling)this).GenerateReceipt(transactionId, userId, ReceiptType.DepositRefund);
                        }
                    }

                    return receipt;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the patient deposit. The summary. The recent deposit and the available amount
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientDeposit(int patientId, int locationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable theDS = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPatientDepositSummary", ClsUtility.ObjectEnum.DataTable);
                    return theDS;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets all the deposits transactions for a patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientDepositTransactions(int patientId, int locationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable theDS = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPatientDepositTransactions", ClsUtility.ObjectEnum.DataTable);
                    return theDS;
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion Deposits

        #region PaymentMethods

        /// <summary>
        /// Gets the exemption reasons.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<KeyValuePair<int, string>> GetExemptionReasons(string filterName = "")
        {
            ClsUtility.Init_Hashtable();
            ClsObject BillManager = new ClsObject();
            if (filterName != "")
            {
                ClsUtility.AddParameters("@Reason", SqlDbType.VarChar, filterName);
            }
            DataTable theDT = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetExemptionReason",
                ClsUtility.ObjectEnum.DataTable);
            return theDT.AsEnumerable()
                .ToDictionary(
                    r => Convert.ToInt32(r["ReasonId"]),
                    r => r["ReasonText"].ToString()
                ).ToList();
        }

        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<PaymentMethod> GetPaymentMethods(string filterName = "")
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject BillManager = new ClsObject();
                    if (filterName != "")
                    {
                        ClsUtility.AddParameters("@MethodName", SqlDbType.VarChar, filterName);
                    }
                    DataTable theDT = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "Pr_Billing_GetPaymentMethods",
                        ClsUtility.ObjectEnum.DataTable);
                
                    var _pm = (from row in theDT.AsEnumerable()
                               select new PaymentMethod()
                               {
                                   Id = Convert.ToInt32(row["TypeID"]),
                                   Name = row["TypeName"].ToString(),
                                   ControlName = row["PluginName"].ToString(),
                                   MethodDescription = row["TypeDescription"].ToString(),
                                   Active = Convert.ToBoolean(row["Active"]),
                                   Credit = Convert.ToBoolean(row["Credit"]),
                                   Locked = Convert.ToBoolean(row["Locked"])
                               }
                     ).ToList<PaymentMethod>();
                    return _pm;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves the payment method.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="UserId">The user identifier.</param>
        public void SavePaymentMethod(PaymentMethod p, int UserId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    if (p.Id.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@MethodID", SqlDbType.Int, p.Id);
                        ClsUtility.AddParameters("@Action", SqlDbType.VarChar, "UPDATE");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Action", SqlDbType.VarChar, "NEW");
                    }
                    // ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddParameters("@MethodName", SqlDbType.VarChar, p.Name);
                    ClsUtility.AddParameters("@TypeDescription", SqlDbType.VarChar, p.MethodDescription);
                    ClsUtility.AddParameters("@PluginName", SqlDbType.VarChar, p.ControlName);
                    ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, p.Active);
                    ClsObject BillablesManager = new ClsObject();
                    BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.Pr_Billing_ManagePaymentMethods", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion PaymentMethods

        #region QuickPanel

        /// <summary>
        /// Gets the patients items issued by user identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="itemIssueDate">The item issue date.</param>
        /// <returns></returns>
        public List<IssuableItem> GetPatientsItemsIssuedByUserID(int patientId, int locationId, int userId, DateTime? itemIssueDate, int? itemTypeId = null)
        {
            lock (this)
            {
                try
                {
                    // List<IssuableItem> resultSet = new List<IssuableItem>();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                    ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
                    if (itemIssueDate.HasValue)
                    {
                        DateTime newDate = new DateTime(itemIssueDate.Value.Year, itemIssueDate.Value.Month, itemIssueDate.Value.Day);
                        ClsUtility.AddExtendedParameters("@IssueDate", SqlDbType.DateTime, newDate);
                    }
                    if (itemTypeId.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, itemTypeId.Value);
                    }
                    ClsObject BillManager = new ClsObject();
                    DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientItemsByUserID",
                            ClsUtility.ObjectEnum.DataTable);
                    int? nullInt = null;
                    var resultSet = (from row in dt.AsEnumerable()
                                     select new IssuableItem()
                                     {
                                         ItemId = Convert.ToInt32(row["ItemID"]),
                                         ItemIssuanceId = Convert.ToInt32(row["PatientItemID"]),
                                         BillItemId = (row["billItemID"] == DBNull.Value) ? nullInt : Convert.ToInt32(row["billItemID"]),
                                         PatientId = Convert.ToInt32(row["patientId"]),
                                         LocationId = Convert.ToInt32(row["LocationID"]),
                                         ModuleId = Convert.ToInt32(row["ModuleID"]),
                                         ItemName = (row["ItemName"].ToString()),
                                         ItemTypeId = Convert.ToInt32(row["ItemTypeID"]),
                                         IssueDate = Convert.ToDateTime(row["IssueDate"]),
                                         IssuedByName = row["IssuedByName"].ToString(),
                                         IssuedById = Convert.ToInt32(row["UserID"]),
                                         IssuedQuantity = Convert.ToInt32(row["IssuedQuantity"]),
                                         SellingPrice = Convert.ToDecimal(row["SellingPrice"]),
                                         BilledAmount = (row["BilledAmount"] == DBNull.Value) ? 0.0M : Convert.ToDecimal(row["BilledAmount"]),
                                         Received = (row["ServiceStatus"] == DBNull.Value) ? false : Convert.ToBoolean(row["ServiceStatus"]),
                                         Paid = (row["PaymentStatus"] == DBNull.Value) ? false : Convert.ToBoolean(row["PaymentStatus"]),
                                         Billed = (row["BillID"] == DBNull.Value) ? false : true,
                                         Active = true,
                                         CanBeBilled = Convert.ToBoolean(row["Active"]),
                                         CostCenter = (row["CostCenterName"] == DBNull.Value) ? "" : row["CostCenterName"].ToString()
                                     }
                         ).ToList<IssuableItem>();

                    return resultSet;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Issues the item.
        /// </summary>
        /// <param name="Items">The items.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int IssueItem(List<IssuableItem> Items, int patientId, int LocationID, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsObject objItemList = new ClsObject();

                    int theRowAffected = 0;

                    List<BillItem> ItemsToBill = new List<BillItem>();
                    List<BillItem> ItemsToUpdate = new List<BillItem>();

                    var _newItems = Items.Where(it => it.ItemIssuanceId.HasValue == false);
                    var _deletedItems = Items.Where(it => it.Active == false);

                    foreach (IssuableItem item in _deletedItems)
                    {
                        this.RemoveIssuedItem(UserID, item.ItemIssuanceId.Value);
                        if (item.BillItemId.HasValue && item.Billed == false) // item already sent to dtl_bill but billed for
                        {
                            BillItem billItem;
                            billItem = new BillItem()
                            {
                                BillItemId = item.BillItemId.Value,
                                Amount = item.SellingPrice.Value,
                                ItemId = item.ItemId,
                                ItemName = item.ItemName,
                                ItemTypeId = item.ItemTypeId,
                                PatientId = item.PatientId,
                                LocationId = item.LocationId,
                                ModuleId = item.ModuleId,
                                CostCenter = item.CostCenter,
                                BillItemDate = item.IssueDate,
                                Quantity = item.IssuedQuantity,
                                Received = false,
                                Paid = false,
                                Discount = item.ItemDiscount,
                                AddedBy = item.IssuedById,
                                ItemSourceReference = item.ItemIssuanceId.Value
                            };
                            ItemsToUpdate.Add(billItem);
                        }
                    }

                    foreach (IssuableItem item in _newItems)
                    {
                        BillItem billItem;
                        bool SendBill = item.SellingPrice.HasValue;
                        if (!item.ItemIssuanceId.HasValue)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddExtendedParameters("@patientId", SqlDbType.Int, item.PatientId);
                            ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, item.LocationId);
                            ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, item.ModuleId);
                            ClsUtility.AddExtendedParameters("@Quantity", SqlDbType.Int, item.IssuedQuantity);
                            ClsUtility.AddParameters("@ItemId", SqlDbType.Int, item.ItemId.ToString());
                            ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, item.ItemTypeId.ToString());
                            ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, item.ItemName);
                            DateTime newDate = new DateTime(item.IssueDate.Year, item.IssueDate.Month, item.IssueDate.Day);
                            ClsUtility.AddExtendedParameters("@DateIssued", SqlDbType.DateTime, newDate);
                            if (item.SellingPrice.HasValue)
                            {
                                ClsUtility.AddExtendedParameters("@SellingPrice", SqlDbType.Decimal, item.SellingPrice.ToString());
                            }
                        }
                        else
                        {
                            if (!item.BillItemId.HasValue)
                            {
                                //just remove from issued item
                            }
                            else if (item.BillItemId.HasValue && item.Paid == false && item.Active == false)
                            {
                                //set billitem to service=0 then delete
                            }
                        }
                        if (SendBill)
                        {
                        }
                        ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, UserID);
                        DataRow row = (DataRow)objItemList.ReturnObject(
                            ClsUtility.theParams, "dbo.pr_Clinical_IssueItemToPatient",
                            ClsUtility.ObjectEnum.DataRow);
                        theRowAffected += 1;
                        if (SendBill && row != null)
                        {
                            billItem = new BillItem()
                            {
                                Amount = item.SellingPrice.Value,
                                ItemId = item.ItemId,
                                ItemName = item.ItemName,
                                ItemTypeId = item.ItemTypeId,
                                PatientId = item.PatientId,
                                LocationId = item.LocationId,
                                ModuleId = item.ModuleId,
                                BillItemDate = item.IssueDate,
                                Quantity = item.IssuedQuantity,
                                CostCenter = item.CostCenter,
                                Received = true,
                                Paid = false,
                                Discount = item.ItemDiscount,
                                AddedBy = item.IssuedById,
                                ItemSourceReference = int.Parse(row[0].ToString())
                            };
                            ItemsToBill.Add(billItem);
                        }
                    }
                    if (ItemsToBill.Count > 0)
                    {
                        this.SavePatientPayableItems(ItemsToBill, UserID);
                    }
                    if (ItemsToUpdate.Count > 0)
                    {
                        this.SavePatientPayableItems(ItemsToUpdate, UserID);
                        this.RemoveItemFromBill(ItemsToUpdate, UserID);
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Removes the issued item.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemIssueID">The item issue identifier.</param>
        public void RemoveIssuedItem(int userID, int itemIssueID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemIssueID", SqlDbType.Int, itemIssueID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Clinical_DeleteItemIssuedToPatient", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        #endregion QuickPanel

        #region DiscountPlans

        /// <summary>
        /// Adds the discount plan.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <param name="UserID">The user identifier.</param>
        public void AddDiscountPlan(DiscountPlan plan, int UserID)
        {//plan.DiscountedPayMethod.ID.Value,
            //int _duplicates = this.OverLappingDiscountPlans(plan.StartDate, plan.EndDate,  null);
            //if (_duplicates == 0)
            //{
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@StartDate", SqlDbType.DateTime, plan.StartDate.ToString("yyyy-MM-dd HH:mm"));
                    if (plan.EndDate.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@EndDate", SqlDbType.DateTime, plan.EndDate.Value.ToString("yyyy-MM-dd HH:mm"));
                    }
                    //  ClsUtility.AddExtendedParameters("@PaymentTypeID", SqlDbType.Int, plan.DiscountedPayMethod.ID);
                    ClsUtility.AddExtendedParameters("@Rate", SqlDbType.Decimal, plan.Rate);
                    ClsUtility.AddParameters("@DiscountName", SqlDbType.VarChar, plan.Name);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, !plan.Active);
                    ClsUtility.AddParameters("@Mode", SqlDbType.VarChar, "NEW");
                    ClsObject BillManager = new ClsObject();
                    BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_ManageDiscountPlans", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
            //}
            //else
            //{
            //    throw new Exception("There exists another discount plan for the same or overlapping date range");
            //}
        }

        /// <summary>
        /// Gets the discount plans.
        /// </summary>
        /// <param name="PaymentTypeID">The payment type identifier.</param>
        /// <param name="DiscountDate">The discount date.</param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <returns></returns>
        public List<DiscountPlan> GetDiscountPlans(int? DiscountID = null, int? PaymentTypeID = null, DateTime? DiscountDate = null, bool Active = true)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    if (DiscountID.HasValue)
                        ClsUtility.AddExtendedParameters("@DiscountID", SqlDbType.Int, DiscountID.Value);
                    if (DiscountDate.HasValue)
                        ClsUtility.AddExtendedParameters("@DiscountDate", SqlDbType.DateTime, DiscountDate.Value.ToString("yyyy-MM-dd HH:mm"));
                    if (PaymentTypeID.HasValue)
                        ClsUtility.AddExtendedParameters("@PaymentTypeID", SqlDbType.Int, PaymentTypeID.Value);
                    ClsUtility.AddExtendedParameters("@ShowActiveOnly", SqlDbType.Bit, Active);
                    ClsObject BillManager = new ClsObject();
                    DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetDiscountPlans", ClsUtility.ObjectEnum.DataTable);
                    DateTime? nullDate = null;
                    var resultSet = (from row in dt.AsEnumerable()
                                     select new DiscountPlan()
                                     {
                                         PlanId = Convert.ToInt32(row["DiscountID"]),
                                         Name = row["DiscountName"].ToString(),
                                         //    DiscountedPayMethod = new PaymentMethod() { ID = Convert.ToInt32(row["PaymentTypeID"]), Name = row["PaymentName"].ToString() },
                                         Active = Convert.ToBoolean(row["Active"]),
                                         Rate = Convert.ToDecimal(row["Rate"]),
                                         StartDate = Convert.ToDateTime(row["StartDate"]),
                                         EndDate = row["EndDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["EndDate"])
                                     }
                                          ).ToList<DiscountPlan>();
                    return resultSet;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the discount plan.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <param name="UserID">The user identifier.</param>
        public void UpdateDiscountPlan(DiscountPlan plan, int UserID)
        {//plan.DiscountedPayMethod.ID.Value,
            //int _duplicates = this.OverLappingDiscountPlans(plan.StartDate, plan.EndDate,  plan.PlanID);
            //if (_duplicates == 0)
            //{
            //}
            //else
            //{
            //    throw new Exception("There exists another discount plan for the same or overlapping date range");
            //}
        }

        /// <summary>
        /// Overs the lapping discount plans.
        /// </summary>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="PaymentTypeID">The payment type identifier.</param>
        /// <param name="DiscountID">The discount identifier.</param>
        /// <returns></returns>
        private int OverLappingDiscountPlans(DateTime StartDate, DateTime EndDate, int? DiscountID = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    DateTime _dtStart = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
                    DateTime _dtEnd = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);
                    ClsUtility.AddExtendedParameters("@StartDate", SqlDbType.Date, _dtStart.ToString("yyyyMMMdd"));
                    ClsUtility.AddExtendedParameters("@EndDate", SqlDbType.Date, _dtEnd.ToString("yyyyMMMdd"));
                    // ClsUtility.AddExtendedParameters("@PaymentTypeID", SqlDbType.Int, PaymentTypeID);
                    if (DiscountID.HasValue)
                        ClsUtility.AddExtendedParameters("@DiscountID", SqlDbType.Int, DiscountID.Value);
                    ClsObject BillManager = new ClsObject();
                    DataRow dataRow = (DataRow)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_OverlappingPlans", ClsUtility.ObjectEnum.DataRow);

                    return (int)dataRow["Duplicates"];
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion DiscountPlans


        public List<ItemType> GetBillingType()
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    
                    ClsObject BillManager = new ClsObject();
                    DataTable dt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemType", ClsUtility.ObjectEnum.DataTable);

                    var resultSet = (from row in dt.AsEnumerable()
                                     select new ItemType()
                                     {
                                         Id = Convert.ToInt32(row["TypeId"]),
                                         Name = row["Name"].ToString(),
                                         ContainerName = row["ContainerName"].ToString(),
                                         ColumnItemIdentifier = row["ColumnItemIdentifier"].ToString(),
                                         ColumnItemName = row["ColumnItemName"].ToString(),
                                         Description="",
                                    }
                                          ).ToList<ItemType>();
                    return resultSet;
                }

                catch
                {
                    throw;
                }

            }
        }
    }
}