using System;

namespace Entities.Billing
{
    [Serializable]
    public class PaymentMethod
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// The locked
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// The method description
        /// </summary>
        public string MethodDescription { get; set; }
        /// <summary>
        /// The control name
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PaymentMethod"/> is credit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if credit; otherwise, <c>false</c>.
        /// </value>
        public bool Credit { get; set; }
    }
}
