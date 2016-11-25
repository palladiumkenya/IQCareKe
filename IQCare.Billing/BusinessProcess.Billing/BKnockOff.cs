using System;
using System.Collections.Generic;
using Interface.Billing;
using DataAccess.Base;
using System.Data;
using Entities.Billing;
using DataAccess.Common;
using DataAccess.Entity;
using System.Linq;
using System.Xml.Linq;
namespace BusinessProcess.Billing
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Base.ProcessBase" />
    /// <seealso cref="Interface.Billing.IKnockOff" />
    public class BKnockOff : ProcessBase, IKnockOff
    {
        /// <summary>
        /// Deletes the voucher.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteVoucher(int voucherId, int userId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@VoucherId", SqlDbType.Int, voucherId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            ClsObject objMgr = new ClsObject();
            objMgr.ReturnObject(ClsUtility.theParams, "Billing_PaymentVoucher_Delete", ClsUtility.ObjectEnum.ExecuteNonQuery);
        }
        
        /// <summary>
        /// Gets all vouchers.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PaymentVoucher> GetAllVouchers(int locationId, DateTime? from= null, DateTime? to=null, string voucherType = "", bool withDetail = false)
        {

            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            if (from.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DateFrom", SqlDbType.DateTime, from.Value);
            }
           if (to.HasValue)
           {
               ClsUtility.AddExtendedParameters("@DateTo", SqlDbType.DateTime, to.Value);
           }
            ClsUtility.AddExtendedParameters("@VoucherType", SqlDbType.VarChar, voucherType);
            ClsObject objMgr = new ClsObject();
            List<PaymentVoucher> results = null;
            int? nullInt = null;
            DateTime? nullDate = null;
            DataTable dt = (DataTable)objMgr.ReturnObject(ClsUtility.theParams, "Billing_PaymentVoucher_GetMany", ClsUtility.ObjectEnum.DataTable);
            if (dt != null)
            {
                results = (from row in dt.AsEnumerable()
                           select new PaymentVoucher()
                           {
                               Id = Convert.ToInt32(row["Id"]),
                               LocationId = Convert.ToInt32(row["LocationId"]),
                               KnockOffs = withDetail? this.GetKnockOffs(null,Convert.ToInt32(row["Id"])): null,
                               ReferenceId = Convert.ToString(row["ReferenceId"]),
                               Description = Convert.ToString(row["Description"]),
                               Amount = Convert.ToDouble(row["Amount"]),
                               AmountUsed = Convert.ToDouble(row["AmountUsed"]),
                               VoucherDate = Convert.ToDateTime(row["VoucherDate"]),
                               VoucherType = Convert.ToString(row["VoucherType"]),
                               UserId = Convert.ToInt32(row["UserId"]),
                               CreateDate = Convert.ToDateTime(row["CreateDate"]),
                               DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                               DeleteBy = row["VoidedBy"] == DBNull.Value ? nullInt : Convert.ToInt32(row["VoidedBy"]),
                               DeleteDate = row["VoidDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["VoidDate"])

                           }).ToList();
               
            }

            return results;
        }

        /// <summary>
        /// Gets the credit transactions.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataTable GetCreditTransactions(int locationId, DateTime from, DateTime to, int paymentTypeId, string billNumber="", PaymentVoucher voucher = null)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            ClsUtility.AddExtendedParameters("@DateFrom", SqlDbType.DateTime, from);
            ClsUtility.AddExtendedParameters("@DateTo", SqlDbType.DateTime, to);
            ClsUtility.AddExtendedParameters("@PaymentTypeId", SqlDbType.Int, paymentTypeId);
            if (!string.IsNullOrEmpty(billNumber))
            {
                ClsUtility.AddExtendedParameters("@BillNumber", SqlDbType.VarChar, billNumber);
            }
            if (voucher != null)
            {
                ClsUtility.AddExtendedParameters("@VoucherId", SqlDbType.Int, voucher.Id);
            }
            ClsObject objMgr = new ClsObject();

            DataTable dt = (DataTable)objMgr.ReturnObject(ClsUtility.theParams, "Billing_KnockOff_GetTransaction", ClsUtility.ObjectEnum.DataTable);
            objMgr = null;
            return dt;
        }



        /// <summary>
        /// Knocks the off transactions.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="transactions">The transactions.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void KnockOffTransactions(PaymentVoucher voucher, int userId, List<KnockOff> transactions)
        {
             ClsUtility.Init_Hashtable();
            ClsObject objMgr = new ClsObject();
            XDocument tranList = new XDocument(
                 new XElement("root",
                                 (
                                   from item in transactions
                                   select new XElement("tran",
                                      new XElement("transactionid", item.TransactionId),
                                      new XElement("paymenttypeid", item.PaymentTypeId),
                                      new XElement("amount", item.KnockOffAmount),
                                      new XElement("description", item.Description)
                                   )
                                   )
                                ));
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            ClsUtility.AddExtendedParameters("@VoucherId", SqlDbType.Int, voucher.Id);
            ClsUtility.AddExtendedParameters("@TransactionList", SqlDbType.Xml, tranList.ToString());
            objMgr.ReturnObject(ClsUtility.theParams, "Billing_KnockOff_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
        }
       
        /// <summary>
        /// Saves the voucher.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PaymentVoucher SaveVoucher(PaymentVoucher voucher, int userId)
        {
            ClsUtility.Init_Hashtable();
            ClsObject objMgr = new ClsObject();
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, voucher.LocationId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            ClsUtility.AddExtendedParameters("@VoucherDate", SqlDbType.DateTime, voucher.VoucherDate);
            ClsUtility.AddExtendedParameters("@Amount", SqlDbType.Decimal, voucher.Amount);
            ClsUtility.AddExtendedParameters("@Description", SqlDbType.VarChar, voucher.Description);
            ClsUtility.AddExtendedParameters("@ReferenceId", SqlDbType.VarChar, voucher.ReferenceId);
            ClsUtility.AddExtendedParameters("@VoucherType", SqlDbType.VarChar, voucher.VoucherType);

            DataRow row = (DataRow)objMgr.ReturnObject(ClsUtility.theParams, "Billing_PaymentVoucher_Insert", ClsUtility.ObjectEnum.DataRow);
            objMgr = null;
            return this.GetVoucherById(Convert.ToInt32(row["Id"]), false);
        }

        /// <summary>
        /// Gets the knock offs.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <returns></returns>
        public List<KnockOff> GetKnockOffs(int? transactionId = null, int? voucherId = null)
        {
            ClsUtility.Init_Hashtable();
            if (voucherId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@VoucherId", SqlDbType.Int, voucherId.Value);
            }
            if (transactionId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@TransactionId", SqlDbType.Int, transactionId.Value);
            }
            ClsObject objMgr = new ClsObject();
            List<KnockOff> results = null;
            int? nullInt = null;
            DateTime? nullDate = null;
            DataTable dt = (DataTable)objMgr.ReturnObject(ClsUtility.theParams, "Billing_KnockOff_GetMany", ClsUtility.ObjectEnum.DataTable);
            results = (from row in dt.AsEnumerable()
                       select new KnockOff()
                       {
                           Id = Convert.ToInt32(row["Id"]),
                           TransactionId = Convert.ToInt32(row["TransactionId"]),
                           VoucherId = Convert.ToInt32(row["VoucherId"]),
                           PaymentTypeId = Convert.ToInt32(row["PaymentTypeId"]),
                           KnockOffAmount = Convert.ToDouble(row["KnockOffAmount"]),
                           TransactionAmount = Convert.ToDouble(row["TransactionAmount"]),
                           Description = Convert.ToString(row["Description"]),
                           UserId = Convert.ToInt32(row["UserId"]),
                           CreateDate = Convert.ToDateTime(row["CreateDate"]),
                           DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                           DeleteBy = row["VoidedBy"] == DBNull.Value ? nullInt : Convert.ToInt32(row["VoidedBy"]),
                           DeleteDate = row["VoidDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["VoidDate"])
                       }

                ).ToList();
            return results;
        }
        /// <summary>
        /// Gets the voucher by identifier.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <param name="withDetails">if set to <c>true</c> [with details].</param>
        /// <returns></returns>
        public PaymentVoucher GetVoucherById(int voucherId, bool withDetails = false)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@VoucherId", SqlDbType.Int, voucherId);
            ClsObject objMgr = new ClsObject();
            DataTable theDt = (DataTable)objMgr.ReturnObject(ClsUtility.theParams, "Billing_PaymentVoucher_GetOne", ClsUtility.ObjectEnum.DataTable);
            PaymentVoucher voucher = null;
            int? nullInt = null;
            DateTime? nullDate = null;
            if (theDt != null)
            {
                voucher = (from row in theDt.AsEnumerable()
                           select new PaymentVoucher()
                           {
                               Id = Convert.ToInt32(row["Id"]),
                               LocationId = Convert.ToInt32(row["LocationId"]),
                               KnockOffs = null,
                               ReferenceId = Convert.ToString(row["ReferenceId"]),
                               Description = Convert.ToString(row["Description"]),
                               Amount = Convert.ToDouble(row["Amount"]),
                               VoucherDate = Convert.ToDateTime(row["VoucherDate"]),
                               VoucherType = Convert.ToString(row["VoucherType"]),
                               AmountUsed = Convert.ToDouble(row["AmountUsed"]),
                               UserId = Convert.ToInt32(row["UserId"]),
                               CreateDate = Convert.ToDateTime(row["CreateDate"]),
                               DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                               DeleteBy = row["VoidedBy"] == DBNull.Value ? nullInt : Convert.ToInt32(row["VoidedBy"]),
                               DeleteDate = row["VoidDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["VoidedDate"])

                           }).DefaultIfEmpty(null).FirstOrDefault();
                if (withDetails && voucher != null)
                {
                    voucher.KnockOffs = this.GetKnockOffs(null,voucher.Id);
                }
            }
            return voucher;
        }





        
    }
}
