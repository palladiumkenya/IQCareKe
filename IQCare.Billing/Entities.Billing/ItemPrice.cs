using System;

namespace Entities.Billing
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ItemPrice
    {

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public int ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        public int ItemTypeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the name of the item type.
        /// </summary>
        /// <value>
        /// The name of the item type.
        /// </value>
        public string ItemTypeName { get; set; }
        /// <summary>
        /// Gets or sets the price date.
        /// </summary>
        /// <value>
        /// The price date.
        /// </value>
        public DateTime PriceDate { get; set; }
        /// <summary>
        /// Gets or sets the price on date.
        /// </summary>
        /// <value>
        /// The price on date.
        /// </value>
        public Price PriceOnDate { get; set; }
        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        public Price CurrentPrice { get; set; }

    }
    
}
