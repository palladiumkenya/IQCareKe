using System;
using System.Collections.Generic;
namespace Entities.Billing
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PaymentVoucher
    {
       
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the voucher date.
        /// </summary>
        /// <value>
        /// The voucher date.
        /// </value>
        public DateTime VoucherDate { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public Double Amount { get; set; }
        /// <summary>
        /// Gets or sets the type of the voucher.
        /// </summary>
        /// <value>
        /// The type of the voucher.
        /// </value>
        public string VoucherType { get; set; }
        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceId { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the delete date.
        /// </summary>
        /// <value>
        /// The delete date.
        /// </value>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// Gets or sets the delete by.
        /// </summary>
        /// <value>
        /// The delete by.
        /// </value>
        public int? DeleteBy { get; set; }

        /// <summary>
        /// Gets or sets the knock offs.
        /// </summary>
        /// <value>
        /// The knock offs.
        /// </value>
        public virtual List<KnockOff> KnockOffs { get; set; }
        /// <summary>
        /// Gets the knocked off amount.
        /// </summary>
        /// <value>
        /// The knocked off amount.
        /// </value>
        public double AmountUsed
        {
            get;
            set;
        }
        public double AmountAvailable
        {
            get
            {
                return Amount - AmountUsed;
            }
            
        }
       
    }
}
