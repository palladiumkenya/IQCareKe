using System;

namespace Entities.Billing
{
    [Serializable]
    public class DiscountPlan
    {
        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
        public int PlanId { get; set; }
        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public double Rate { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DiscountPlan"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets the discounted pay method.
        /// </summary>
        /// <value>
        /// The discounted pay method.
        /// </value>
        // public  PaymentMethod DiscountedPayMethod {get;set;}
    }
    
}
