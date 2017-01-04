using System;
using Entities.Administration;
using Entities.Common;

namespace Entities.PatientCore
{
    /// <summary>
    /// 
    /// </summary>
     [Serializable]
    public class PatientIdentifier:IAuditEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
         public virtual int PatientId { get; set; }
        public virtual int PatientEnrollmentId { get; set; }
        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public virtual Patient Patient { get; set; }
         /// <summary>
         /// Gets or sets the identifier.
         /// </summary>
         /// <value>
         /// The identifier.
         /// </value>
         public Identifier Identifier { get; set; }
         /// <summary>
         /// Gets or sets the value.
         /// </summary>
         /// <value>
         /// The value.
         /// </value>
         public string Value { get; set; }

        public int CreatedBy
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public bool DeleteFlag
        {
            get; set;
        }

        public string AuditData
        {
            get; set;
        }
    }
}
