using System;
using System.Collections.Generic;
using System.Data;
using Entities.Billing;

namespace Interface.Billing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IKnockOff
    {
        /// <summary>
        /// Gets the voucher by identifier.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <returns></returns>
        PaymentVoucher GetVoucherById(int voucherId, bool withDetails=false);
        /// <summary>
        /// Gets all vouchers.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="voucherType">Type of the voucher.</param>
        /// <param name="withDetail">if set to <c>true</c> [with detail].</param>
        /// <returns></returns>
        List<PaymentVoucher> GetAllVouchers(int locationId, DateTime? from= null, DateTime? to=null,string voucherType="", bool withDetail=false);
        /// <summary>
        /// Gets the transcation knock.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        List<KnockOff> GetKnockOffs(int? transactionId= null, int? voucherId = null);
        /// <summary>
        /// Saves the voucher.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        PaymentVoucher SaveVoucher(PaymentVoucher voucher, int userId);
        /// <summary>
        /// Gets the credit transactions.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns></returns>
        DataTable GetCreditTransactions(int locationId, DateTime from, DateTime to, int paymentType, string billNumber = "", PaymentVoucher voucher = null);
        /// <summary>
        /// Deletes the voucher.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteVoucher(int voucherId, int userId);
        /// <summary>
        /// Knocks the off transactions.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="transactions">The transactions.</param>
        void KnockOffTransactions(PaymentVoucher voucher, int userId, List<KnockOff> transactions);
    }
}
