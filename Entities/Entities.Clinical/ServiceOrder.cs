using System;
using System.Collections.Generic;

namespace Entities.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ServiceOrder
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the visit identifier.
        /// </summary>
        /// <value>
        /// The visit identifier.
        /// </value>
        public int VisitId { get; set; }
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier from where the order orginated.
        /// </value>
        public int ModuleId { get; set; }
        /// <summary>
        /// Gets or sets the target module identifier.
        /// </summary>
        /// <value>
        /// The target module identifier.
        /// </value>
        public int TargetModuleId { get; set; }
        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Gets or sets the ordered by.
        /// </summary>
        /// <value>
        /// The ordered by.
        /// </value>
        public int OrderedBy { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }
        /// <summary>
        /// Gets or sets the clinical notes.
        /// </summary>
        /// <value>
        /// The clinical notes.
        /// </value>
        public string ClinicalNotes { get; set; }
        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public List<OrderedService> Services { get; set; }
    }
    
}
