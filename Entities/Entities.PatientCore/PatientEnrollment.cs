using System;
using Entities.Administration;
using System.Collections.Generic;

namespace Entities.PatientCore
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PatientEnrollment
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }
        /// <summary>
        /// Gets or sets the service area identifier.
        /// </summary>
        /// <value>
        /// The service area identifier.
        /// </value>
        public int ServiceAreaId { get; set; }
        /// <summary>
        /// Gets or sets the enrollment date.
        /// </summary>
        /// <value>
        /// The enrollment date.
        /// </value>
        public DateTime EnrollmentDate { get; set; }
        /// <summary>
        /// Gets or sets the service area.
        /// </summary>
        /// <value>
        /// The service area.
        /// </value>
        public ServiceArea ServiceArea { get; set; }
        /// <summary>
        /// Gets or sets the care status.
        /// </summary>
        /// <value>
        /// The care status.
        /// </value>
        public string CareStatus { get; set; }
        /// <summary>
        /// Gets or sets the exit reason.
        /// </summary>
        /// <value>
        /// The exit reason.
        /// </value>
        public string ExitReason { get; set; }
        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>
        /// The identifiers.
        /// </value>
        public virtual List<PatientIdentifier> Identifiers {get;set;}
    }
}
