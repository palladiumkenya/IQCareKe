using System;

namespace Entities.Billing
{
    [Serializable]
    public class BillItem : Discount<BillItem>
    {
        /// <summary>
        /// The bill item identifier
        /// </summary>
        public int? BillItemId { get; set; }
        /// <summary>
        /// The bill item identifier
        /// </summary>
        public int? BillId { get; set; }
        /// <summary>
        /// The item identifier
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        public int ItemTypeId { get; set; }
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
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public int? ModuleId { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the added by.
        /// </summary>
        /// <value>
        /// The added by.
        /// </value>
        public int? AddedBy { get; set; }
        /// <summary>
        /// Gets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal? Discount
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the calculated discount.
        /// </summary>
        /// <value>
        /// The calculated discount.
        /// </value>
        public decimal CalculatedDiscount
        {
            get
            {
                return CalculateDiscount(this, this.PatientId, this.BillItemDate);
            }
        }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BillItem"/> is received.
        /// </summary>
        /// <value>
        ///   <c>true</c> if received; otherwise, <c>false</c>.
        /// </value>
        public bool Received { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BillItem"/> is paid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paid; otherwise, <c>false</c>.
        /// </value>
        public bool Paid { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BillItem"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets the item source reference.
        /// </summary>
        /// <value>
        /// The item source reference.
        /// </value>
        public int? ItemSourceReference { get; set; }
        /// <summary>
        /// The item name
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the cost center.
        /// </summary>
        /// <value>
        /// The cost center.
        /// </value>
        public string CostCenter { get; set; }
        /// <summary>
        /// Gets or sets the bill item date.
        /// </summary>
        /// <value>
        /// The bill item date.
        /// </value>
        public DateTime BillItemDate { get; set; }
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DiscountDate">The discount date.</param>
        /// <returns></returns>
        protected override decimal CalculateDiscount(BillItem t, int PatientId, DateTime DiscountDate)
        {

            return base.CalculateDiscount(t, PatientId, DiscountDate);

        }
    }
   
}
