using System;

namespace Entities.Billing
{
    [Serializable]
    public class Price
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is bundled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is bundled; otherwise, <c>false</c>.
        /// </value>
        public bool IsBundled { get; set; }
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent { get; set; }
        public int PricePlanId { get; set; }
        public virtual PricePlan PricePlan { get; set; }
        /// <summary>
        /// Gets or sets the version stamp.
        /// </summary>
        /// <value>
        /// The version stamp.
        /// </value>
        public UInt64 VersionStamp { get; set; }
    }
}
