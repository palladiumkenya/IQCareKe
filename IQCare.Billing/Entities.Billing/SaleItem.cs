using System;

namespace Entities.Billing
{
    [Serializable]
    public class SaleItem : Discount<SaleItem>
    {
        /// <summary>
        /// The item identifier
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// The item type identifier
        /// </summary>
        public int ItemTypeId { get; set; }
        /// <summary>
        /// The selling price
        /// </summary>
        public double? SellingPrice { get; set; }
        /// <summary>
        /// The item name
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// The item type name
        /// </summary>
        public string ItemTypeName { get; set; }
        /// <summary>
        /// The price date
        /// </summary>
        public DateTime? PriceDate { get; set; }
        /// <summary>
        /// The priced per item
        /// </summary>
        /// <value>
        /// The priced per item.
        /// </value>
        public bool? PricedPerItem { get; set; }
        /// <summary>
        /// Gets or sets the version stamp.
        /// </summary>
        /// <value>
        /// The version stamp.
        /// </value>
        public UInt64? VersionStamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaleItem"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
    }
}
