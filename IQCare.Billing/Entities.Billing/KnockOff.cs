using System;

namespace Entities.Billing
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class KnockOff
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the voucher identifier.
        /// </summary>
        /// <value>
        /// The voucher identifier.
        /// </value>
        public int VoucherId { get; set; }
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public virtual int TransactionId { get; set; }
        /// <summary>
        /// Gets or sets the payment type identifier.
        /// </summary>
        /// <value>
        /// The payment type identifier.
        /// </value>
        public virtual int PaymentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public Double KnockOffAmount { get; set; }
        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// <value>
        /// The transaction amount.
        /// </value>
        public Double TransactionAmount { get; set; }
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
    }
}
