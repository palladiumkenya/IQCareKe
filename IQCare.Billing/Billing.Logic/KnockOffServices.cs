using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Presentation;
using Entities.Billing;
using Interface.Billing;
using IQCare.Web.UILogic;
using System.Linq.Expressions;

namespace IQCare.Billing.Logic
{
    public class DateRange
    {
        /// <summary>
        /// The date from
        /// </summary>
        public readonly DateTime DateFrom;
        /// <summary>
        /// The date to
        /// </summary>
        public readonly DateTime DateTo;
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        public DateRange(DateTime dateFrom, DateTime dateTo)
        {
            this.DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class KnockOffServices
    {
        public static string PropertyName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }
        /// <summary>
        /// 
        /// </summary>
        public enum ResponseCode
        {
            /// <summary>
            /// The ok
            /// </summary>
            Ok = 200,
            /// <summary>
            /// The unauthorized
            /// </summary>
            Unauthorized = 401,
            BadRequest = 400,
            /// <summary>
            /// The resource not found
            /// </summary>
            ResourceNotFound = 404,
            /// <summary>
            /// The new voucher success
            /// </summary>
            NewVoucherSuccess = 100,
            /// <summary>
            /// The new voucher fail
            /// </summary>
            NewVoucherFail = -100,

            /// <summary>
            /// The exceed knock off amount
            /// </summary>
            ExceedKnockOffAmount = 200,


        }
        /// <summary>
        /// Deletes the voucher.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <param name="session">The session.</param>
        public void DeleteVoucher(int voucherId, CurrentSession session)
        {
            IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");
            if (session.HasFeaturePermission("BILLING_MODULE"))
            {
                PaymentVoucher voucher = mgr.GetVoucherById(voucherId, false);
                if (voucher != null && voucher.KnockOffs == null && voucher.LocationId == session.Facility.Id)
                {
                    mgr.DeleteVoucher(voucher.Id, session.User.Id);
                }
            }
        }
        /// <summary>
        /// Gets the credit transactions.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <param name="billNumber">The bill number.</param>
        /// <param name="voucher">The voucher.</param>
        /// <returns></returns>
        public DataTable GetCreditTransactions(CurrentSession session, DateRange range, int paymentType, string billNumber = "", PaymentVoucher voucher = null)
        {
            IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");
            return mgr.GetCreditTransactions(session.Facility.Id, range.DateFrom, range.DateTo, paymentType, billNumber, voucher);
        }
        /// <summary>
        /// Gets the vouchers.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="voucherType">Type of the voucher.</param>
        /// <param name="withDetails">if set to <c>true</c> [with details].</param>
        /// <param name="openOnly">if set to <c>true</c> [open only].</param>
        /// <returns></returns>
        public List<PaymentVoucher> GetVouchers(CurrentSession session, DateRange range, string voucherType = "", bool withDetails = false, bool openOnly = false)
        {
            IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");            
            List<PaymentVoucher> vouchers = new List<PaymentVoucher>();
            if (range == null)
            {
              vouchers = mgr.GetAllVouchers(session.Facility.Id,null, null, voucherType, withDetails);
            }
            else
            {

            }
            if (openOnly)
            {
                return vouchers.Where(v => v.AmountAvailable > 0.0D).ToList();
            }
            return vouchers;
        }
        public List<PaymentVoucher> GetOpenVoucher(CurrentSession session)
        {
            return this.GetVouchers(session, null, "", true, true);
        }
        /// <summary>
        /// Saves the voucher.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="voucherdDate">The voucherd date.</param>
        /// <param name="voucherAmount">The voucher amount.</param>
        /// <param name="voucherType">Type of the voucher.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public ResponseCode SaveVoucher(CurrentSession session, DateTime voucherdDate, double voucherAmount, string voucherType, string referenceId, string description)
        {
            if (session.HasFeaturePermission("BILLING_MODULE"))
            {
                IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");
                PaymentVoucher voucher = new PaymentVoucher()
                {
                    Amount = voucherAmount,
                    DeleteBy = null,
                    DeleteDate = null,
                    Description = description,
                    LocationId = session.Facility.Id,
                    UserId = session.User.Id,
                    VoucherDate = voucherdDate,
                    VoucherType = voucherType,
                    ReferenceId = referenceId,
                    Id = -1
                };
                PaymentVoucher newVoucher = mgr.SaveVoucher(voucher, session.User.Id);

                mgr = null;
                return ResponseCode.Ok;
            }
            return ResponseCode.Unauthorized;
        }

        List<KnockOff> GetTransactionKnockOffs(int transactionId)
        {
            IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");
            return mgr.GetKnockOffs(transactionId, null);
        }
        /// <summary>
        /// Knocks the off transaction.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public ResponseCode KnockOffTransaction(CurrentSession session, int voucherId, List<KnockOff> transaction)
        {
            IKnockOff mgr = (IKnockOff)ObjectFactory.CreateInstance("BusinessProcess.Billing.BKnockOff, BusinessProcess.Billing");
            if (session.HasFeaturePermission("BILLING_MODULE"))
            {
                PaymentVoucher voucher = mgr.GetVoucherById(voucherId, false);

                if (voucher != null && voucher.LocationId == session.Facility.Id)
                {
                    Double amountToKO = transaction.Sum(t => t.KnockOffAmount);
                    if (voucher.AmountAvailable < amountToKO)
                    {
                        return ResponseCode.ExceedKnockOffAmount;
                    }
                    List<KnockOff> _validKnockOff = new List<KnockOff>();
                    foreach (KnockOff k in transaction)
                    {
                        //remove duplicates or excess knockof
                        Double knockedOffAmt = 0.0D;
                        Double tranAmount = k.TransactionAmount;
                        List<KnockOff> tranKO = GetTransactionKnockOffs(k.TransactionId);
                        if (tranKO != null && tranKO.Count > 0)
                        {
                            knockedOffAmt = tranKO.Sum(o => o.KnockOffAmount);
                            tranAmount = tranKO.FirstOrDefault().TransactionAmount;
                        }
                        if (k.KnockOffAmount + knockedOffAmt <= tranAmount)
                        {
                            _validKnockOff.Add(k);
                        }                        
                    }
                    if (_validKnockOff.Count > 0)
                    {
                        mgr.KnockOffTransactions(voucher, session.User.Id, _validKnockOff);
                        return ResponseCode.Ok;
                    }
                    else
                    {
                        return ResponseCode.BadRequest;
                    }
                }
                else
                {
                    return ResponseCode.ResourceNotFound;
                }
            }
            else
            {
                return ResponseCode.Unauthorized;
            }
        }
    }
}
