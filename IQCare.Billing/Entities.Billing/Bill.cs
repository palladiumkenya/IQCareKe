using System;

namespace Entities.Billing
{
    [Serializable]
    public class Bill : Discount<Bill>
    {
        public int BillId { get; set; }
        /// <summary>
        /// The location identifier
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }
        /// <summary>
        /// The reference number
        /// </summary>
        public string ReferenceNumber { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public double TotalAmount { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public double AmountOutstanding { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BillStatus Status { get; set; }

    }
    
}
