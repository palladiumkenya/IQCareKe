using System;

namespace Entities.Billing
{
    [Serializable]
    public class IssuableItem : SaleItem
    {
        /// <summary>
        /// Gets or sets the item issuance identifier.
        /// </summary>
        /// <value>
        /// The item issuance identifier.
        /// </value>
        public int? ItemIssuanceId { get; set; }
        /// <summary>
        /// Gets or sets the item issuance identifier.
        /// </summary>
        /// <value>
        /// The item issuance identifier.
        /// </value>
        public int? BillItemId { get; set; }
        /// <summary>
        /// Gets or sets the issue date.
        /// </summary>
        /// <value>
        /// The issue date.
        /// </value>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int IssuedQuantity { get; set; }
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public int? ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the cost center.
        /// </summary>
        /// <value>
        /// The cost center.
        /// </value>
        public string CostCenter { get; set; }
        /// <summary>
        /// Gets or sets the issued by.
        /// </summary>
        /// <value>
        /// The issued by.
        /// </value>
        public int IssuedById { get; set; }

        /// <summary>
        /// Gets or sets the name of the issued by.
        /// </summary>
        /// <value>
        /// The name of the issued by.
        /// </value>
        public string IssuedByName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IssuableItem"/> is received.
        /// </summary>
        /// <value>
        ///   <c>true</c> if received; otherwise, <c>false</c>.
        /// </value>
        public bool Received { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IssuableItem"/> is paid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paid; otherwise, <c>false</c>.
        /// </value>
        public bool Paid { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IssuableItem"/> is billed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if billed; otherwise, <c>false</c>.
        /// </value>
        public bool Billed { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can be billed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can be billed; otherwise, <c>false</c>.
        /// </value>
        public bool CanBeBilled { get; set; }
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DiscountDate">The discount date.</param>
        /// <returns></returns>
        protected override decimal CalculateDiscount(SaleItem t, int PatientId, DateTime DiscountDate)
        {
            return base.CalculateDiscount(t, PatientId, DiscountDate);
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal IssuedAmount
        {
            get
            {
                decimal sp = 0.0M;
                if (SellingPrice.HasValue) sp = SellingPrice.Value;
                return (sp * IssuedQuantity) - ItemDiscount;
            }
        }
        /// <summary>
        /// Gets or sets the billed amount.
        /// </summary>
        /// <value>
        /// The billed amount.
        /// </value>
        public decimal BilledAmount
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the item discount.
        /// </summary>
        /// <value>
        /// The item discount.
        /// </value>
        public decimal ItemDiscount
        {
            get
            {
                return CalculateDiscount(this, this.PatientId, this.IssueDate);
            }
        }

    }
    
}
